using UnityEngine;
public class BillBoard : MonoBehaviour {
    public Camera cam;
    public bool lockVertical = true; // stops tilting on slopes

    void Update() {
        Vector3 direction = transform.position - cam.transform.position;

        if (lockVertical)
            direction.y = 0; // keeps it perfectly upright

        transform.rotation = Quaternion.LookRotation(direction);
    }
}