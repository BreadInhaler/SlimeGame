using UnityEngine;

public class BasicProjectile : BaseProjectile{
    protected override void OnTriggerEnter(Collider collider){
        if(Settings.IsInLayerMask(collider.gameObject,data.hitMask) == true){
            OnHit(collider);
        }
    }
}
