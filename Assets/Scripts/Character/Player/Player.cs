using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class Player : Character{
    private HUDData hudData;
    public HUDHandler hudHandler;
    private InputAction attackInput;
    private InputAction itemInput;
    public int itemInputInt=0;
    //do stuff here
    public CharacterMovement movement;
    private AttackHandler baseAttack;
    private AttackHandler airAttack;
    private AttackHandler runAttack;
    [SerializeField] protected AbilityData ability;
    [SerializeField] protected Inventory inventory;
    [SerializeField] private Item[] activeItems = new Item[3];
    protected override void Awake(){
        attackInput= InputSystem.actions.FindAction("attack");
        itemInput = InputSystem.actions.FindAction("UseItem");
        LookUpResources.Init();//will move to another class but keeping it here for now
        //check if save if not load base stats
        InitializeStats(baseStats);
        ChangeAttacks();
        hudData = new HUDData();
        inventory = new Inventory();
        FillInventory();
    }
    private void FillInventory(){
        inventory.AddItem(LookUpResources.GetItemById("basic_heal"),10);
        inventory.AddItem(LookUpResources.GetItemById("super_heal"),5);
        inventory.AddItem(LookUpResources.GetItemById("max_heal"),2);
        activeItems[0] = inventory.SearchItemInInventory(LookUpResources.GetItemById("basic_heal"));
        activeItems[1] = inventory.SearchItemInInventory(LookUpResources.GetItemById("super_heal"));
        activeItems[2] = inventory.SearchItemInInventory(LookUpResources.GetItemById("max_heal"));
        Debug.Log(activeItems);
    }
    public override void TakeDamage(float damage){
        base.TakeDamage(damage);
        hudData.playerHP = stats.hp/stats.maxHP;
        hudData.playerAbility = ability.id;
        hudHandler.UpdateUI(hudData);
    }
    private void ChangeAbility(AbilityData ability){
        if(this.ability == ability) return;
        Instantiate(ability.prefab,this.transform.position,this.transform.rotation,this.transform);
        this.ability=ability;
        ChangeAttacks();
    }
    private void ChangeAttacks(){
        baseAttack = new AttackHandler(ability.baseAttack);
        airAttack = new AttackHandler(ability.airAttack);
        runAttack = new AttackHandler(ability.runAttack);
    }
    protected override void Update(){
        base.Update();
        TickAttacks();
        if(movement.isGrounded == false) HandleAttack(airAttack);
        else HandleAttack(baseAttack);
        
        StopAttacks();
        if(attackInput.WasPerformedThisFrame()) this.TakeDamage(5);
        List<InventorySlot> items = this.inventory.GetAllItems();
        if(itemInput.WasPerformedThisFrame()) {
            this.inventory.UseItem(items[itemInputInt].item,this);
            Debug.Log("heal item used");
            hudData.playerHP = stats.hp/stats.maxHP;
            hudData.playerAbility = ability.id;
            hudHandler.UpdateUI(hudData);
        }
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
}