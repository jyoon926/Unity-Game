using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCameraController : MonoBehaviour
{    public float smooth;
    public Transform Target;
    public Transform Player;
    public float zoomSpeed;
    public float rotationSpeed;
    public float followSpeed;
    public float mouseSensitivity;
    
    private Quaternion camRotation;
    private RaycastHit hit;
    private Vector3 offset;
    public float distance;
    public float minDistance;
    public float maxDistance;
    public float wallDistance;
    public float angle;
    
    //Vector3 dollyDir;
    //private Vector3 dollyDirAdjusted;

    // Start is called before the first frame update
    void Start()
    {
        //camRotation = transform.localRotation;
        //offset = transform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //camRotation.x += Input.GetAxis("Mouse Y") * smooth * (-1) * mouseSensitivity;
        camRotation.y += Input.GetAxis("Mouse X") * smooth * mouseSensitivity;

        //camRotation.x = Mathf.Clamp(camRotation.x, 35f, 45f);
        Target.position = Vector3.Lerp(Target.position, new Vector3(Player.position.x, Player.position.y + 2f, Player.position.z), followSpeed * Time.deltaTime);
        Target.rotation = Quaternion.Lerp(Target.rotation, Quaternion.Euler(angle, camRotation.y, camRotation.z), rotationSpeed * Time.deltaTime);
        //Target.rotation = Quaternion.Euler(angle, camRotation.y, camRotation.z);
        //ViewObstructed();
    }
    
    /*void ViewObstructed() {
        Vector3 desiredCameraPos = Target.TransformPoint(dollyDir * maxDistance);

        RaycastHit hit;
        if (Physics.Linecast(Target.position, desiredCameraPos, out hit)) {
            distance = Mathf.Clamp(hit.distance * wallDistance, minDistance, maxDistance);
        }
        else {
            distance = maxDistance;
        }
        transform.localPosition = dollyDir * distance;
    }*/
    
}
