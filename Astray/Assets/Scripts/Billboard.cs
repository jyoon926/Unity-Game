using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Update() 
    {
        Vector3 target = Camera.main.transform.position - transform.position;
        float angle = Mathf.Atan2(target.z, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, -1 * angle - 90, 0);
    }
}
