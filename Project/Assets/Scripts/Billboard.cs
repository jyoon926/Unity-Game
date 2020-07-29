using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update() 
    {
        Vector3 target = Camera.main.transform.position - transform.position;
        float angle = Mathf.Atan2(target.z, target.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(angle3, -1 * angle - 90, angle2);
        //transform.LookAt(Camera.main.transform.position, Vector3.up);
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}
