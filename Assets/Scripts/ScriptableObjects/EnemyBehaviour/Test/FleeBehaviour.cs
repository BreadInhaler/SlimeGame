using UnityEngine;
[CreateAssetMenu(menuName = "Behaviour/Flee",fileName = "Flee")]
class FleeBehaviour : EnemyBehaviour{
    public float fleeDistance;
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange()==false) return false;

        enemy.MoveAwayFromPlayer(fleeDistance);
        return true;
    }
}