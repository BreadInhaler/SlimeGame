using UnityEngine;
public abstract class StatusEffectSO : ScriptableObject {
    public string id;
    public float effectStrength;//damage/modifier for stats
    public float duration=10f;
    public float tickInterval=1f;
    public bool stackable=false;//false by default
    public abstract StatusEffectInstance CreateEffectInstance();
}