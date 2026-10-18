using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using YG;
using TMPro;

public class SaveMenager : MonoBehaviour
{
    public static SaveMenager singalton;
    public Image FillNam1, FillNam2, FillNam3;
    public TMP_Text CoinText;
    private void Awake() => singalton = this;
    public void SetCoin(int Coin)
    {
        YandexGame.savesData.Coin += Coin;
        YandexGame.SaveProgress();
        UpdateUI();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.P))
        {
            SetCoin(10);
        }
        if (Input.GetKey(KeyCode.R))
        {
            YandexGame.savesData.Coin = 0;
            YandexGame.savesData.LevalInt1 = 0;
            YandexGame.savesData.LevalInt2 = 0;
            YandexGame.savesData.LevalInt3 = 0;
            YandexGame.SaveProgress();
            UpdateUI();
        }
    }

    public void SetIt1()
    {
        YandexGame.savesData.LevalInt1++;
        YandexGame.SaveProgress();
        UpdateUI();
    }
    public void SetIt2()
    {
        YandexGame.savesData.LevalInt2++;
        YandexGame.SaveProgress();
        UpdateUI();
    }
    public void SetIt3()
    {
        YandexGame.savesData.LevalInt3++;
        YandexGame.SaveProgress();
        UpdateUI();
    }
    public void SetTime(float neTim)
    {
        YandexGame.savesData.time = neTim;
        YandexGame.SaveProgress();
    }

    public void SetSound(float neSound)
    {
        YandexGame.savesData.VoulemGameSound = neSound;
        YandexGame.SaveProgress();
    }

    public void SetMusic(float neMusic)
    {
        YandexGame.savesData.VoulemMusic = neMusic;
        YandexGame.SaveProgress();
    }

    public void SetModeMove()
    {
        YandexGame.savesData.IsMove = !YandexGame.savesData.IsMove;
        YandexGame.SaveProgress();
    }

    public int GetCoin()
    {
       return YandexGame.savesData.Coin;
    }
    public int GetIt1()
    {
        return YandexGame.savesData.LevalInt1;
    }
    public int GetIt2()
    {
        return YandexGame.savesData.LevalInt2;
    }
    public int GetIt3()
    {
        return YandexGame.savesData.LevalInt3;
    }
    public float GetTime()
    {
        return YandexGame.savesData.time;
    }

    public float GetSound()
    {
        return YandexGame.savesData.VoulemGameSound;
    }
    public float GetMusic()
    {
        return YandexGame.savesData.VoulemMusic;
    }

    public bool GetModMove()
    {
        return YandexGame.savesData.IsMove;
    }

    public void UpdateUI()
    {
        CoinText.text = "" + YandexGame.savesData.Coin;
    }
    public void UpdateUI2()
    {
        FillNam1.fillAmount = (float)(YandexGame.savesData.LevalInt1 + 1f) / 10f;
        FillNam2.fillAmount = (float)(YandexGame.savesData.LevalInt2 + 1f) / 10f;
        FillNam3.fillAmount = (float)YandexGame.savesData.LevalInt3 / 3f;
    }
}
