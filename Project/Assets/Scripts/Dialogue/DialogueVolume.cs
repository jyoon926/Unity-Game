using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueVolume : MonoBehaviour
{
    private Transform player;
    private DialogueTrigger trigger;
    public DialogueManager manager;
    
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        trigger = GetComponent<DialogueTrigger>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            trigger.TriggerDialogue();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            trigger.StopDialogue();
    }
}
