using UnityEngine;
[CreateAssetMenu(menuName = "Behaviour/Chase",fileName = "Chase")]
public class ChaseBehaviour : EnemyBehaviour{
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange() == false) return false;

        enemy.FollowPlayer();
        return true;
    }
}