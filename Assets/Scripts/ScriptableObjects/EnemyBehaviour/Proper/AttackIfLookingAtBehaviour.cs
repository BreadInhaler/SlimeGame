using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/Behaviour/AttackIfLockingAt" , fileName = "AttackIfLockingAt")]
class AttackIfLookingAtBehaviour : EnemyBehaviour{
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange() == false)return false;
        Vector3 dirToPlayer = (enemy.player.transform.position - enemy.transform.position).normalized;
        float angle = Vector3.Angle(enemy.transform.forward, dirToPlayer);

        if (angle <= 5f) enemy.AttackWithArc();
        else return false;
        return true;
    }
}