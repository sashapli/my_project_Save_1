using System.Collections.Generic;
using UnityEngine;

public class Turel2 : MonoBehaviour
{
    [Header("Настройки")]
    public float damage = 10f;
    public float fireRate = 1f;
    public float range = 10f;
    public float offsetAngle = 0f;
    public float CD = 0.5f; // Радиус капсулы для детекции

    [Header("Компоненты")]
    public CircleCollider2D collider2D;
    public Transform turretHead;
    public Transform MainObj;
    public Animator Effect;
    public AudioSource audio;
    public Healter_Script HealterSc;

    [Header("Ссылки")]
    public Healter_Script healerScript;

    private List<Transform> fishList = new List<Transform>();
    private Transform currentTarget;
    private float nextFireTime;


    private void Update()
    {
        collider2D.radius = range;

        Transform target = FindClosest();
        if (target == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > range)
            return;

        Vector2 worldDirection = (target.position - turretHead.position).normalized;
        Vector2 localDirection = turretHead.parent.InverseTransformDirection(worldDirection);
        float angle = Mathf.Atan2(localDirection.y, localDirection.x) * Mathf.Rad2Deg;
        turretHead.localEulerAngles = new Vector3(0, 0, angle + offsetAngle);

        if (Time.time - nextFireTime > CD)
        {
            nextFireTime = Time.time;
            HealterBot healterBot = target.GetComponent<HealterBot>();
            if (healterBot != null) // Всегда проверяйте на null!
            {
                Effect.SetTrigger("Shot");
                audio.Play();
                float HPCurrentFish = healterBot.TakeDemage(damage);
                if (HPCurrentFish <= 0)
                {
                    HealterSc.AmFish++;
                    HealterSc.TakeDemag(-10);
                    HealterSc.MainCamper.transform.localScale = Vector3.one * (1 + HealterSc.AmFish * HealterSc.NextScale);
                    HealterSc.Flid = 1 + (int)(HealterSc.AmFish * HealterSc.NextScale2);
                    Debug.Log($"Рыб поймано: {HealterSc.AmFish}");
                }
            }
        }
    }

    Transform FindClosest()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 playerPosition = transform.position;

        foreach (Transform obj in fishList)
        {
            if (obj == null) continue; // Пропускаем пустые ссылки

            float distance = Vector3.Distance(playerPosition, obj.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = obj;
            }
        }

        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingObject"))
        {
            if (!fishList.Contains(collision.transform))
                fishList.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FallingObject"))
        {
            fishList.Remove(collision.transform);
            if (currentTarget == collision.transform)
                currentTarget = null;
        }
    }
}