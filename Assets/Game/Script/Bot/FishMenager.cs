 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMenager : MonoBehaviour
{
    public GameObject Fish, perent;
    public Transform[] ZoneSpawned;
    public List<GameObject> FishAll;

    public int MaxSpawn = 10;
    public int LateTime = 3;
    private float Late;
    public int CountSpawn;

    private void Update()
    {
        if (Time.time - Late > LateTime && CountSpawn < MaxSpawn)
        {
            Late = Time.time;
            CountSpawn++;
            float Size = Random.Range(0.1f, 0.4f);
            Transform[] ZoneSpawn = ZoneSpawned;

            int posIndex = Random.Range(0, ZoneSpawn.Length);
            int LevalFish = Random.Range(0, 5);
            GameObject fish = Instantiate(Fish, perent.transform);
            FishAll.Add(fish);
            fish.transform.position = ZoneSpawn[posIndex].position;
            fish.transform.localScale = Vector3.one * Size;
            fish.GetComponent<FishLeval>().applayFish(LevalFish);
        }
    }

    public void ResetFish()
    {
        foreach(var i in FishAll)
            Destroy(i);
        enabled = false;
    }
}
