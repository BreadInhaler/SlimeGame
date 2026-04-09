using UnityEngine;
[CreateAssetMenu(menuName = "BehaviourConditional/LookIfDistance",fileName = "LookIfDistance")]
public class LookIfDistanceBehaviour : EnemyBehaviour{
    public float distanceModifier;
    public bool higher=true;
    [HideInInspector]public float rotationSpeed = 200f;
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange() == false) return false;

        float distance = Vector3.Distance(enemy.transform.position,enemy.player.transform.position);
        float modifiedDistance = enemy.GetComponentInChildren<SphereCollider>().radius/distanceModifier;
        if(higher==true){
            if(distance<modifiedDistance) return false;
            enemy.agent.ResetPath();
            Vector3 direction = enemy.player.transform.position - enemy.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            enemy.transform.rotation = Quaternion.RotateTowards(
                enemy.transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            return true;
        }else{
            if(distance>modifiedDistance) return false;
            enemy.agent.ResetPath();
            Vector3 direction = enemy.player.transform.position - enemy.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            enemy.transform.rotation = Quaternion.RotateTowards(
                enemy.transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            return true;
        }
    }
}