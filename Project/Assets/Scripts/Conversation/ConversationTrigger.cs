using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    private Transform player;
    public ConversationManager manager;
    public Conversation conversation;
    private bool triggered;
    
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        triggered = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.name == "Player") {
            manager.StartDialogue(conversation);
            triggered = true;
        }
    }
}