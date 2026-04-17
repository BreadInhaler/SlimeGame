using UnityEngine;
public class BillBoard : MonoBehaviour {
    public Camera cam;
    public bool lockVertical = true; // stops tilting on slopes
    void Start(){
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Update() {
        Vector3 direction = transform.position - cam.transform.position;
        if (lockVertical) direction.y = 0; // keeps it perfectly upright
        transform.rotation = Quaternion.LookRotation(direction);
    }
}