using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Text dialogueText;
    private Animator animator;
    private Queue<string> sentences;
    private bool typing = false;
    public bool start;
    private RigidBodyCharacterController playerController;

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update() {
        if (Input.GetKeyDown("e")) {
        //    DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        dialogueText = GameObject.FindGameObjectWithTag("Dialogue Text").GetComponent<Text>();
        animator = GameObject.FindGameObjectWithTag("Dialogue Animator").GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<RigidBodyCharacterController>();

        FindObjectOfType<AudioManager>().Play("Dialogue In");

        animator.SetBool("IsOpen", true);
        //Cursor.lockState = CursorLockMode.Confined;

        //nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        start = true;
    }

    public void DisplayNextSentence() {
        if (!typing) {
            if (sentences.Count == 0) {
                EndDialogue();
                return;
            }
            if (start)
                FindObjectOfType<AudioManager>().Play("Select");
            string sentence = sentences.Dequeue();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence) {
        typing = true;
        dialogueText.text = sentence;
        /*foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }*/
        yield return null;
        typing = false;
    }

    public void EndDialogue() {
        if (start)
            FindObjectOfType<AudioManager>().Play("Dialogue Out");
        animator.SetBool("IsOpen", false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        start = false;
    }
}
