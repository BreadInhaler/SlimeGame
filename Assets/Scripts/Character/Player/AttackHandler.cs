using UnityEngine;
using System.Collections.Generic;

public class AttackHandler{
    public Attack data;
    private float sustainedTimer;
    public bool isExecuting=false;
    private List<ProjectileEmitter> activeEmmitters = new List<ProjectileEmitter>();
    public AttackHandler(Attack data){
        this.data = data;
    }
    public void Execute(Character character,float archDir=0,Transform target=null){
        if(GetIsSustained()){
            isExecuting=true;
            CreateEmmitters(character,archDir,target);
            sustainedTimer=data.fireRate;
        }else{
            CreateEmmitters(character,archDir,target);
            if(character.GetComponent<Player>()!=null) FireEmmitters(character.GetComponent<Player>());
            else if(character.GetComponent<Enemy>()!=null) FireEmmitters(character.GetComponent<Enemy>());
            DestroyEmmitters();
        }
    }
    public void Tick(float deltaTime,Character character){
        if(GetIsSustained() == false || isExecuting==false) return;
        sustainedTimer+=deltaTime;
        if(sustainedTimer>=data.fireRate){
            if(character.GetComponent<Player>()!=null) FireEmmitters(character.GetComponent<Player>());
            else if(character.GetComponent<Enemy>()!=null) FireEmmitters(character.GetComponent<Enemy>());
            sustainedTimer=0f;
        }
    }
    public void StopTicking(){
        if(GetIsSustained() == false) return;
        isExecuting=false;
        DestroyEmmitters();
    }
    public void FireEmmitters(Player player){
        if(player==null) return;
        else{ 
            if(player.spHandler.HasEnough(data.spCost)){
                foreach(ProjectileEmitter emitter in activeEmmitters) emitter.Fire();
                player.spHandler.RemoveAmount(data.spCost);
                player.spHandler.ResetDelayTimer();
            }
        }
    }
    public void FireEmmitters(Enemy enemy){
        foreach(ProjectileEmitter emitter in activeEmmitters) emitter.Fire();
    }
    public void CreateEmmitters(Character character,float arcDirection=0, Transform target=null){
        for(int i=0;i<data.numEmmitters;i++){
            float angle = (360f / data.numEmmitters) * i;
            Quaternion rotation = Quaternion.AngleAxis(angle, character.transform.up);
            Vector3 dir = rotation * character.transform.forward;
            if(target != null){
                Vector3 toTarget = target.position - character.transform.position;
                toTarget.y = 0;
                dir = toTarget.normalized;
            }
            Vector3 spawnPos = character.transform.position + dir * data.offset + Vector3.up * 0.5f;
            Vector3 arcDir;
            if(arcDirection!=0){
                Vector3 right = Vector3.Cross(dir,Vector3.up).normalized;
                arcDir = Quaternion.AngleAxis(-arcDirection, right) * dir;
            }
            else arcDir = Quaternion.AngleAxis(-data.archDir, character.transform.right) * dir;
            GameObject obj;
            obj= UnityEngine.Object.Instantiate(
                this.data.prefab,
                spawnPos,
                Quaternion.LookRotation(arcDir),
                character.transform
            );
            ProjectileEmitter emmitter = obj.GetComponent<ProjectileEmitter>();
            emmitter.Setup(data.projectileData,data.statusEffect);
            activeEmmitters.Add(emmitter);
        }
    }
    public void DestroyEmmitters(){
        foreach(ProjectileEmitter emitter in activeEmmitters) GameObject.Destroy(emitter.gameObject);
        activeEmmitters.Clear();
    }
    public bool GetIsSustained(){
        return data.isSustained;
    }
}