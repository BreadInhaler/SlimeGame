using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/Behaviour/LookAtPlayer" , fileName = "LookAtPlayer")]
class LookAtPlayerBehaviour : EnemyBehaviour{
    public float minDistance=10f;
    public float distanceSize=2f;
    public bool shortDistance=true;
    public float rotationSpeed;
    public override bool Execute(Enemy enemy){
        if(enemy.TargetInRange()==false) return false;
        float distance = Vector3.Distance(enemy.transform.position,enemy.player.transform.position);
        if(minDistance==0 && distanceSize>0){
            float miniDistance = enemy.GetComponentInChildren<SphereCollider>().radius/distanceSize;
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
        }else{
            if(shortDistance) if(distance>minDistance) return false;
            if(shortDistance==false) if(distance<minDistance) return false;
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