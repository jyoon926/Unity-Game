using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject dialogueBox;
    private void Start() {
    }
    public void TriggerDialogue() {
        dialogueBox.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    public void StopDialogue() {
        FindObjectOfType<DialogueManager>().EndDialogue();
        StartCoroutine(Disable());
    }
    IEnumerator Disable() {
        yield return new WaitForSeconds(0.25f);
        dialogueBox.SetActive(false);
    }
}
