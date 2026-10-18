using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLeval : MonoBehaviour
{
    public FishMod[] fishMods;
    public BotFish botFish;
    public HealterBot healterBot;
    public SpriteRenderer sprite;
    public void applayFish(int mod)
    {
        healterBot.AplayHPMax(fishMods[mod].MaxHP + UiManager.Singalton.CiuntSecund);
        botFish.ApplaySpeed(fishMods[mod].speed, fishMods[mod].AdrSpeed);
        sprite.sprite = fishMods[mod].imgFish;
    }
}
[System.Serializable]
public class FishMod
{
    public int speed, AdrSpeed, MaxHP;
    public Sprite imgFish;
}