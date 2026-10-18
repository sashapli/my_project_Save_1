using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turel1 : MonoBehaviour
{
    public int DemaG;
    private float late;
    public float TimeCd;

    public Animator Effect;
    public AudioSource audio;
    public Healter_Script HealterSc;

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("каснулся зоны стрельбы");
        if (Time.time - late > TimeCd)
        {
            late = Time.time;
            if (other.tag == "FallingObject")
            {
                Debug.Log("каснулся зоны стрельбы fish");
                HealterBot healterBot = other.GetComponent<HealterBot>();
                if (healterBot != null) // Всегда проверяйте на null!
                {
                    Effect.SetTrigger("Shot");
                    audio.Play();
                    float HPCurrentFish = healterBot.TakeDemage(DemaG);
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Effect.SetTrigger("Shot");
            audio.Play();
        }
    }
}
