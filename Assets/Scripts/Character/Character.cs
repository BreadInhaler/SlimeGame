using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour{
    [SerializeField] protected Stats stats;
    [SerializeField] protected StatsSO baseStats;
    private List<StatusEffectInstance> statusEffects = new List<StatusEffectInstance>();
    protected virtual void Awake() {
        InitializeStats(baseStats);
    }
    protected void Update(){
        TickStatusEffects(Time.deltaTime);
    }
    protected void InitializeStats(StatsSO stats){
        this.stats=new Stats(stats);
        Debug.Log(gameObject.name+"'s stats initialized");
    }
    public void RecieveHeal(float healAmount,bool mult){
        if(mult) stats.hp+=stats.maxHP*healAmount;
        else stats.hp+=healAmount;
        if (stats.hp>stats.maxHP) stats.hp=stats.maxHP;
    }
    public void TakeDamage(float damage){
        Stats currentStats = RefreshStats();
        print(gameObject.name+" is taking "+damage+" damage - "+currentStats.defense+" defense");
        float finalDamage = damage-currentStats.defense;//later add other stats relevant to calculation ex. item stats
        print(gameObject.name+" is taking "+finalDamage+" final damage");
        if(finalDamage > 0)
            stats.hp-=finalDamage;
        if(stats.hp <= 0)
            Die();
    }
    public void TickStatusEffects(float deltaTime){
        List<StatusEffectInstance> toTick = new List<StatusEffectInstance>(statusEffects);
        foreach(StatusEffectInstance effect in toTick){
            effect.elapsedTime+=deltaTime;
            effect.tickTimer+=deltaTime;

            if(effect.tickTimer >= effect.effectData.tickInterval){
                effect.tickTimer-=effect.effectData.tickInterval;
                effect.OnTick(this);
            }
            if(effect.effectData.duration <= effect.elapsedTime){
                effect.OnRemove(this);
            }
        }
    }
    public Stats RefreshStats(){
        Stats current=stats.CloneStats();
        foreach(StatusEffectInstance effect in statusEffects){
            effect.ModifyStats(current);
        }
        print(gameObject.name+" 's defense: "+current.defense);
        return current;
    }
    public void ApplyStatusEffect(StatusEffectInstance effect){
        statusEffects.Add(effect);
        print(gameObject.name+" recieved "+effect.effectData.id);
    }
    public void RemoveStatusEffect(StatusEffectInstance effect){
        statusEffects.Remove(effect);
    }
    public void RemoveAllStatusEffects(){
        List<StatusEffectInstance> toRemove = new List<StatusEffectInstance>(statusEffects);
        foreach(StatusEffectInstance effect in toRemove){
            effect.OnRemove(this);
        }
        statusEffects.Clear();
    }
    public List<StatusEffectInstance> GetStatusEffects(){
        return this.statusEffects;
    }
    public Stats GetStats(){
        return stats;
    }
    public Stats GetModifiedStats(){
        return RefreshStats();
    }
    private void Die(){
        Destroy(this.gameObject);
        return;//later add the actuall behaviour
    }

#if UNITY_EDITOR

private void OnDrawGizmos(){
    if(statusEffects == null || statusEffects.Count == 0) return;
    
    string label = gameObject.name + "\n";
    foreach(StatusEffectInstance effect in statusEffects){
        label += $"{effect.effectData.id} | stacks:{effect.stacks} | time:{effect.elapsedTime:F1}/{effect.effectData.duration}\n";
    }
    
    Handles.Label(transform.position + Vector3.up * 2f, label);
}
#endif
/*

The `#if UNITY_EDITOR` wrapper is important — it means this code is completely stripped out in builds so it has zero performance impact in your actual game.

You'll see it floating above each enemy in the Scene view like:

Goblin
FireEffect | stacks:2 | time:3.2/10.0
DefenseBreak | stacks:1 | time:1.5/10.0
*/
}