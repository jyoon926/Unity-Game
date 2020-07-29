using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI iPlayerText;
    public Image playerBox;
    public TextMeshProUGUI birdText;
    public TextMeshProUGUI iBirdText;
    public Image birdBox;

    public Animator playerAnimator;
    public Animator birdAnimator;
    private Queue<Sentence> sentences;
    private bool typing = false;
    public bool start;
    public Vector2 playerBoxTarget;
    public Vector2 birdBoxTarget;
    public Animator playerOpacityAnimation;
    public Animator birdOpacityAnimation;
    public float fadeSpeed;
    private string previous = "";
    void Start()
    {
        sentences = new Queue<Sentence>();
        playerBoxTarget = new Vector2(0f, playerBox.rectTransform.sizeDelta.y);
        birdBoxTarget = new Vector2(0f, birdBox.rectTransform.sizeDelta.y);
    }

    void LateUpdate() {
        if (Input.GetKeyDown("e") && start) {
            DisplayNextSentence();
        }
        //Fade the width of box
        Vector2 playerBoxLerp = Vector2.Lerp(playerBox.rectTransform.sizeDelta, playerBoxTarget, Time.deltaTime * fadeSpeed);
        playerBox.rectTransform.sizeDelta = playerBoxLerp;
        Vector2 birdBoxLerp = Vector2.Lerp(birdBox.rectTransform.sizeDelta, birdBoxTarget, Time.deltaTime * fadeSpeed);
        birdBox.rectTransform.sizeDelta = birdBoxLerp;
    }

    public void DisplayNextSentence() {
        if (!typing) {
            if (sentences.Count == 0) {
                EndDialogue();
                return;
            }
            if (start)
                FindObjectOfType<AudioManager>().Play("Select");
            Sentence _sentence = sentences.Dequeue();
            string name = _sentence.name;
            string sentence = _sentence.sentence;
            playerOpacityAnimation.SetBool("Open", false);
            birdOpacityAnimation.SetBool("Open", false);
            StartCoroutine(TypeSentence(name, sentence));
        }
    }

    public void StartDialogue(Conversation conversation) {
        FindObjectOfType<AudioManager>().Play("Dialogue In");
        sentences.Clear();
        foreach (Sentence sentence in conversation.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        start = true;
    }

    public void EndDialogue() {
        if (start)
            FindObjectOfType<AudioManager>().Play("Dialogue Out");
        playerAnimator.SetBool("Open", false);
        birdAnimator.SetBool("Open", false);
        start = false;
    }

    IEnumerator TypeSentence(string name, string sentence) {
        typing = true;
        if (name.Equals("Player")) {
            //if (previous.Equals("Player")) {
                yield return new WaitForSeconds(0.4f);
            //}
            playerAnimator.SetBool("Open", true);
            birdAnimator.SetBool("Open", false);
            iPlayerText.text = sentence;
            playerOpacityAnimation.SetBool("Open", true);
            playerBoxTarget = new Vector2(iPlayerText.preferredWidth + 500f, playerBox.rectTransform.sizeDelta.y);
            birdBoxTarget = new Vector2(0f, birdBox.rectTransform.sizeDelta.y);
            playerText.rectTransform.sizeDelta = new Vector2(iPlayerText.preferredWidth, playerText.rectTransform.sizeDelta.y);
            playerText.text = "";
            yield return new WaitForSeconds(0.5f);
            foreach (char letter in sentence.ToCharArray()) {
                playerText.text += letter;
                yield return new WaitForSeconds(0.025f);
            }
        }
        else if (name.Equals("Bird")) {
            //if (previous.Equals("Bird")) {
                yield return new WaitForSeconds(0.4f);
            //}
            playerAnimator.SetBool("Open", false);
            birdAnimator.SetBool("Open", true);
            iBirdText.text = sentence;
            birdOpacityAnimation.SetBool("Open", true);
            playerBoxTarget = new Vector2(0f, playerBox.rectTransform.sizeDelta.y);
            birdBoxTarget = new Vector2(iBirdText.preferredWidth + 500f, birdBox.rectTransform.sizeDelta.y);
            birdText.rectTransform.sizeDelta = new Vector2(iBirdText.preferredWidth, birdText.rectTransform.sizeDelta.y);
            birdText.text = "";
            yield return new WaitForSeconds(0.5f);
            foreach (char letter in sentence.ToCharArray()) {
                birdText.text += letter;
                yield return new WaitForSeconds(0.025f);
            }
        }
        yield return null;
        previous = name;
        typing = false;
    }
}