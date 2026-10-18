using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutarial : MonoBehaviour
{
    public int currentSlite;
    public GameObject[] allslite;
    public GameObject NextSlid, EndTutarial;

    private void OnEnable()
    {
        currentSlite = 0;
        UIUpdatew();
        NextSlid.SetActive(true);
        EndTutarial.SetActive(false);
    }

    public void NextSlit()
    {
        if (currentSlite >= 2)
            return;
        currentSlite++;
        if (currentSlite == 2)
        {
            NextSlid.SetActive(false);
            EndTutarial.SetActive(true);
        }
        UIUpdatew();
    }

    public void InNextSlit()
    {
        if (currentSlite <= 0)
            return;
        currentSlite--;
        if (currentSlite != 2)
        {
            NextSlid.SetActive(true);
            EndTutarial.SetActive(false);
        }
        UIUpdatew();
    }

    public void UIUpdatew()
    {
        foreach(var i in allslite)
            i.SetActive(false);
        allslite[currentSlite].SetActive(true);
    }
}
