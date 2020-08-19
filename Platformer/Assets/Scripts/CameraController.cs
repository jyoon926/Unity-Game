using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public Transform player1;
    public Transform player2;

    void LateUpdate()
    {
        Vector3 position = new Vector3((player2.position.x + player1.position.x) / 2f, 0f, -10f);
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);
    }
}
