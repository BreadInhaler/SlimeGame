using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Follow settings")]
    public float followSpeed = 8f;
    public Vector3 followOffset = new Vector3(0, 2, 0); // height offset above player
    [Header("Lock settings")]
    public float lockTransitionSpeed = 3f;

    [Header("Orbit / tilt")]
    public float mouseSensitivityX = 3f;
    public float mouseSensitivityY = 2f;
    public float minPitchAngle = -20f;
    public float maxPitchAngle = 60f;

    [Header("Rig references")]
    public Transform pivot;   // child empty - rotates up/down
    public Transform camArm;  // the camera itself, just pulled back on Z

    // internal state
    float yaw;    // left/right rotation (Y axis, on the rig)
    float pitch;  // up/down rotation (X axis, on the pivot)

    // lock state
    public bool isLocked = false;
    public bool isTransitioning=false;
    public float transitionTimer = 0f;
    public float transitionDuration = 1f; // tune this in Inspector
    Vector3 lockedPosition;
    Quaternion lockedRotation;

    void Start(){
        // Initialize yaw from current rotation so camera doesn't snap on start
        yaw = transform.eulerAngles.y;
        pitch = pivot.localEulerAngles.x;
    }

    void LateUpdate(){
        if (isLocked) { 
            transform.position = Vector3.Lerp(transform.position, lockedPosition, lockTransitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, lockedRotation, lockTransitionSpeed * Time.deltaTime);
            pivot.localRotation = Quaternion.identity; // reset pitch so it doesn't fight the locked rotation
            return;
        }
        if (isTransitioning){
            transitionTimer += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, transitionTimer / transitionDuration);

            Vector3 target = player.position + followOffset;
            transform.position = Vector3.Lerp(transform.position, target, t);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yaw, 0), t);
            pivot.localRotation = Quaternion.Slerp(pivot.localRotation, Quaternion.Euler(pitch, 0, 0), t);

            if (transitionTimer >= transitionDuration){
                isTransitioning = false;
                // force exact final values so there's no gap when normal follow takes over
                transform.position = target;
                transform.rotation = Quaternion.Euler(0, yaw, 0);
                pivot.localRotation = Quaternion.Euler(pitch, 0, 0);
            }

            return;
        }

        // follow player
        Vector3 targetPos = player.position + followOffset;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        // controller
        var gamepad = Gamepad.current;
        if (gamepad != null){
            Vector2 stickInput = gamepad.rightStick.ReadValue();
            yaw   += stickInput.x * mouseSensitivityX;
            pitch -= stickInput.y * mouseSensitivityY;
        }

        // mouse (works without action asset too)
        var mouse = Mouse.current;
        if (mouse != null){
            Vector2 mouseDelta = mouse.delta.ReadValue();
            yaw   += mouseDelta.x * mouseSensitivityX * Time.deltaTime;
            pitch -= mouseDelta.y * mouseSensitivityY * Time.deltaTime;
    }

    pitch = Mathf.Clamp(pitch, minPitchAngle, maxPitchAngle);

    transform.rotation  = Quaternion.Euler(0, yaw, 0);
    pivot.localRotation = Quaternion.Euler(pitch, 0, 0);
    transform.position = Vector3.Lerp(transform.position, player.position + followOffset, followSpeed * Time.deltaTime);
    }

    // --- Called by trigger zones ---
    public void LockCamera(Vector3 position, Quaternion rotation){
        isLocked = true;
        lockedPosition = position;
        lockedRotation = rotation;
    }

    public void UnlockCamera(){
        isLocked = false;
        isTransitioning = true;
        transitionTimer = 0f;
        yaw = transform.eulerAngles.y;
        pitch = pivot.localEulerAngles.x;
    }
}