using UnityEngine;
[CreateAssetMenu(menuName = "Effect/DefenseBreak",fileName = "DefenseBreakEffect")]
public class DefenseBreakEffectSO : StatusEffectSO{
    public override StatusEffectInstance CreateEffectInstance(){return new DefenseBreakEffect(this);}
}