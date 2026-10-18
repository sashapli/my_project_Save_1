using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour
{
    public float Speed = 50000;
    public float RotationSpeed = 7;
    public float DravWater = 0.2f;
    public float DravGraund = 2f;
    public float Gravity = 2;

    private Vector2 movementDirection;
    public Rigidbody2D _Rg;
    public float CamSpeed =4;

    public Vector3 _velocity;
    public bool isSuha;
    public Vector2 DerectionMove; // Теперь это направление, а не позиция

    public Healter_Script MeHPScript;
    public Jostick_Move jostick;
    public UiManager uiM;

    private void Start()
    {
        transform.localScale = Vector3.one * (1 +MeHPScript.AmFish * MeHPScript.NextScale);
        _Rg = GetComponent<Rigidbody2D>();
        if (SaveMenager.singalton.GetModMove())
            jostick.gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (!SaveMenager.singalton.GetModMove())
        {
            Vector3 mousePos = GetMouseWorldPosition();
            mousePos.z = 0;

            Vector3 directionToMouse = mousePos - transform.position;

            // Это и есть ваше направление движения!
            DerectionMove = new Vector2(directionToMouse.x, directionToMouse.y).normalized;
        }
        else
            DerectionMove = new Vector2(jostick._InputVector.x, jostick._InputVector.y).normalized;
        
        movementDirection = DerectionMove;

        if (movementDirection != Vector2.zero && Input.GetMouseButton(0))
        {
            _velocity = !isSuha ? (Vector3)movementDirection * Speed * Time.fixedDeltaTime : Vector3.zero;
            _Rg.AddForce(_velocity);

            // Поворачиваем рыбу в сторону мыши
            float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg - 180;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

            // Переворачиваем спрайт, если нужно
            if (movementDirection.x > 0)
                transform.localScale = new Vector3(1+MeHPScript.AmFish * MeHPScript.NextScale, -(1+MeHPScript.AmFish * MeHPScript.NextScale), 1+ MeHPScript.AmFish * MeHPScript.NextScale);
            else
                transform.localScale = Vector3.one * (1 +MeHPScript.AmFish * MeHPScript.NextScale);
        }

        if (transform.position.y >= 12)
        {
            isSuha = true;
            _Rg.gravityScale = Gravity;
            _Rg.drag = DravGraund;
        }
        else
        {
            isSuha = false;
            _Rg.gravityScale = 0;
            _Rg.drag = DravWater;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        // Проверяем, зажата ли ЛКМ (на ПК) или тач (на телефоне)
        if (Input.GetMouseButton(0)) // 0 - левая кнопка мыши
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // Важно! Расстояние от камеры

            // Конвертируем в мировые координаты
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f;

            return worldPos;
        }
        else if (Input.touchCount > 0) // Для телефона
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPos = touch.position;
                touchPos.z = 10f;

                Vector3 worldPos = Camera.main.ScreenToWorldPoint(touchPos);
                worldPos.z = 0f;

                return worldPos;
            }
        }

        return Vector3.zero; // Если не нажато
    }

    public void MoveRemove()
    {
        transform.eulerAngles = Vector3.zero;
        isSuha = false;
        _velocity = Vector3.zero;
        DerectionMove = Vector2.zero;
    }

    public void ResetCamper()
    {
        transform.position = new Vector3(0, 32.4f, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = Vector3.one;
        MeHPScript.AmFish = 0;
        _Rg.simulated = false;
        MeHPScript.enabled = false;
        enabled = false;
        uiM.CiuntSecund = 0;
        uiM.isStart = false;
    }
}