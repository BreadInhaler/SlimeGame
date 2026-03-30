using UnityEngine;

public class ProjectileEmitter : MonoBehaviour{
    public ProjectileData projectileData;
    public StatusEffectSO statusEffect;
    public GameObject ProjectilesParentObject;
    public void Setup(ProjectileData data,StatusEffectSO statusEffect){
        projectileData = data;
        this.statusEffect = statusEffect;
    }
    public void Fire(){
        GameObject obj;
        ProjectilesParentObject = GameObject.Find("ProjectilesParentObject");
        if(projectileData == null) Debug.Log("null check");
        obj = Instantiate(
            projectileData.prefab,
            transform.position,
            Quaternion.LookRotation(transform.forward),
            ProjectilesParentObject.transform
        );
        Debug.Log(obj);
        BaseProjectile projectile = obj.GetComponent<BaseProjectile>();
        if(statusEffect==null) projectile.Instantiate(projectileData,transform.forward);
        else projectile.Instantiate(projectileData,transform.forward,statusEffect.CreateEffectInstance());
    }
}