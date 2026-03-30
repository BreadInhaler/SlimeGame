using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Character{
    private InputAction attackInput;
    //do stuff here
    public CharacterMovement movement;
    private GameObject prefab;
    private AttackHandler baseAttack;
    private AttackHandler airAttack;
    private AttackHandler runAttack;
    [SerializeField] protected AbilityData ability;
    [SerializeField] protected Inventory inventory;
    protected override void Awake(){
        attackInput= InputSystem.actions.FindAction("attack");
        LookUpResources.Init();//will move to another class but keeping it here for now
        //check if save if not load base stats
        InitializeStats(baseStats);
        ChangeAttacks();
    }
    private void ChangeAbility(AbilityData ability){
        if(this.ability == ability) return;
        prefab=ability.prefab;
        this.ability=ability;
        ChangeAttacks();
    }
    private void ChangeAttacks(){
        baseAttack = new AttackHandler(ability.baseAttack);
        airAttack = new AttackHandler(ability.airAttack);
        runAttack = new AttackHandler(ability.runAttack);
    }
    protected void Update(){
        base.Update();
        TickAttacks();
        if(movement.isGrounded == false) HandleAttack(airAttack);
        else HandleAttack(baseAttack);
        
        StopAttacks();
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