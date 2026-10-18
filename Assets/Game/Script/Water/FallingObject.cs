using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private Vector2 movementDirection;
    public Rigidbody2D rigidbody2D;
    [SerializeField]
    private float forceAmount;
    private float timeElapsed;
    private bool isMoving = false;

    void Start()
    {
        rigidbody2D.velocity = Vector3.down * forceAmount;
    }

    void Update()
    {
        // Используем GetAxisRaw для более быстрого реагирования
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(horizontal, vertical);
        timeElapsed += Time.deltaTime;

        // Проверяем, двигается ли объект
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            isMoving = true;
            rigidbody2D.velocity = movementDirection.normalized * forceAmount;
        }
        else
        {
            isMoving = false;
        }
    }
}

