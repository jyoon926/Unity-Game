using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI iPlayerText;
    public Image playerBox;
    public TextMeshProUGUI birdText;
    public TextMeshProUGUI iBirdText;
    public Image birdBox;
    public TextMeshProUGUI parentsText;
    public TextMeshProUGUI iParentsText;
    public Image parentsBox;

    public Animator playerAnimator;
    public Animator birdAnimator;
    public Animator parentsAnimator;
    private Queue<Sentence> sentences;
    public bool typing = false;
    public bool loop = false;
    public bool start;
    public Vector2 playerBoxTarget;
    public Vector2 birdBoxTarget;
    public Vector2 parentsBoxTarget;
    public Animator playerOpacityAnimation;
    public Animator birdOpacityAnimation;
    public Animator parentsOpacityAnimation;
    public float fadeSpeed;
    private string previous = "";
    public Speech speech;
    string currentCharacter;
    void Start()
    {
        sentences = new Queue<Sentence>();
        playerBoxTarget = new Vector2(0f, playerBox.rectTransform.sizeDelta.y);
        birdBoxTarget = new Vector2(0f, birdBox.rectTransform.sizeDelta.y);
        parentsBoxTarget = new Vector2(0f, parentsBox.rectTransform.sizeDelta.y);
    }

    void LateUpdate() {
        if (Input.GetKeyDown("e")) {
            if (!typing) {
                if (start)
                    DisplayNextSentence();
            }
            else {
                loop = false;
            }
        }
        //Fade the width of box
        Vector2 playerBoxLerp = Vector2.Lerp(playerBox.rectTransform.sizeDelta, playerBoxTarget, Time.deltaTime * fadeSpeed);
        playerBox.rectTransform.sizeDelta = playerBoxLerp;
        Vector2 birdBoxLerp = Vector2.Lerp(birdBox.rectTransform.sizeDelta, birdBoxTarget, Time.deltaTime * fadeSpeed);
        birdBox.rectTransform.sizeDelta = birdBoxLerp;
        Vector2 parentsBoxLerp = Vector2.Lerp(parentsBox.rectTransform.sizeDelta, parentsBoxTarget, Time.deltaTime * fadeSpeed);
        parentsBox.rectTransform.sizeDelta = parentsBoxLerp;
    }

    void Update() {
        if (typing && !speech.playing) {
            speech.PlayRandom(currentCharacter);
        }
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
            parentsOpacityAnimation.SetBool("Open", false);
            StartCoroutine(TypeSentence(name, sentence));
        }
    }

    public void StartDialogue(Conversation conversation, float delay = 0f) {
        StartCoroutine(StartD(conversation, delay));
    }
    IEnumerator StartD(Conversation conversation, float delay = 0f) {
        yield return new WaitForSeconds(delay);
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
        playerOpacityAnimation.SetBool("Open", false);
        birdOpacityAnimation.SetBool("Open", false);
        parentsOpacityAnimation.SetBool("Open", false);
        StartCoroutine(Close());
        GameObject.FindGameObjectWithTag("Player").GetComponent<RigidBodyCharacterController>().canControl = true;
        
        StartCoroutine(StartFalse());
    }
    IEnumerator StartFalse() {
        yield return new WaitForSeconds(1.5f);
        start = false;
    }

    IEnumerator Close() {
        yield return new WaitForSeconds(0.4f);
        playerAnimator.SetBool("Open", false);
        birdAnimator.SetBool("Open", false);
        parentsAnimator.SetBool("Open", false);
        birdBoxTarget = new Vector2(26f, birdBox.rectTransform.sizeDelta.y);
        parentsBoxTarget = new Vector2(26f, birdBox.rectTransform.sizeDelta.y);
        playerBoxTarget = new Vector2(26f, birdBox.rectTransform.sizeDelta.y);
    }

    IEnumerator TypeSentence(string name, string sentence) {
        currentCharacter = name;
        loop = true;
        typing = true;
        if (name.Equals("Player")) {
            yield return new WaitForSeconds(0.4f);
            playerAnimator.SetBool("Open", true);
            birdAnimator.SetBool("Open", false);
            parentsAnimator.SetBool("Open", false);
            playerText.text = "";
            iPlayerText.text = sentence;
            playerOpacityAnimation.SetBool("Open", true);
            playerBoxTarget = new Vector2(iPlayerText.preferredWidth + 20f, playerBox.rectTransform.sizeDelta.y);
            birdBoxTarget = new Vector2(26f, birdBox.rectTransform.sizeDelta.y);
            parentsBoxTarget = new Vector2(26f, birdBox.rectTransform.sizeDelta.y);
            playerText.rectTransform.sizeDelta = new Vector2(iPlayerText.preferredWidth, playerText.rectTransform.sizeDelta.y);
            yield return new WaitForSeconds(0.5f);
            foreach (char letter in sentence.ToCharArray()) {
                if (loop) {
                    playerText.text += letter;
                    yield return new WaitForSeconds(0.025f);
                }
            }
            playerText.text = sentence;
            loop = false;
        }
        else if (name.Equals("Bird")) {
            yield return new WaitForSeconds(0.4f);
            playerAnimator.SetBool("Open", false);
            birdAnimator.SetBool("Open", true);
            birdText.text = "";
            iBirdText.text = sentence;
            birdOpacityAnimation.SetBool("Open", true);
            playerBoxTarget = new Vector2(26f, playerBox.rectTransform.sizeDelta.y);
            birdBoxTarget = new Vector2(iBirdText.preferredWidth + 20f, birdBox.rectTransform.sizeDelta.y);
            birdText.rectTransform.sizeDelta = new Vector2(iBirdText.preferredWidth, birdText.rectTransform.sizeDelta.y);
            yield return new WaitForSeconds(0.5f);
            foreach (char letter in sentence.ToCharArray()) {
                if (loop) {
                    birdText.text += letter;
                    yield return new WaitForSeconds(0.025f);
                }
            }
            birdText.text = sentence;
            loop = false;
        }
        else if (name.Equals("Parents")) {
            yield return new WaitForSeconds(0.4f);
            playerAnimator.SetBool("Open", false);
            birdAnimator.SetBool("Open", false);
            parentsAnimator.SetBool("Open", true);
            iParentsText.text = sentence;
            parentsOpacityAnimation.SetBool("Open", true);
            playerBoxTarget = new Vector2(26f, playerBox.rectTransform.sizeDelta.y);
            birdBoxTarget = new Vector2(26f, birdBox.rectTransform.sizeDelta.y);
            parentsBoxTarget = new Vector2(iParentsText.preferredWidth + 500f, parentsBox.rectTransform.sizeDelta.y);
            parentsText.rectTransform.sizeDelta = new Vector2(iParentsText.preferredWidth, parentsText.rectTransform.sizeDelta.y);
            parentsText.text = "";
            yield return new WaitForSeconds(0.5f);
            foreach (char letter in sentence.ToCharArray()) {
                if (Input.GetKeyDown("e")) {
                    parentsText.text = sentence;
                    loop = false;
                    //typing = false;
                }
                else if (loop) {
                    parentsText.text += letter;
                    yield return new WaitForSeconds(0.025f);
                }
            }
        }
        yield return null;
        previous = name;
        typing = false;
    }
}