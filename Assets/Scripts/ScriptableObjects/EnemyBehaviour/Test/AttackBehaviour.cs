using UnityEngine;
[CreateAssetMenu(menuName = "Behaviour/Attack",fileName = "Attack")]
class AttackBehaviour : EnemyBehaviour{
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange() == false) return false;
        enemy.AttackWithArc();
        return true;
    }
}