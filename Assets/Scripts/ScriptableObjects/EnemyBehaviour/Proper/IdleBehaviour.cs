using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/Behaviour/Idle",fileName = "Idle")]
public class IdleBehaviour : EnemyBehaviour{
    public bool stopMovement = false;
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange() == true) return false;
        if(stopMovement) enemy.agent.ResetPath();
        return true;
    }
}