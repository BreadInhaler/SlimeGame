using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/Behaviour/ChasePlayer",fileName = "ChasePlayer")]
public class ChasePlayerBehaviour : EnemyBehaviour{
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange() == false) return false;

        enemy.FollowPlayer();
        return true;
    }
}