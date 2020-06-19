using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;
    private bool typing = false;
    //public GameObject cinemachine;
    public MouseCameraController camera;
    public RigidBodyCharacterController playerController;
    public InDialogue playerDialogue;
    public Animator playerAnimator;
    public AudioSource audioData;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update() {
        if (Input.GetKeyDown("space"))
            DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue) {
        audioData.Pause();
        camera.enabled = false;
        playerController.enabled = false;
        playerAnimator.SetBool("isRunning", false);
        animator.SetBool("IsOpen", true);
        CursorControl.instance.width = 80f;
        Screen.lockCursor = false;
        Cursor.lockState = CursorLockMode.Confined;

        playerDialogue.inDialogue = true;

        //nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (!typing) {
            if (sentences.Count == 0) {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence) {
        typing = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
        typing = false;
    }

    public void EndDialogue() {
        animator.SetBool("IsOpen", false);
        Cursor.visible = false;
        Screen.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        camera.enabled = true;
        CursorControl.instance.width = 0f;
        playerController.enabled = true;
    }
}
