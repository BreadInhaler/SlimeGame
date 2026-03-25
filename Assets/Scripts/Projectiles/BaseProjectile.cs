using UnityEngine;
public abstract class BaseProjectile : MonoBehaviour{
    protected ProjectileData data;
    protected Vector3 direction;
    protected float timer;
    public void Instantiate(ProjectileData data,Vector3 direction){
        this.data=data;
        this.direction=direction.normalized;
        timer=0f;
    }
    protected virtual void Update(){
        Move();
        Rotate();
        LifeTimeCheck();
    }
    protected virtual void Move(){
        //Debug.Log(data.speed);
        transform.position += direction * data.speed * Time.deltaTime;
    }
    protected virtual void Rotate(){
        return;
    }
    protected virtual void LifeTimeCheck(){
        timer+=Time.deltaTime;
        if(timer>=data.lifeTime){
            Destroy(gameObject);
        }
    }
    protected virtual void OnHit(Collider collider){
        
        if(data.statusEffect != null && collider.GetComponentInParent<Character>() != null)
            data.statusEffect.OnApply(collider.GetComponentInParent<Character>(),data.statusEffect);
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter(Collider collider){
        Debug.Log("hit: " + collider.gameObject.name + " layer: " + collider.gameObject.layer);
        if(Settings.IsInLayerMask(collider.gameObject,data.hitMask))
            OnHit(collider);
    }
}