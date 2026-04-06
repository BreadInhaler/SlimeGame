using UnityEngine;

public class ItemDrop : MonoBehaviour{
    public Item item;
    public int quantity=1;
    public bool giveItem;
    public int money;
    void Start() {
        //Instantiate(new InventorySlot{item=this.item,quantity=this.quantity});
        Instantiate(10);
    }
    public void Instantiate(InventorySlot inventorySlot){
        this.item=inventorySlot.item;
        this.quantity=inventorySlot.quantity;

        if (!Application.isPlaying) return; // stops it running in Editor
        
        if (item != null) {
            MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
            mr.material = new Material(mr.sharedMaterial);
            mr.material.mainTexture = item.icon.texture;
        }
    }
    public void Instantiate(int money){
        this.money=money;
        if (money>0) {
            MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
            mr.material = new Material(mr.sharedMaterial);
            mr.material.mainTexture = Globals.moneyIcon.texture;
        }
    }
    void OnTriggerEnter(Collider other){
        Player player = other.GetComponent<Player>();
        if(player==null) return;
        if(item!=null){
            player.PickUpItem(new InventorySlot{item=item,quantity=quantity});
            Destroy(gameObject);
            return;
        }
        if(money>0) player.wallet.AddAmount(money);
        Destroy(gameObject);
    }
}