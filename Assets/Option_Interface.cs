using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Option_Interface : MonoBehaviour
{
    public RectTransform Musics;
    public bool isMusic, isPlayMusic, IsModeMove;
    public Image PlayBuutan, ModeMove;
    public Sprite play, stop;
    public Sprite PCCursor, ModilJost;

    public Slider music, sound;

    public AudioSource audioSource;
    public AudioSource[] GameSource;

    private void Start()
    {
        music.value = SaveMenager.singalton.GetMusic();
        sound.value = SaveMenager.singalton.GetSound();
        IsModeMove = SaveMenager.singalton.GetModMove();
        music.onValueChanged.AddListener(UpdateMusicValue);
        foreach (var i in GameSource)
           i.volume = sound.value;     
        audioSource.volume = music.value;

        ModeMoveUI();
    }

    public void OpenMusic()
    {
        Musics.DOAnchorPos(new Vector2(!isMusic ? 0 : -314, 186), 0.5f).SetEase(Ease.OutSine).From(new Vector2(!isMusic ? -314 : 0, 186));
        isMusic = !isMusic;
    }

    public void PlayMusic()
    {

        if (isPlayMusic)
        {
            audioSource.Stop();
            PlayBuutan.sprite = play;
            PlayBuutan.rectTransform.eulerAngles= new Vector3(0,0,90);
        }
        else
        {
            audioSource.Play();
            PlayBuutan.sprite = stop;
            PlayBuutan.rectTransform.eulerAngles = new Vector3(0, 0, 0);
        }
        isPlayMusic = !isPlayMusic;
    }

    public void SaveVaulem()
    {
        SaveMenager.singalton.SetSound(sound.value);
        SaveMenager.singalton.SetMusic(music.value);
        foreach (var i in GameSource)
            i.volume = sound.value;
        audioSource.volume = music.value;
    }
    public void UpdateMusicValue(float value) => audioSource.volume = value;  

    public void ModeMoveNext()
    {
        IsModeMove = !IsModeMove;
        ModeMoveUI();
    }

    public void ModeMoveUI()
    {
        if (IsModeMove)
            ModeMove.sprite = ModilJost;
        else
            ModeMove.sprite = PCCursor;
    }
}
