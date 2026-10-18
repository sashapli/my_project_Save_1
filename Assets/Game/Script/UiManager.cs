using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using YG;

public class UiManager : MonoBehaviour
{
    public static UiManager Singalton;

    public CanvasGroup group;
    public CanvasGroup group2;
    public CanvasGroup group3;

    public Transform perentFood;
    public Transform perentFish;

    public GameObject PlayerFish;

    public FishMenager fishMenager;
    public TurelApplay turelApplay;

    public int CiuntSecund;
    public float late;
    public bool isStart;
    public TMP_Text Timer;

    public CanvasGroup MINU;
    public CanvasGroup Shop;
    public CanvasGroup option;
    public Transform Camper;

    public TMP_Text description;
    public TMP_Text MoneyCur;
    public string[] descriptionAllRu;
    public string[] descriptionAllEn;
    public ItemMoney[] AllSell;
    public byte ItemCurent = 0;
    public Image Fillitem1, Fillitem2, Fillitem3;
    public RectTransform button;
    public RectTransform Liderbort;
    public AdSceneManager AdScene;
    public MoveFish moveFish;
    public GameObject Tutarial;
    public CamMove CamMove;
    public GameObject boomEffect;

    void Start()
    {
        group.alpha = 1;
        group2.alpha = 0;
        group3.alpha = 0;
        group.gameObject.SetActive(true);
        group2.gameObject.SetActive(false);
        group3.gameObject.SetActive(false);
        SaveMenager.singalton.UpdateUI();
        Singalton = this;
        if (YandexGame.savesData.isFirstSession)
        {
            Tutarial.SetActive(true);
        }
    }

    private void Update()
    {
        if (Time.time - late > 1 && isStart)
        {
            late = Time.time;
            CiuntSecund++;
            int sec = CiuntSecund % 60;
            int min = CiuntSecund / 60;
            Timer.text = min.ToString("00") + ":" + sec.ToString("00");
        }
    }

    public void OpenShop()
    {
        SaveMenager.singalton.UpdateUI2();
        RectTransform rect = MINU.gameObject.GetComponent<RectTransform>();
        RectTransform rect2 = Shop.gameObject.GetComponent<RectTransform>();
        Shop.gameObject.SetActive(true);

        rect.DOAnchorPos(new Vector2(-100, rect.anchoredPosition.y), 1).From(Vector2.zero);
        MINU.DOFade(0, 1).From(1).OnComplete(() => MINU.gameObject.SetActive(false));

        rect2.DOAnchorPos(new Vector2(0, rect2.anchoredPosition.y), 1).From(new Vector2(100, 0));
        Shop.DOFade(1, 1).From(0);

        Camper.DOMoveX(-6.5f, 1);
    }

    public void Openoption()
    {
        option.gameObject.SetActive(true);
        RectTransform rect = option.gameObject.GetComponent<RectTransform>();
        option.DOFade(1, 0.5f).From(0);
        rect.DOScale(1, 0.5f).From(3);
    }

    public void Cloesoption()
    {
        RectTransform rect = option.gameObject.GetComponent<RectTransform>();
        option.DOFade(0, 0.5f).From(1);
        rect.DOScale(3, 0.5f).From(1).OnComplete(() => option.gameObject.SetActive(false));
    }

    public void CloesShop()
    {
        RectTransform rect = MINU.gameObject.GetComponent<RectTransform>();
        RectTransform rect2 = Shop.gameObject.GetComponent<RectTransform>();
        MINU.gameObject.SetActive(true);
        rect.DOAnchorPos(new Vector2(0, rect.anchoredPosition.y), 1).From(new Vector2(-100, 0));
        MINU.DOFade(1, 1).From(0);

        rect2.DOAnchorPos(new Vector2(-100, rect2.anchoredPosition.y), 1).From(new Vector2(0, 0));
        Shop.DOFade(0, 1).From(0).OnComplete(() => Shop.gameObject.SetActive(false));

        Camper.DOMoveX(0, 1);
    }

    public void ClosedMenu()
    {
        isStart = true;
        late = Time.time;
        group.gameObject.SetActive(true);
        group2.gameObject.SetActive(true);
        group.DOFade(0, 1).SetUpdate(true).OnComplete(() => group.gameObject.SetActive(false));
        group2.DOFade(1, 1).SetUpdate(true);
    }

