using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Enemy : Character{
    //---------------------------------------------Navigation----------------------------------
    [HideInInspector]public NavMeshAgent agent;
    private float pathUpdateTimer = 0f;
    private float pathUpdateInterval = 0.2f;
    //---------------------------------------------OtherStuff----------------------------------
    [HideInInspector]public Player player;
    public List<EnemyBehaviour> enemyBehaviours = new List<EnemyBehaviour>();
    public List<Attack> attacks = new List<Attack>();
    private List<AttackHandler> attackHandlers = new List<AttackHandler>();
    //---------------------------------------------Overrides----------------------------------
    protected override void Awake(){
        base.Awake();
        agent=gameObject.GetComponent<NavMeshAgent>();
        agent.speed=RefreshStats().speed;
        FillAttacks();
    }
    public override void TakeDamage(float damage){
        base.TakeDamage(damage);
        Globals.lastHitEnemy=this;
    }
    protected override void Update(){
        base.Update();
        pathUpdateTimer += Time.deltaTime;
        for(int i=0;i<enemyBehaviours.Count;i++){
            if(enemyBehaviours[i].Execute(this)) break;
        }
    }
    //---------------------------------------------PlayerTargetFuncions----------------------------------
    public bool TargetInRange(){
        return player!=null;
    }
    public void FollowPlayer(){
        if(pathUpdateTimer >= pathUpdateInterval){
            pathUpdateTimer = 0f;
            agent.SetDestination(player.transform.position);
        }
    }
    public void MoveAwayFromPlayer(float fleeDistance){
        if(pathUpdateTimer >= pathUpdateInterval){
            pathUpdateTimer = 0f;
            Vector3 directionAway = (transform.position - player.transform.position).normalized;
            Vector3 desiredPosition = transform.position + directionAway * fleeDistance;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(desiredPosition, out hit, 5f, NavMesh.AllAreas)) agent.SetDestination(hit.position);
        }
    }
    //---------------------------------------------OtherStuff----------------------------------
    private void FillAttacks(){
        for(int i=0;i<attacks.Count;i++){
            attackHandlers.Add(new AttackHandler(attacks[i]));
        }
    }
    public void Attack(){
        for(int i = 0;i<attackHandlers.Count;i++){
            attackHandlers[i].Execute(this);
        }
    }
    public void AttackWithArc(){
        ProjectileData pData = attackHandlers[0].data.projectileData;
        if(pData.id == "test_arc_aim"){
            float launchAngle = GetLaunchAngle(transform.position, player.transform.position, pData.speed);
            Debug.Log("launchAngle: " + launchAngle);
            Debug.Log("distance: " + Vector3.Distance(transform.position, player.transform.position));
            Debug.Log("speed: " + pData.speed);
            Vector3 toTarget = player.transform.position - transform.position;
            toTarget.y = 0;
            Vector3 emitterPos = transform.position + toTarget.normalized * attackHandlers[0].data.offset + Vector3.up * 0.5f;
        
            launchAngle = GetLaunchAngle(emitterPos, player.transform.position, pData.speed);
            attackHandlers[0].Execute(this, -launchAngle, player.transform);
            attackHandlers[0].Execute(this, -launchAngle, player.transform);
        }
    }
    float GetLaunchAngle(Vector3 origin, Vector3 target, float speed){
        Vector3 toTarget = target - origin;
        float x = new Vector2(toTarget.x, toTarget.z).magnitude;
        float y = toTarget.y;
        float g = Mathf.Abs(Physics.gravity.y); // use positive 9.81
        float v = speed;

        float discriminant = (v * v * v * v) - g * (g * x * x + 2 * y * v * v);

        if (discriminant < 0){
            Debug.LogWarning("Target out of range!");
            return 0f;
        }

        float angle = Mathf.Atan2(v * v - Mathf.Sqrt(discriminant), g * x);
        return angle * Mathf.Rad2Deg;
    }
}
