using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class springWater : MonoBehaviour
{
    public float velocity = 0;
    public float force = 0;
    public float height = 0f;
    private float target_height = 0f;
    [SerializeField]
    private SpriteShapeController spriteShapeController = null;
    private int waveIndex = 0;
    public float resistance = 40f;

    private bool isInitialized = false;

    public void Init(SpriteShapeController ssc)
    {
        spriteShapeController = ssc;

        // Определяем индекс по позиции в родительском объекте
        waveIndex = transform.GetSiblingIndex() + 1;

        velocity = 0;
        height = transform.localPosition.y;
        target_height = transform.localPosition.y;
        isInitialized = true;
    }

    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        if (!isInitialized) return;
        if (spriteShapeController == null) return;

        height = transform.localPosition.y;
        var x = height - target_height;
        var loss = -dampening * velocity;

        force = -springStiffness * x + loss;
        velocity += force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y + velocity, transform.localPosition.z);
    }

    public void WavePointUpdate()
    {
        if (!isInitialized) return;
        if (spriteShapeController == null) return;

        Spline waterSpline = spriteShapeController.spline;
        if (waterSpline == null) return;

        // Проверяем индекс
        int pointCount = waterSpline.GetPointCount();
        if (waveIndex < 1 || waveIndex >= pointCount - 1)
        {
            // Если индекс невалидный, ищем правильный
            waveIndex = FindCorrectIndex(waterSpline);
            if (waveIndex < 1 || waveIndex >= pointCount - 1)
            {
                return;
            }
        }

        try
        {
            Vector3 wavePosition = waterSpline.GetPosition(waveIndex);
            waterSpline.SetPosition(waveIndex, new Vector3(wavePosition.x, transform.localPosition.y, wavePosition.z));
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error updating wave at index {waveIndex}: {e.Message}");
        }
    }

    private int FindCorrectIndex(Spline waterSpline)
    {
        if (waterSpline == null) return -1;

        int pointCount = waterSpline.GetPointCount();
        Vector3 myPos = transform.position;
        int bestIndex = -1;
        float bestDistance = float.MaxValue;

        for (int i = 1; i < pointCount - 1; i++)
        {
            try
            {
                Vector3 pointPos = waterSpline.GetPosition(i);
                float distance = Vector3.Distance(myPos, pointPos);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestIndex = i;
                }
            }
            catch { continue; }
        }

        return bestIndex;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) return;
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        var speed = rb.velocity;
        velocity += speed.y / resistance;
    }
}