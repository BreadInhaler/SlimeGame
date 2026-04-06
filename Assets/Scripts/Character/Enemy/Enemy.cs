using UnityEngine;

public class Enemy : Character{
    public override void TakeDamage(float damage){
        base.TakeDamage(damage);
        Globals.lastHitEnemy=this;
    }
}
