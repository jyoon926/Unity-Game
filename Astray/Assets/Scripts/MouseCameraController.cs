using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseCameraController : MonoBehaviour
{
    public bool pixelated;
    public RenderTexture renderTexture;
    public GameObject rawImage;
    public float smooth;
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
    
    Vector3 dollyDir;
    private Vector3 dollyDirAdjusted;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camRotation = transform.localRotation;
        offset = transform.localPosition;
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;

        if (pixelated) {
            Camera.main.targetTexture = renderTexture;
            rawImage.SetActive(true);
        }
        else {
            Camera.main.targetTexture = null;
            rawImage.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camRotation.x += Input.GetAxis("Mouse Y") * smooth * (-1) * mouseSensitivity;
        camRotation.y += Input.GetAxis("Mouse X") * smooth * mouseSensitivity;

        camRotation.x = Mathf.Clamp(camRotation.x, 35f, 45f);
        Target.position = Vector3.Lerp(Target.position, new Vector3(Player.position.x, Player.position.y + 2f, Player.position.z), followSpeed * Time.deltaTime);
        Target.rotation = Quaternion.Lerp(Target.rotation, Quaternion.Euler(35f, camRotation.y, camRotation.z), rotationSpeed * Time.deltaTime);
        //ViewObstructed();
    }
    
    void ViewObstructed() {
        Vector3 desiredCameraPos = Target.TransformPoint(dollyDir * maxDistance);

        RaycastHit hit;
        if (Physics.Linecast(Target.position, desiredCameraPos, out hit)) {
            distance = Mathf.Clamp(hit.distance * wallDistance, minDistance, maxDistance);
        }
        else {
            distance = maxDistance;
        }
        transform.localPosition = dollyDir * distance;
    }
    
}
