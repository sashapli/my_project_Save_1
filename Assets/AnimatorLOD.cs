using UnityEngine;

public class AnimatorLOD : MonoBehaviour
{
    [Header("Настройки LOD")]
    public float updateInterval = 0.1f; // Интервал обновления анимации
    public bool useDistanceBasedLOD = true; // Использовать LOD по расстоянию
    public float maxDistance = 20f; // Дистанция, после которой анимация замедляется

    private Animator animator;
    private float timer;
    private bool isVisible = true;
    private float currentUpdateInterval;
    private Transform playerCamera;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator не найден на объекте " + gameObject.name);
            enabled = false;
            return;
        }

        // Находим основную камеру
        if (Camera.main != null)
            playerCamera = Camera.main.transform;

        currentUpdateInterval = updateInterval;
    }

    void Update()
    {
        // Если объект не видим, отключаем анимацию полностью
        if (!isVisible)
        {
            if (animator.enabled)
                animator.enabled = false;
            return;
        }

        // Если используем LOD по расстоянию
        if (useDistanceBasedLOD && playerCamera != null)
        {
            float distance = Vector3.Distance(transform.position, playerCamera.position);
            if (distance > maxDistance)
            {
                // Объект далеко - отключаем анимацию
                if (animator.enabled)
                    animator.enabled = false;
                return;
            }
            else if (!animator.enabled)
            {
                // Объект приблизился - включаем анимацию обратно
                animator.enabled = true;
            }
        }

        // Обновляем анимацию с пониженной частотой
        timer += Time.deltaTime;
        if (timer >= currentUpdateInterval)
        {
            timer = 0;
            // Включаем аниматор для одного обновления
            if (!animator.enabled)
                animator.enabled = true;

            // Не вызываем Update вручную - это может сломать аниматор
            // Просто даем ему работать как обычно
        }
    }

    // Вызывается когда объект становится видимым для камеры
    void OnBecameVisible()
    {
        isVisible = true;
        if (animator != null)
            animator.enabled = true;
    }

    // Вызывается когда объект становится НЕвидимым для камеры
    void OnBecameInvisible()
    {
        isVisible = false;
        if (animator != null)
            animator.enabled = false;
    }

    // Для отладки - визуализация расстояния в Scene View
    void OnDrawGizmosSelected()
    {
        if (useDistanceBasedLOD)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxDistance);
        }
    }
}