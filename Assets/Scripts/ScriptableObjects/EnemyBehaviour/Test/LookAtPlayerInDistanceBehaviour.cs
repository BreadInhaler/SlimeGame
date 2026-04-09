using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/Behaviour/LookAtPlayer" , fileName = "LookAtPlayer")]
class LookAtPlayerInDistanceBehaviour : EnemyBehaviour{
    public float distanceModifier=2f;
    public bool shortDistance=true;
    public float rotationSpeed;
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange()==false) return false;
        float distance = Vector3.Distance(enemy.transform.position,enemy.player.transform.position);
        float miniDistance = enemy.GetComponentInChildren<SphereCollider>().radius/distanceModifier;
        if(shortDistance) if(distance>miniDistance) return false;
        if(shortDistance==false) if(distance<miniDistance) return false;
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