using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BotFish : MonoBehaviour
{
    [Header("Движение")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float AdrinalinSpeed = 20f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float changeDirectionInterval = 2f; // уменьшил интервал
    [SerializeField] private float AdrinalinTime = 7f;

    [SerializeField] private float time;
    [SerializeField] private float timeMax;
    [SerializeField] private float timeMin;

    [Header("Граница воды")]
    [SerializeField] private float waterLevel = 12f;

    private float targetDirection;
    private Vector2 Direction;
    private float timerLate;
    private float timerLate2;
    private Rigidbody2D rb;

    private void Start()
    {
        time = Random.Range(timeMin, timeMax);
        rb = GetComponent<Rigidbody2D>();
        targetDirection = Random.Range(-180, 180);
        float RadianDirection = targetDirection * Mathf.Deg2Rad;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetDirection-180);
        Direction = new Vector2(Mathf.Cos(RadianDirection), Mathf.Sin(RadianDirection));
    }

    private void Update()
    {
        if (Time.time - timerLate > time && transform.position.y < waterLevel)
        {
            ReRotate(-180, 180);
            timerLate = Time.time;
        }
        if (Time.time - timerLate2 > 1f && transform.position.y >= waterLevel)
        {
            ReRotate(-80, -100);
            timerLate2 = Time.time;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetDirection - 180), Time.deltaTime * rotationSpeed);
        rb.velocity = Direction * moveSpeed;
    }

    public void ReRotate(int ro1, int ro2)
    {
        time = Random.Range(timeMin, timeMax);
        targetDirection = Random.Range(ro1, ro2);
        float RadianDirection = targetDirection * Mathf.Deg2Rad;
        Direction = new Vector2(Mathf.Cos(RadianDirection), Mathf.Sin(RadianDirection));
    }

    async public void RunAdrinalin()
    {
        moveSpeed = 20;
        if ( transform.position.y < waterLevel)
            ReRotate(-180, 180);
        else
            ReRotate(-80, -100);
        
        await Task.Delay((int)(AdrinalinTime * 1000));
        moveSpeed = 2;
    }

    public void ApplaySpeed(float Speed, float AdrSpeed)
    {
        moveSpeed = Speed;
        AdrinalinSpeed = AdrSpeed;
    }
}