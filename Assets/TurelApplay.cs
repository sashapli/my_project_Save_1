using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurelApplay : MonoBehaviour
{
    public List<GO> turellev; 

    void Start()
    {
        UpdateTurel();
    }

    public void UpdateTurel()
    {
        Debug.Log(SaveMenager.singalton.GetIt3());

        switch (SaveMenager.singalton.GetIt3())
        {
            case 0: OnAactivTurel(false, false, false); break;
            case 1: OnAactivTurel(true, false, false); break;
            case 2: OnAactivTurel(true, true, false); break;
            case 3: OnAactivTurel(true, true, true); break;
        }
    }

    void OnAactivTurel(bool b1, bool b2, bool b3)
    {
        foreach(var i in turellev[0].objects)
            i.SetActive(b1);
        foreach (var i in turellev[1].objects)
            i.SetActive(b2);
        foreach (var i in turellev[2].objects)
            i.SetActive(b3);
    }
}

[System.Serializable]
public class GO
{
   public GameObject[] objects;
}