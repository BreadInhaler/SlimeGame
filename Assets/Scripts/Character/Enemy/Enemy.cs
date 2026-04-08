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
}
