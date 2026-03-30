using UnityEngine;
[CreateAssetMenu(menuName = "Player/Attack" , fileName = "Attack")]
public class Attack : ScriptableObject{
    public string id;
    public float damage;
    public bool isSustained = false;
    public float fireRate = 0;
    public StatusEffectSO statusEffect;
    public ProjectileData projectileData;
    public int numEmmitters = 1;
    public float offset;
    public float archDir;
    public GameObject prefab;
}