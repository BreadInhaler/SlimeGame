using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraController cameraController;

    [Header("Where the camera goes when locked")]
    public Transform lockedCameraPosition; // an empty in the scene, place it where you want

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            cameraController.LockCamera(lockedCameraPosition.position, lockedCameraPosition.rotation);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            cameraController.UnlockCamera();
    }
}
/*
**Scene hierarchy to set up:**

CameraRig          (empty, has CameraController.cs)
  └── CameraPivot  (empty, drag into "pivot" slot)
        └── Camera (your actual camera, drag into "camArm" slot)
*/