using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapArrow : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    void Update()
    {
        transform.position = new Vector3(player.position.x, 38f, player.position.z);
        transform.rotation = Quaternion.Euler(90f, 0f, cam.eulerAngles.y * -1);
    }
}