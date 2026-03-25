using UnityEngine;
[CreateAssetMenu(menuName = "Character/Stats" , fileName = "Stats")]
public class StatsSO : ScriptableObject{
    public float maxHP;
    public float defense;
    public float damage;
    public float speed;
    public float jumpForce;
}