using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    public bool freezePlayer;
    public float delay;
    private Transform player;
    public ConversationManager manager;
    public Conversation conversation;
    private bool triggered;
    public bool checkSprint = false;
    public bool checkJump = false;
    public RigidBodyCharacterController playerController;
    
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        triggered = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.name == "Player" && !manager.start) {
            if (checkSprint) {
                if (!playerController.learnedToSprint) {
                    if (freezePlayer) {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<RigidBodyCharacterController>().canControl = false;
                    }
                    manager.StartDialogue(conversation, delay);
                    triggered = true;
                }
            }
            else if (checkJump) {
                if (freezePlayer) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<RigidBodyCharacterController>().canControl = false;
                }
                manager.StartDialogue(conversation, delay);
                triggered = true;
            }
            else {
                if (freezePlayer) {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<RigidBodyCharacterController>().canControl = false;
                }
                manager.StartDialogue(conversation, delay);
                triggered = true;
            }
        }
    }
}