using UnityEngine;
public class Stats{
    public float hp;
    public float maxHP;
    public float defense;
    public float damage;
    public float speed;
    public float jumpForce;
    public Stats(){
        this.maxHP=0;
        hp=maxHP;
        this.speed=0;
        this.defense=0;
        this.damage=0;
        this.jumpForce=0;
    }
    public Stats(float maxHP,float speed,float defense,float damage,float jumpForce){
        this.maxHP=maxHP;
        hp=maxHP;
        this.speed=speed;
        this.defense=defense;
        this.damage=damage;
        this.jumpForce=jumpForce;
    }
    public Stats(StatsSO stats){
        this.maxHP=stats.maxHP;
        hp=maxHP;
        this.speed=stats.speed;
        this.defense=stats.defense;
        this.damage=stats.damage;
        this.jumpForce=stats.jumpForce;
    }
    public Stats(float hp,float maxHP,float speed,float defense,float damage,float jumpForce){
        this.hp=hp;
        this.maxHP=maxHP;
        this.speed=speed;
        this.defense=defense;
        this.damage=damage;
        this.jumpForce=jumpForce;
    }
    public Stats CloneStats(){
        return new Stats(
            hp,
            maxHP,
            defense,
            damage,
            speed,
            jumpForce
        );
    }
}