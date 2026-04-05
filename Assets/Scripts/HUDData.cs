using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HUDData{
    [Range(0,1)]
    public float playerHP = 1f;
    [Range(0,1)]
    public float playerSP = 1f;

    public string playerAbility = "";
    public Sprite playerAbilityIcon;
    public string playerMemory = "";
    public Sprite playerMemoryIcon;

    //[Range(0,1)]
    //public float enemyHP = 1f;

    public List<InventorySlot> items = new List<InventorySlot>();
}