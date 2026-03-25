using UnityEngine;

public class ProjectileEmitter : MonoBehaviour{
    public ProjectileData projectileData;
    public GameObject ProjectilesParentObject;
    public void Setup(ProjectileData data){
        projectileData = data;
    }
    public void Fire(){
        GameObject obj;
        if(projectileData == null) Debug.Log("null check");
        obj = Instantiate(
            projectileData.prefab,
            transform.position,
            Quaternion.LookRotation(transform.forward)
        );
        Debug.Log(obj);
        BaseProjectile projectile = obj.GetComponent<BaseProjectile>();
        projectile.Instantiate(projectileData,transform.forward);
    }
}