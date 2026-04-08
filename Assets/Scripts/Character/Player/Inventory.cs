using UnityEngine;
using System.Collections.Generic;
public class InventorySlot{
    public Item item;
    public int quantity=0;
}
public class Inventory{
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public bool UseItem(Item item,Character character){
        int pos;
        pos = GetItemPosition(item);
        inventorySlots[pos].item.Use(character);
        inventorySlots[pos].quantity--;
        if(CheckQuantity(inventorySlots[pos])) {
            RemoveItem(item);
            return true;
        }
        return false;
    }
    public bool UseItemAtPos(int pos,Character character){
        inventorySlots[pos].item.Use(character);
        inventorySlots[pos].quantity--;
        if(CheckQuantity(inventorySlots[pos])){
            RemoveItem(inventorySlots[pos].item);
            return true;
        } 
        return false;
    }
    public void AddItem(Item item , int quantity = 1){
        if(HasItemInInventory(item)) inventorySlots[GetItemPosition(item)].quantity+=quantity;
        else inventorySlots.Add(new InventorySlot{ item=item , quantity=quantity});
    }
    public void AddItem(InventorySlot slot){
        if(HasItemInInventory(slot.item)) inventorySlots[GetItemPosition(slot.item)].quantity+=slot.quantity;
        else inventorySlots.Add(new InventorySlot{ item=slot.item , quantity=slot.quantity});
    }
    public void RemoveItem(Item item){
        if(HasItemInInventory(item)) inventorySlots.RemoveAt(GetItemPosition(item));
    }
    public Item GetItemInInventory(Item item){
        for(int i=0;i<inventorySlots.Count;i++){
            if(inventorySlots[i].item == item) return item;
        }
        return null;
    }
    public Item GetItemInInventory(int pos){
        if(inventorySlots.Count >= pos) return inventorySlots[pos].item;
        return null;
    }
    public bool HasItemInInventory(Item item){
        return GetItemPosition(item) != -1;
    }
    public int GetItemPosition(Item item){
        for(int i=0;i<inventorySlots.Count;i++){
            if(inventorySlots[i].item == item) return i;
        }
        return -1;
    }
    public bool CheckQuantity(InventorySlot slot){
        if(slot.quantity==0) return true;
        else return false;
    }
    public List<InventorySlot> GetAllItems(){
        return inventorySlots;
    }
}