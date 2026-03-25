using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Player/Attack" , fileName = "Attack")]
public class Attack : ScriptableObject{
    public string id;
    public float damage;
    public StatusEffectSO statusEffect;
    public ProjectileData projectileData;
    public int numEmmitters = 1;
    public float offset;
    public float archDir;
    public GameObject prefab;
    
}