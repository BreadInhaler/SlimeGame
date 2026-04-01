using UnityEngine;

[System.Serializable]
public class HUDData{
    [Range(0,1)]
    public float playerHP = 1f;
    [Range(0,1)]
    public float playerSP = 1f;

    public string playerAbility = "";
    public string playerMemory = "";

    //[Range(0,1)]
    //public float enemyHP = 1f;

    public Item[] items = new Item[3];
}