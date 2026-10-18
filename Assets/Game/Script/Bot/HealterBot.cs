using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealterBot : MonoBehaviour
{
    [SerializeField] private float HP;
    [SerializeField] private float MaxHP;

    [SerializeField] private Image FillAmoutHealter;
    public BotFish botFish;
    public FishMenager fishMenager;

    private void Start()
    {
        HP = MaxHP;
        botFish = GetComponent<BotFish>();
        fishMenager = FindObjectOfType<FishMenager>();
    }
    public float TakeDemage(float Damag)
    {
        HP -= Damag;
        UpateUI();
        botFish.RunAdrinalin();

        if (HP <= 0)
        {
            fishMenager.CountSpawn--;
            Destroy(gameObject);
        }
        return HP;
    }

    public void AplayHPMax(int MAxH)
    {
        MaxHP = MAxH;
        HP = MaxHP;
    }

    private void UpateUI()
    {
        FillAmoutHealter.fillAmount = (float)HP / (float)MaxHP;
    }
}
