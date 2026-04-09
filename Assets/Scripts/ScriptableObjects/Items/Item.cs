using UnityEngine;
public abstract class Item : ScriptableObject{
    public string id;
    public string description;
    public Sprite icon;
    public virtual void Use(Character character){
        Debug.Log(id+" used on "+character.name);
    }
}