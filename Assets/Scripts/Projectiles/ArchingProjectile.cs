using UnityEngine;
public class ArchingProjectile : BaseProjectile{
    private Vector3 currentVelocity;
    private void Start(){
        currentVelocity=transform.forward * data.speed;
    }
    protected override void Move(){
        currentVelocity+=Physics.gravity * Time.deltaTime;
        transform.position += currentVelocity * Time.deltaTime;
    }
    protected override void Rotate(){
        if(currentVelocity != Vector3.zero)
            transform.rotation=Quaternion.LookRotation(currentVelocity);
    }
}