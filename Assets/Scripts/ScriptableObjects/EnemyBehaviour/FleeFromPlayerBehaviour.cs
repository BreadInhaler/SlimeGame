using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/Behaviour/FleeFromPlayer",fileName = "FleeFromPlayer")]
class FleeFromPlayerBehaviour : EnemyBehaviour{
    public float fleeDistance;
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange()==false) return false;

        enemy.MoveAwayFromPlayer(fleeDistance);
        return true;
    }
}