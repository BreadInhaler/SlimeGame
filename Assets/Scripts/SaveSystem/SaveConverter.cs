using System.Collections.Generic;
public static class SaveConverter{
    public static InventorySaveData ConvertInventoryToInventorySaveData(Inventory inventory){
        InventorySaveData data = new InventorySaveData();
        List<InventorySlot> inventorySlots = inventory.GetAllItems();
        foreach(InventorySlot slot in inventorySlots){
            InventorySlotSaveData slotData = new InventorySlotSaveData();
            slotData.itemId = slot.item.id;
            slotData.quantity = slot.quantity;
            data.inventorySlots.Add(slotData);
        }
        return data;
    }
    public static Inventory ConvertInvetorySaveDataToInventory(InventorySaveData saveData){
        Inventory inventory = new Inventory();
        foreach(InventorySlotSaveData data in saveData.inventorySlots){
            InventorySlot slot = new InventorySlot();
            Item item = LookUpResources.GetItemById(data.itemId);
            slot.item = item;
            slot.quantity = data.quantity;
            inventory.AddItem(slot);
        }
        return inventory;
    }
    public static Stats ConvertStatsSaveDataToStats(PlayerStatsSaveData data){
        Stats stats = new Stats();
        stats.hp = data.hp;
        stats.maxHP = data.maxHP;
        stats.defense = data.defense;
        stats.speed = data.speed;
        return stats;
    }
    public static PlayerStatsSaveData ConvertStatsToStatsSaveData(Stats stats){
        PlayerStatsSaveData data = new PlayerStatsSaveData();
        data.hp = stats.hp;
        data.maxHP = stats.maxHP;
        data.defense = stats.defense;
        data.speed = stats.speed;
        return data;
    }
    public static AbilityData ConvertAbilityIdToAbility(string id){
        return LookUpResources.GetAbilityById(id);
    }
}