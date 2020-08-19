using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VolumeTrigger : MonoBehaviour
{
    public UnityEvent postAction;
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            postAction.Invoke();
        }
    }
}
