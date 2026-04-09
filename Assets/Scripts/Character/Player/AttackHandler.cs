using UnityEngine;
using System.Collections.Generic;

public class AttackHandler{
    public Attack data;
    private float sustainedTimer;
    private List<ProjectileEmitter> activeEmmitters = new List<ProjectileEmitter>();
    public AttackHandler(Attack data){
        this.data = data;
    }
    public void Execute(Character character,float archDir=0,Transform target=null){
        if(GetIsSustained()){
            CreateEmmitters(character,archDir,target);
            sustainedTimer=data.fireRate;
        }else{
            CreateEmmitters(character,archDir,target);
            FireEmmitters();
            DestroyEmmitters();
        }
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
            Debug.DrawRay(character.transform.position, dir * 3, Color.blue, 2f);  // horizontal aim
            Vector3 spawnPos = character.transform.position + dir * data.offset + Vector3.up * 0.5f;
            Vector3 arcDir;
            if(arcDirection!=0){
                Vector3 right = Vector3.Cross(dir,Vector3.up).normalized;
                arcDir = Quaternion.AngleAxis(-arcDirection, right) * dir;
                Debug.Log("arcDirection: " + arcDirection);
                Debug.Log("dir: " + dir);
                Debug.Log("right: " + right);
                Debug.Log("arcDir: " + arcDir);
            }
            else arcDir = Quaternion.AngleAxis(-data.archDir, character.transform.right) * dir;
            Debug.DrawRay(spawnPos, arcDir * 3, Color.red, 2f);  // final fire direction
            //spawnPos = new Vector3(spawnPos.x,spawnPos.x-data.offset,spawnPos.x);
            GameObject obj;
            //adding the emmiters gameobject
            //have to calculate the position and rotation acording to number of emmiters to be created
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