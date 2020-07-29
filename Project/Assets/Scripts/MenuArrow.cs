using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuArrow : MonoBehaviour
{
    Animator animator;
    public Animator instructions;
    public int index;
    public RawImage start;
    public RawImage options;
    public RawImage quit;
    public Color selected;
    public Color deselected;
    public LevelLoader levelLoader;
    public bool onStart = true;
    public Canvas optionsCanvas;
    void Start()
    {
        animator = GetComponent<Animator>();
        index = 0;
        optionsCanvas.sortingOrder = -1;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Index", index);
        if (onStart) {
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                if (index == 0 || index == 1) {
                    index ++;
                    FindObjectOfType<AudioManager>().Play("Select");
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                if (index == 1 || index == 2) {
                    index --;
                    FindObjectOfType<AudioManager>().Play("Select");
                }
            }
        }
        if (index == 0) {
            start.color = selected;
            options.color = deselected;
            quit.color = deselected;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
                FindObjectOfType<AudioManager>().Play("Click");
                StartCoroutine(StartGame());
            }
        }
        else if (index == 1) {
            start.color = deselected;
            options.color = selected;
            quit.color = deselected;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
                onStart = false;
                FindObjectOfType<AudioManager>().Play("Click");
                optionsCanvas.sortingOrder = 1;
            }
        }
        else if (index == 2) {
            start.color = deselected;
            options.color = deselected;
            quit.color = selected;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
                FindObjectOfType<AudioManager>().Play("Click");
                levelLoader.Quit();
            }
        }
    }
    IEnumerator StartGame() {
        instructions.SetBool("Black", true);
        yield return new WaitForSeconds(2f);
        instructions.SetBool("In", true);
        yield return new WaitForSeconds(6f);
        levelLoader.LoadLevel();
        yield return new WaitForSeconds(1f);
        instructions.SetBool("In", false);
    }
}
