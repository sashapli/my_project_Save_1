using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform FishPlayer;
    public Transform ZeroPos;
    public float CamSpeed = 4f;

    private void Start()
    {
        StartGame();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(FishPlayer.position.x, FishPlayer.position.y, -10), CamSpeed * Time.fixedDeltaTime);
    } 
    public void Resetpos() =>   FishPlayer = ZeroPos;
    public void StartGame() => FishPlayer = FindObjectOfType<MoveFish>().gameObject.transform;
   
}
