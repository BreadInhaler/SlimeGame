using UnityEngine;
class DetectionTrigger : MonoBehaviour{
    Enemy enemy;
    void Start(){
        enemy=gameObject.GetComponentInParent<Enemy>();
    }
    void OnTriggerEnter(Collider other){
        Player player=other.GetComponent<Player>();
        if(player) enemy.player=player;
    }
    void OnTriggerExit(Collider other){
        Player player=other.GetComponent<Player>();
        if(player) enemy.player=null;
    }
}