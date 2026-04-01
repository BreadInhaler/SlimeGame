using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class SaveData{
    public PlayerStatsSaveData playerStats;
    public InventorySaveData inventorySaveData;
}   
public class PlayerExtraSaveData{
    public string abilityId;
}
public class PlayerStatsSaveData{
    public float hp;
    public float maxHP;
    public float speed;
    public float defense;
}
public class InventorySaveData{
    public List<InventorySlotSaveData> inventorySlots;
}
public class InventorySlotSaveData{
    public string itemId;
    public int quantity;
}
public static class SaveSystem{
    #if UNITY_EDITOR
        private static string absPath=Application.dataPath+"/../";
    #else
        private static string absPath=Application.persistentDataPath;
    #endif
    private static string savePath=absPath + "/Saves/saveFile";
    private static string vendorPath=absPath + "/Vendors/";

    public static void SaveGame(SaveData data, int pathNumber){
        Directory.CreateDirectory(Path.GetDirectoryName(savePath+pathNumber+".json"));
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath+pathNumber+".json",json);
        Debug.Log("Game saved to -> "+savePath+pathNumber+".json");
    }
    public static SaveData LoadGame(int pathNumber){
        if(File.Exists(savePath+pathNumber+".json")){
           string json = File.ReadAllText(savePath+pathNumber+".json");
           SaveData data = JsonUtility.FromJson<SaveData>(json);
           Debug.Log("Game loaded from -> "+savePath+pathNumber+".json");
           return data;
        }else{
            Debug.LogWarning("Save file not found at: " + savePath+pathNumber+".json");
            return null;
        }
    }
}