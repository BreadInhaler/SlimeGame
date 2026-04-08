using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/Behaviour/AttackPlayer",fileName = "AttackPlayer")]
class AttackPlayerBehaviour : EnemyBehaviour{
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange() == false) return false;
        enemy.Attack();
        return true;
    }
}