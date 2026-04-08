using UnityEngine;
public abstract class StatusEffectInstance{
    public StatusEffectSO effectData;
    public float tickTimer;
    public float elapsedTime;
    public int stacks=1;

    public StatusEffectInstance(StatusEffectSO statusEffectSO){this.effectData = statusEffectSO;}

    public virtual void OnApply(Character character, StatusEffectInstance newEffect){
        foreach(StatusEffectInstance statusEffect in character.GetStatusEffects()){
            if(statusEffect.effectData.id == newEffect.effectData.id){
                if(statusEffect.effectData.stackable)
                    statusEffect.stacks++;
                statusEffect.elapsedTime = 0f;
                statusEffect.tickTimer = 0f;
                return;
            }
        }
        character.ApplyStatusEffect(newEffect);
        newEffect.elapsedTime = 0f;
        newEffect.tickTimer = 0f;
    }
    public virtual void OnTick(Character character){return;}//do Nothing by default in case of not being a damaging effect
    public virtual void OnRemove(Character character){character.RemoveStatusEffect(this);}
    public virtual void ModifyStats(Stats stats){return;}//do Nothing by default in case of not being a stat modifying effect
}
public class FireEffect : StatusEffectInstance{
    public FireEffect(StatusEffectSO statusEffectSO) : base(statusEffectSO){}

    public override void OnTick(Character character){
        if(effectData.stackable)
            character.TakeDamage(effectData.effectStrength*stacks);
        else
            character.TakeDamage(effectData.effectStrength);
    }
}
public class DefenseBreakEffect : StatusEffectInstance{
    public DefenseBreakEffect(StatusEffectSO statusEffectSO) : base(statusEffectSO){}

    public override void ModifyStats(Stats stats){
        if(effectData.stackable)
            stats.defense=stats.defense-(effectData.effectStrength*stacks);
        else
            stats.defense=stats.defense-effectData.effectStrength;
    }
}//add more