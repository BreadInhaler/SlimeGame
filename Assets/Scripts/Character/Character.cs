using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour{
    [SerializeField] protected Stats stats;
    [SerializeField] protected StatsSO baseStats;
    private List<StatusEffectInstance> statusEffects = new List<StatusEffectInstance>();
    protected virtual void Awake() {
        InitializeStats(baseStats);
    }
    private void Update(){
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
        float finalDamage = damage-currentStats.defense;//later add other stats relevant to calculation ex. item stats
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
    private void Die(){
        return;//later add the actuall behaviour
    }
}