using UnityEngine;
[CreateAssetMenu(menuName = "Player/AbilityData" , fileName = "Ability")]
public class AbilityData : ScriptableObject{
    public string id;
    public GameObject prefab;
    public StatusEffectInstance statusEffect;
    public Attack baseAttack;
    public Attack airAttack;
    public Attack runAttack;
}