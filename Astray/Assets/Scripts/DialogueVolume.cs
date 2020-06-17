using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueVolume : MonoBehaviour
{
    public Transform player;
    public Transform box;
    public DialogueTrigger trigger;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            trigger.TriggerDialogue();
    }
}
