using UnityEngine;
[CreateAssetMenu(menuName = "Item/EffectItem" , fileName = "EffectItem")]
public class EffectItem : Item{
    public StatusEffectSO buff;
    public override void Use(Character character){
        StatusEffectInstance buffInst = buff.CreateEffectInstance();
        buffInst.OnApply(character,buffInst);
    }
}