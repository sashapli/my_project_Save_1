using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using YG;

public class Healter_Script : MonoBehaviour
{
    public int MaxHealter =100;
    public float CurentHealter;
    public float AmFish;

    public Image FillBarHealter;
    public TMP_Text HealterText;
    public UiManager uiManager;
    public AnimatorSpript animatorSpript;
    public MoveFish moveFish;

    public GameObject MainCamper;
    public float NextScale;
    public float NextScale2;
    public float NextScaleSpeed;

    private float late;
    private float late2;
    public float Flid = 1;
    [SerializeField] private float CDAttak;
    public Image FillFlud;
    public GameObject EffectDied;

    [SerializeField] private float CurrentFlud = 100;

    public float Demag = 20;
    public float late2TIm;

    private void OnEnable()
    {
        RempveHealter();
        FillBarHealter.fillAmount = (float)(CurentHealter / 100);
        HealterText.text = "" + CurentHealter;

        late2TIm = 0.2f + (SaveMenager.singalton.GetIt1()* 0.1f);
        Demag = 20 + (SaveMenager.singalton.GetIt2()* 10);
    }

    public void Update()
    {if (Time.time - late2 > late2TIm)
        {
            late2 = Time.time;
            TakeDemag(Flid);
        }
    }

    public void TakeDemag(float Demag)
    {
        CurentHealter -= Demag;
        CurentHealter = Mathf.Min(CurentHealter, 100);
        FillBarHealter.fillAmount = (float)((float)CurentHealter / (float)MaxHealter);
        HealterText.text =  "" + CurentHealter;
        if (CurentHealter <= 0)
        {
            HealterText.text = "0";
            EffectDied.SetActive(true);
            moveFish._Rg.simulated = false;
            StartCoroutine(LostMenuCoroutine());
        }
    }

     IEnumerator LostMenuCoroutine()
    {
        SaveMenager.singalton.SetCoin(uiManager.CiuntSecund / 3);
        uiManager.fishMenager.ResetFish();
        yield return new WaitForSeconds(2f); // Вместо await Task.Delay(2000)

        uiManager.CamMove.Resetpos();
        moveFish.ResetCamper();
        uiManager.boomEffect.SetActive(false);

        yield return new WaitForSeconds(1f);

        uiManager.group.gameObject.SetActive(true);
        uiManager.group2.gameObject.SetActive(false);
        uiManager.group3.gameObject.SetActive(false);
        uiManager.group.DOFade(1, 1).SetUpdate(true);
        uiManager.group2.alpha = 0;
        uiManager.group3.alpha = 0;

        float maxTime = SaveMenager.singalton.GetTime();
        if (uiManager.CiuntSecund > maxTime)
        {
            maxTime = uiManager.CiuntSecund;
            SaveMenager.singalton.SetTime(maxTime);
        }

        // Отправляем в лидерборд МАКСИМАЛЬНОЕ время
        YandexGame.NewLBScoreTimeConvert("TimeFish", (int)maxTime * 1000);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("FallingObject") && Time.time - late > CDAttak)
        {
            late = Time.time;
            animatorSpript.Attak();

            HealterBot healterBot = other.GetComponent<HealterBot>();
            if (healterBot != null) // Всегда проверяйте на null!
            {
                float HPCurrentFish = healterBot.TakeDemage(Demag);
                if (HPCurrentFish <= 0)
                {
                    AmFish++;
                    TakeDemag(-10);
                    MainCamper.transform.localScale = Vector3.one * (1+AmFish * NextScale);
                    Flid = 1 + (int)(AmFish * NextScale2);
                    Debug.Log($"Рыб поймано: {AmFish}");
                }
            }
        }
    }

    public void RempveHealter()
    {
        CurentHealter = MaxHealter;
    }
}
