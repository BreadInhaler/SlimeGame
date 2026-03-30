using UnityEngine;
public abstract class StatusEffectSO : ScriptableObject {
    public string id;
    public float effectStrength;//damage/modifier for stats
    [Header("Time")]
    public float duration=10f;
    public float tickInterval=1f;
    [Header("Stacks")]
    public bool stackable=false;//false by default
    public float maxStacks=1f;
    public abstract StatusEffectInstance CreateEffectInstance();
}