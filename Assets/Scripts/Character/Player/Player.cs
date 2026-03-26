using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Character{
    //do stuff here
    public CharacterMovement movement;
    private GameObject prefab;
    private AttackHandler baseAttack;
    private AttackHandler airAttack;
    private AttackHandler runAttack;
    [SerializeField] protected AbilityData ability;
    [SerializeField] protected Inventory inventory;
    protected override void Awake(){
        LookUpResources.Init();//will move to another class but keeping it here for now
        //check if save if not load base stats
        InitializeStats(baseStats);
        baseAttack = new AttackHandler(ability.baseAttack);
        airAttack = new AttackHandler(ability.airAttack);
        runAttack = new AttackHandler(ability.runAttack);
    }
    private void ChangeAbility(AbilityData ability){
        if(this.ability == ability) return;
        prefab=ability.prefab;
        this.ability=ability;
    }
    void Update(){
        if(InputSystem.actions.FindAction("attack").WasPerformedThisFrame())
            if(movement.isGrounded == false) airAttack.Execute(this);
            else baseAttack.Execute(this);
    }
    //if(buttonpressed) do ability.baseAttack.use
    //if(buttonpressed && notgrounded) ability.airAttack.use
    //if(buttonpressed && running) ability.runAttack.use
}