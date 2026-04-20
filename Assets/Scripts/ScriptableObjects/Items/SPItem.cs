using UnityEngine;
[CreateAssetMenu(menuName = "Item/SPItem" , fileName = "SPItem")]
public class SPItem : Item{
    public float restoreAmount;
    public bool mult;
    public override void Use(Character character){
        Player player = character.GetComponent<Player>(); 
        if(player==null) return;
        player.spHandler.AddAmount(restoreAmount,mult);
    }
}