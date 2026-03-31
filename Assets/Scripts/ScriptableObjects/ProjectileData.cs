using UnityEngine;
[CreateAssetMenu(menuName = "Projectile/ProjectileData",fileName = "ProjectileData")]
public class ProjectileData : ScriptableObject{
    [Header("Visual")]
    public GameObject prefab;
    [Header("Stats")]
    public string id;
    public float damage;
    public float speed;
    public float lifeTime;
    [Header("Mask")]
    public LayerMask hitMask;
}