    public void RemoveMenu()
    {
        int childCount = perentFood.childCount;
        GameObject[] children = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
            children[i] = perentFood.GetChild(i).gameObject;
        foreach (var i in children)
            Destroy(i);

        int childCount2 = perentFish.childCount;
        GameObject[] children2 = new GameObject[childCount2];

        for (int i = 0; i < childCount2; i++)
            children2[i] = perentFish.GetChild(i).gameObject;
        foreach (var i in children2)
            Destroy(i);

        group.gameObject.SetActive(true);
        group3.gameObject.SetActive(true);
        group.DOFade(1, 1).SetUpdate(true);
        group3.DOFade(0, 1).SetUpdate(true).OnComplete(() => group3.gameObject.SetActive(false));

        fishMenager.CountSpawn = 0;
    }

    public void Item1()
    {
        description.text = "" + YandexGame.savesData.language == "ru" ? descriptionAllRu[0] : descriptionAllEn[0];
        MoneyCur.text = "" + AllSell[0].money[SaveMenager.singalton.GetIt1()];
        ItemCurent = 1;
    }
    public void Item2()
    {
        description.text = "" + YandexGame.savesData.language == "ru" ? descriptionAllRu[1] : descriptionAllEn[1];
        MoneyCur.text = "" + AllSell[1].money[SaveMenager.singalton.GetIt2()];
        ItemCurent = 2;
    }
    public void Item3()
    {
        description.text = "" + YandexGame.savesData.language == "ru" ? descriptionAllRu[2] : descriptionAllEn[2];
        MoneyCur.text = "" + AllSell[2].money[SaveMenager.singalton.GetIt3()];
        ItemCurent = 3;
    }

    public void BuyItem()
    {
        switch (ItemCurent)
        {
            case 1:
                if (AllSell[0].money[SaveMenager.singalton.GetIt1()] <= SaveMenager.singalton.GetCoin() && SaveMenager.singalton.GetIt1() < 10)
                {
                    SaveMenager.singalton.SetCoin(-AllSell[0].money[SaveMenager.singalton.GetIt1()]);
                    MoneyCur.text = "" + AllSell[0].money[SaveMenager.singalton.GetIt1()];
                    SaveMenager.singalton.SetIt1();
                }
                else
                {
                    button.DOShakePosition(duration: 0.5f, strength: new Vector3(50, 0, 0), vibrato: 20, randomness: 90, snapping: false, fadeOut: true);
                }
                break;
            case 2:
                if (AllSell[1].money[SaveMenager.singalton.GetIt2()] <= SaveMenager.singalton.GetCoin() && SaveMenager.singalton.GetIt2() < 10)
                {
                    SaveMenager.singalton.SetCoin(-AllSell[1].money[SaveMenager.singalton.GetIt2()]);
                    MoneyCur.text = "" + AllSell[1].money[SaveMenager.singalton.GetIt2()];
                    SaveMenager.singalton.SetIt2();
                }
                else
                {
                    button.DOShakePosition(duration: 0.5f, strength: new Vector3(50, 0, 0), vibrato: 20, randomness: 90, snapping: false, fadeOut: true);
                }
                break;
            case 3:
                if (AllSell[2].money[SaveMenager.singalton.GetIt3()] <= SaveMenager.singalton.GetCoin() && SaveMenager.singalton.GetIt3() < 3)
                {
                    SaveMenager.singalton.SetCoin(-AllSell[2].money[SaveMenager.singalton.GetIt3()]);
                    MoneyCur.text = "" + AllSell[2].money[SaveMenager.singalton.GetIt3()];
                    SaveMenager.singalton.SetIt3();
                    turelApplay.UpdateTurel();
                }
                else
                {
                    button.DOShakePosition(duration: 0.5f, strength: new Vector3(50, 0, 0), vibrato: 20, randomness: 90, snapping: false, fadeOut: true);
                }
                break;
        }
        SaveMenager.singalton.UpdateUI2();
    }

    public void OpClLiderBort()
    {
        if(Liderbort.anchoredPosition.x > 800)
        {
            Liderbort.DOAnchorPos(new Vector2(1,0), 1).OnComplete(() =>Liderbort.anchoredPosition = new Vector2(0,0));
        }
        if (Liderbort.anchoredPosition.x < 2)
        {
            Liderbort.DOAnchorPos(new Vector2(820, 0), 1).OnComplete(() => Liderbort.anchoredPosition = new Vector2(820, 0));
        }
    }
}
[System.Serializable]
public class ItemMoney
{
    public int[] money;
}
