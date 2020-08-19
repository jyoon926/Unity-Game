using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPosition;
    public GameObject cam;
    public float parallax;

    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float dist = (cam.transform.position.x * parallax);
        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);
    }
}
