using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSpript : MonoBehaviour
{
    Animator _Aim;

    private void Awake() => _Aim = gameObject.GetComponent<Animator>();


    public void Attak()
    {
        _Aim.SetTrigger("Attak");
    }
}
