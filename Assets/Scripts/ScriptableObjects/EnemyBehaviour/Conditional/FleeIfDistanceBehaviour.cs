using UnityEngine;
[CreateAssetMenu(menuName = "BehaviourConditional/FleeIfDistance",fileName = "FleeIfDistance")]
public class FleeIfDistanceBehaviour : EnemyBehaviour{
    public float distanceModifier;
    public bool higher=true;
    public float fleeDistance;
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange() == false) return false;

        float distance = Vector3.Distance(enemy.transform.position,enemy.player.transform.position);
        float modifiedDistance = enemy.GetComponentInChildren<SphereCollider>().radius/distanceModifier;
        if(higher==true){
            if(distance<modifiedDistance) return false;
            enemy.MoveAwayFromPlayer(fleeDistance);
            return true;
        }else{
            if(distance>modifiedDistance) return false;
            enemy.MoveAwayFromPlayer(fleeDistance);
            return true;
        }
    }
}