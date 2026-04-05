using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public static class LookUpResources{
    private static List<Item> allItems = new List<Item>();
    private static List<AbilityData> allAbilityData = new List<AbilityData>();
    private static string itemsPath = "items";
    private static string abilityPath = "Abilities";
    public static void Init() {
        allItems = Resources.LoadAll<Item>(itemsPath).ToList();
        allAbilityData = Resources.LoadAll<AbilityData>(abilityPath).ToList();
    }
    public static Item GetItemById(string id){
        for(int i = 0; i < allItems.Count ; i++){
            if(allItems[i].id == id) return allItems[i];
        }
        Debug.Log("No Item found by "+id+" -id");
        return null;
    }
    public static AbilityData GetAbilityById(string id){
        for(int i = 0; i < allAbilityData.Count ; i++){
            if(allAbilityData[i].id == id){
                //Debug.Log("returning "+allAbilityData[i].id+" ability");
                return allAbilityData[i];
            } 
                
        }
        Debug.Log("No Ability found by "+id+" -id");
        return null;
    }
    public static List<Item> GetAllItems(){
        return allItems;
    }
    public static List<AbilityData> GetAbilityData(){
        return allAbilityData;
    }
}