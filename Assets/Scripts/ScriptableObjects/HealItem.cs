using UnityEngine;
[CreateAssetMenu(menuName = "Item/HealItem" , fileName = "HealItem")]
public class HealItem : Item{
    public float healAmount;
    public bool mult=false;
    public override void Use(Character character){
        character.RecieveHeal(healAmount,mult);
    }
}