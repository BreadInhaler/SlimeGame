using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {
    [Header("Target")]
    public Transform player;

    [Header("Follow Settings")]
    public float followSpeed = 8f;
    public Vector3 followOffset = new Vector3(0, 2, 0);

    [Header("Lock Settings")]
    public float lockTransitionSpeed = 3f;

    [Header("Orbit / Tilt")]
    public float mouseSensitivityX = 3f;
    public float mouseSensitivityY = 2f;
    public float minPitchAngle = -20f;
    public float maxPitchAngle = 60f;

    [Header("Rig References")]
    public Transform pivot;
    public Transform camArm;

    float yaw;
    float pitch;

    public bool isLocked = false;
    bool isReturning = false;
    Vector3 lockedPosition;
    Quaternion lockedRotation;

    void Start() {
        yaw = transform.eulerAngles.y;
        pitch = pivot.localEulerAngles.x;
    }

    void LateUpdate() {
        if (isLocked) {
            transform.position = Vector3.Lerp(transform.position, lockedPosition, lockTransitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, lockedRotation, lockTransitionSpeed * Time.deltaTime);
            pivot.localRotation = Quaternion.identity;
            return;
        }

        // read input regardless of returning or not
        var gamepad = Gamepad.current;
        if (gamepad != null) {
            Vector2 stickInput = gamepad.rightStick.ReadValue();
            yaw   += stickInput.x * mouseSensitivityX;
            pitch -= stickInput.y * mouseSensitivityY;
        }

        var mouse = Mouse.current;
        if (mouse != null) {
            Vector2 mouseDelta = mouse.delta.ReadValue();
            yaw   += mouseDelta.x * mouseSensitivityX * Time.deltaTime;
            pitch -= mouseDelta.y * mouseSensitivityY * Time.deltaTime;
        }

        pitch = Mathf.Clamp(pitch, minPitchAngle, maxPitchAngle);

        if (isReturning) {
            Vector3 targetPos = player.position + followOffset;
            transform.position = Vector3.Lerp(transform.position, targetPos, lockTransitionSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yaw, 0), lockTransitionSpeed * Time.deltaTime);
            pivot.localRotation = Quaternion.Slerp(pivot.localRotation, Quaternion.Euler(pitch, 0, 0), lockTransitionSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.05f) {
                isReturning = false;
            }
            return;
        }

        Vector3 target = player.position + followOffset;
        transform.position = Vector3.Lerp(transform.position, target, followSpeed * Time.deltaTime);
        transform.rotation  = Quaternion.Euler(0, yaw, 0);
        pivot.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    public void LockCamera(Vector3 position, Quaternion rotation) {
        isLocked = true;
        isReturning = false;
        lockedPosition = position;
        lockedRotation = rotation;
    }

    public void UnlockCamera() {
        isLocked = false;
        isReturning = true;
        yaw = transform.eulerAngles.y;
        pitch = pivot.localEulerAngles.x;
    }
}