using UnityEngine;
[CreateAssetMenu(menuName = "Effect/Fire",fileName = "FireEffect")]
public class FireEffectSO : StatusEffectSO{
    public override StatusEffectInstance CreateEffectInstance(){return new FireEffect(this);}
}