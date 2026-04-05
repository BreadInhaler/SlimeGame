using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;
public class Player : Character{
    //----------------------------------------------------------HUD------------------------------------------
    public Sprite emptyAbilityIcon;
    private HUDData hudData;
    public HUDHandler hudHandler;
    //----------------------------------------------------------Input------------------------------------------
    private InputAction attackInput;
    private InputAction itemInput;
    private InputAction prevItem;
    private InputAction nextItem;
    private InputAction memoryInput;
    public int itemInputInt=0;
    //----------------------------------------------------------Other Stuff------------------------------------------
    //do stuff here
    public CharacterMovement movement;
    private AttackHandler baseAttack;
    private AttackHandler airAttack;
    private AttackHandler runAttack;
    [SerializeField] protected AbilityData ability;
    [SerializeField] protected AbilityData memoryAbility;
    [SerializeField] protected Inventory inventory;
    private List<Item> activeItems = new List<Item>();
    //----------------------------------------------------------Unity------------------------------------------
    protected override void Awake(){
        attackInput= InputSystem.actions.FindAction("attack");
        itemInput = InputSystem.actions.FindAction("UseItem");
        prevItem = InputSystem.actions.FindAction("PrevItem");
        nextItem = InputSystem.actions.FindAction("NextItem");
        memoryInput = InputSystem.actions.FindAction("Memory");

        hudData = new HUDData();
        inventory = new Inventory();

        LookUpResources.Init();//will move to another class but keeping it here for now
        //check if save if not load base stats
        InitializeStats(baseStats);

        FillInventory();
        InitializeAbility();
    }
    protected override void Update(){
        base.Update();
        TickAttacks();
        if(movement.isGrounded == false) HandleAttack(airAttack);
        else HandleAttack(baseAttack);
        if(memoryInput.WasPerformedThisFrame()) ChangeAbility(memoryAbility);
        
        StopAttacks();
        if(attackInput.WasPerformedThisFrame()) this.TakeDamage(5);
        List<InventorySlot> items = this.inventory.GetAllItems();
        if(itemInput.WasPerformedThisFrame()) {
            if(items.Count == 0){
                UpdateInventoryUI();
                return;
            } 
            Debug.Log(items[itemInputInt].item.id+" used");
            Item item = items[itemInputInt].item;
            if(inventory.UseItem(item,this)){
                UpdateInventoryUI();
            } 
        }
        CycleItem();
    }
    //----------------------------------------------------------Override------------------------------------------
    public override void TakeDamage(float damage){
        base.TakeDamage(damage);
        hudData.playerHP = stats.hp/stats.maxHP;
        hudHandler.UpdateUI(hudData);
    }
    public override void RecieveHeal(float healAmount, bool mult){
        base.RecieveHeal(healAmount, mult);
        hudData.playerHP = stats.hp/stats.maxHP;
        hudHandler.UpdateUI(hudData);
    }
    //----------------------------------------------------------Abilities------------------------------------------
    private void ChangeAbility(AbilityData ability){
        //if(this.ability == ability) return;
        GameObject playerPrefab = GameObject.Find(this.ability.prefab.name+"(Clone)");
        //if(ability.prefab == GameObject.Find(this.ability.prefab.name)) print("checked yes");
        if(playerPrefab) Destroy(playerPrefab);  
        Instantiate(ability.prefab,this.transform.position,this.transform.rotation,this.transform);
        this.ability=ability;
        hudData.playerAbilityIcon = ability.icon;
        hudData.playerAbility = ability.id;
        if(memoryAbility!=null){
            hudData.playerMemory = memoryAbility.id;
            hudData.playerMemoryIcon = memoryAbility.icon;
        }
        ChangeAttacks();
        hudHandler.UpdateUI(hudData);
    }
    private void InitializeAbility(){
        this.ability=LookUpResources.GetAbilityById("base_ability");
        hudData.playerAbilityIcon = ability.icon;
        hudData.playerAbility = ability.id;
        if(memoryAbility!=null){
            hudData.playerMemory = memoryAbility.id;
            hudData.playerMemoryIcon = memoryAbility.icon;
        }
        ChangeAttacks();
        hudHandler.UpdateUI(hudData);
        Instantiate(ability.prefab,this.transform.position,this.transform.rotation,this.transform);
    }
    private void UseMemoryAbility(){
        if(memoryAbility!=null) {
            ChangeAbility(memoryAbility);
            memoryAbility=null;
        }
        hudData.playerAbility = ability.id;
        if(memoryAbility!=null){
            hudData.playerMemory = memoryAbility.id;
            hudData.playerMemoryIcon = memoryAbility.icon;
        }else{
            hudData.playerMemory = "empty";
            hudData.playerMemoryIcon = emptyAbilityIcon;
        }
    }
    //----------------------------------------------------------Attacks------------------------------------------
    private void ChangeAttacks(){
        baseAttack = new AttackHandler(ability.baseAttack);
        airAttack = new AttackHandler(ability.airAttack);
        runAttack = new AttackHandler(ability.runAttack);
    }
    private bool IsAttackSustained(AttackHandler attack){
        return attack.GetIsSustained();
    }
    private void HandleAttack(AttackHandler attack){
        if(attackInput.WasPerformedThisFrame()) attack.Execute(this);
        if(attackInput.WasReleasedThisFrame()) attack.StopTicking();
    }
    private void StopAttacks(){
        if(attackInput.WasReleasedThisFrame()) baseAttack.StopTicking();
        if(attackInput.WasReleasedThisFrame()) airAttack.StopTicking();
        if(attackInput.WasReleasedThisFrame()) runAttack.StopTicking();
    }
    private void TickAttacks(){
        if(IsAttackSustained(baseAttack)) baseAttack.Tick(Time.deltaTime);
        if(IsAttackSustained(airAttack)) airAttack.Tick(Time.deltaTime);
        if(IsAttackSustained(runAttack)) runAttack.Tick(Time.deltaTime);
    }
    //if(buttonpressed) do ability.baseAttack.use
    //if(buttonpressed && notgrounded) ability.airAttack.use
    //if(buttonpressed && running) ability.runAttack.use
    //----------------------------------------------------------Items------------------------------------------
    private int NextItem(){
        if(inventory.GetAllItems().Count-1 == itemInputInt) return 0;
        else return (itemInputInt+1);
    }
    private int PrevItem(){
        if(itemInputInt == 0) return inventory.GetAllItems().Count-1;
        else return (itemInputInt-1);
    }
    private void CycleItem(){
        if(prevItem.WasPerformedThisFrame()){
            itemInputInt=PrevItem();
            UpdateInventoryUI();
        }
        if(nextItem.WasPerformedThisFrame()){
            itemInputInt=NextItem();
            UpdateInventoryUI();
        } 
    }
    private void UpdateInventoryUI(){
        InventorySlot[] equippedItems = new InventorySlot[3];
        List<InventorySlot> inventorySlots = this.inventory.GetAllItems();
        //Debug.Log("all inventoryslots count -> "+inventorySlots.Count);
        if(inventorySlots.Count<=0){
            hudData.items=null;
            //Debug.Log("set huddata items to null and returned");
            //Debug.Log("huddata items -> "+(hudData.items==null));
            hudHandler.UpdateUI(hudData);
            return;
        }
        equippedItems[0] =  inventorySlots[PrevItem()];
        equippedItems[1] =  inventorySlots[itemInputInt];
        equippedItems[2] =  inventorySlots[NextItem()];
        hudData.items = equippedItems.ToList();
        hudHandler.UpdateUI(hudData);
    }
    private void FillInventory(){
        Item basicHealItem = LookUpResources.GetItemById("basic_heal");
        Item superHealItem = LookUpResources.GetItemById("super_heal");
        Item maxHealItem = LookUpResources.GetItemById("max_heal");
        Item defenseBuffItem = LookUpResources.GetItemById("defense_effect_item");
        inventory.AddItem(basicHealItem,1);
        inventory.AddItem(superHealItem,1);
        inventory.AddItem(maxHealItem,1);
        inventory.AddItem(defenseBuffItem,1);
        UpdateInventoryUI();
        //print(basicHealItem);
        //hudData.items.Add(activeItems[0].id);
        //hudData.items.Add(activeItems[1].id);
        //hudData.items.Add(activeItems[2].id);
    }
}