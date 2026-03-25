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