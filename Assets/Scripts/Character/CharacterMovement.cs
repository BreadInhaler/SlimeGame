using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour{
    public Animator animator;

    public Stats stats;

    public float speed;
    public float jumpForce;

    private Vector3 velocity;
    public bool movementEnabled=true;

    public bool isGrounded=false;

    public int turnSpeed=10;
    [SerializeField]private Camera cam;
    public CharacterController controller;

    //public InputActionAsset inputSystem;

    void Start(){
        stats=gameObject.GetComponent<Player>().GetStats();
        speed = stats.speed;
        jumpForce = stats.jumpForce;
    }

    void Update(){
        DynamicMovement();
        GroundedCheck();
        VariableJump();

        controller.Move(velocity*Time.deltaTime);
        RotateCharacter();
    }
    private void GroundedCheck(){
        if (!isGrounded) velocity.y-=Globals.gravityModifier*Time.deltaTime;
        else velocity.y=0;
    }
    private void Jump(){
        bool isJumping= InputSystem.actions.FindAction("Jump").IsPressed();
        if(isJumping == true && isGrounded == true){
            velocity.y+=stats.jumpForce;
        }
    }
    private void VariableJump(){
        var isJumping= InputSystem.actions.FindAction("Jump");
        animator.SetBool("isJumping",isJumping.IsPressed());
        if(isJumping.IsPressed() && isGrounded){
            velocity.y+=stats.jumpForce;
        }
        if(isJumping.WasReleasedThisFrame() && velocity.y>0f)
            velocity.y*=0.3f;
    }
    private void RotateCharacter(){
        Vector3 lookRotation = new Vector3(velocity.x,0f,velocity.z);
        
        if (Globals.OR(velocity.x != 0, velocity.z != 0))
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), turnSpeed * Time.deltaTime);
    }
    private void StaticMovement(){
        //move character flat along the x(up and down) and z(left and right) axis
        Vector2 inputMove = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        if (movementEnabled){
            velocity.x = inputMove.x*stats.speed;
            velocity.z = inputMove.y*stats.speed;
        }else{
            velocity.x=0;
            velocity.z=0;
        }
    }
    private void DynamicMovement(){
        //move character along with the rotation of the assigned camera
        Vector2 inputMove = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        if (!movementEnabled){
            velocity.x=0;
            velocity.z=0;
            return;
        }
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        camForward.y=0;
        camRight.y=0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir=camForward*inputMove.y+camRight*inputMove.x;

        velocity.x=moveDir.x*stats.speed;
        velocity.z=moveDir.z*stats.speed;
    }
    public void OnCollisionEnter(Collision collision){
        int groundMask = LayerMask.GetMask("Floor");
        if(Globals.IsInLayerMask(collision.gameObject,groundMask)) isGrounded=true;
    }
    public void OnCollisionExit(Collision collision){
        int groundMask = LayerMask.GetMask("Floor");
        if(Globals.IsInLayerMask(collision.gameObject,groundMask)) isGrounded=false;
    }
}
