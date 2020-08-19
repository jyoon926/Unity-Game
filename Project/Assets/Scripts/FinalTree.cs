using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTree : MonoBehaviour
{
    public MouseCameraController cameraController;
    public Transform player;
    bool start;
    bool triggered;
    public Animator birds;

    // Update is called once per frame
    void Update()
    {
        if (start) {
            float value = Vector2.Distance(new Vector2(player.position.x, player.position.z), new Vector2(transform.position.x, transform.position.z));
            value = Mathf.Clamp(value, 0f, 50f) / 50f;
            float invert = value * -1f + 1f;
            float angle = value * 25f + 10f;
            angle = Mathf.Clamp(angle, 5f, 35f);
            cameraController.angle = angle;
            float distance = invert * 15f + 24f;
            cameraController.distance = distance;
            float height = invert * 6f + 2f;
            cameraController.height = height;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            start = true;
            birds.SetBool("Start", true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.name == "Player") {
            start = false;
        }
    }
}
