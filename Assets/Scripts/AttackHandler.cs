using UnityEngine;
using System.Collections.Generic;

public class AttackHandler{
    private Attack data;
    private float sustainedTimer;
    private List<ProjectileEmitter> activeEmmitters = new List<ProjectileEmitter>();
    public AttackHandler(Attack data){
        this.data = data;
    }
    public void Execute(Player player){
        if(GetIsSustained()){
            CreateEmmitters(player);
            sustainedTimer=data.fireRate;
        }else{
            CreateEmmitters(player);
            FireEmmitters();
            DestroyEmmitters();
        }
    }
    public void ExecuteTest(Player player){

    }
    public void Tick(float deltaTime){
        if(GetIsSustained() == false) return;
        sustainedTimer+=deltaTime;
        if(sustainedTimer>=data.fireRate){
            FireEmmitters();
            sustainedTimer=0f;
        }
    }
    public void StopTicking(){
        if(GetIsSustained() == false) return;
        DestroyEmmitters();
    }
    public void FireEmmitters(){
        //Debug.Log(activeEmmitters.Count);
        foreach(ProjectileEmitter emitter in activeEmmitters){
            emitter.Fire();
        }
    }
    public void CreateEmmitters(Player player){
        for(int i=0;i<data.numEmmitters;i++){
            float angle = (360f / data.numEmmitters) * i;
            Quaternion rotation = Quaternion.AngleAxis(angle, player.transform.up);
            Vector3 dir = rotation * player.transform.forward;
            Vector3 spawnPos = player.transform.position + dir * data.offset + Vector3.up * 0.5f;
            Vector3 arcDir = Quaternion.AngleAxis(-data.archDir, player.transform.right) * dir;
            //spawnPos = new Vector3(spawnPos.x,spawnPos.x-data.offset,spawnPos.x);
            GameObject obj;
            //adding the emmiters gameobject
            //have to calculate the position and rotation acording to number of emmiters to be created
            obj= UnityEngine.Object.Instantiate(
                this.data.prefab,
                spawnPos,
                Quaternion.LookRotation(arcDir),
                player.transform
            );
            ProjectileEmitter emmitter = obj.GetComponent<ProjectileEmitter>();
            emmitter.Setup(data.projectileData,data.statusEffect);
            activeEmmitters.Add(emmitter);
        }
    }
    public void DestroyEmmitters(){
        foreach(ProjectileEmitter emitter in activeEmmitters){
            GameObject.Destroy(emitter.gameObject);
        }
        activeEmmitters.Clear();
        //destroy the game objects them selves or i can just activate and deactivate them acording with the attacks but i have to remake it so it works with such
    }
    public bool GetIsSustained(){
        return data.isSustained;
    }
}