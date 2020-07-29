using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public int index;
    private Image musicControls;
    private Image soundControls;
    private Image fullscreenControls;
    public Color selected;
    public Color deselected;
    public Color black;
    MenuArrow menu;
    private Animator animator;
    void Start()
    {
        index = 0;
        menu = GameObject.Find("Arrow").GetComponent<MenuArrow>();
        musicControls = GameObject.Find("Music").GetComponent<Image>();
        soundControls = GameObject.Find("Sound").GetComponent<Image>();
        fullscreenControls = GameObject.Find("Fullscreen").GetComponent<Image>();
        animator = GameObject.Find("Arrow Options").GetComponent<Animator>();
    }

    void Update()
    {
        if (!menu.onStart) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                this.GetComponent<Canvas>().sortingOrder = -1;
                menu.onStart = true;
                FindObjectOfType<AudioManager>().Play("Click");
            }
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
            if (index == 0) {
                musicControls.color = selected;
                soundControls.color = deselected;
                fullscreenControls.color = deselected;
                animator.SetInteger("Index", 0);
                if (Input.GetKeyDown(KeyCode.RightArrow) && FindObjectOfType<AudioManager>().musicVolume < 10) {
                    FindObjectOfType<AudioManager>().musicVolume++;
                    FindObjectOfType<AudioManager>().Play("Select");
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && FindObjectOfType<AudioManager>().musicVolume > 0) {
                    FindObjectOfType<AudioManager>().musicVolume--;
                    FindObjectOfType<AudioManager>().Play("Select");
                }
            }
            if (index == 1) {
                musicControls.color = deselected;
                soundControls.color = selected;
                fullscreenControls.color = deselected;
                animator.SetInteger("Index", 1);
                if (Input.GetKeyDown(KeyCode.RightArrow) && FindObjectOfType<AudioManager>().soundVolume < 10) {
                    FindObjectOfType<AudioManager>().soundVolume++;
                    FindObjectOfType<AudioManager>().Play("Select");
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && FindObjectOfType<AudioManager>().soundVolume > 0) {
                    FindObjectOfType<AudioManager>().soundVolume--;
                    FindObjectOfType<AudioManager>().Play("Select");
                }
            }
            if (index == 2) {
                musicControls.color = deselected;
                soundControls.color = deselected;
                fullscreenControls.color = selected;
                animator.SetInteger("Index", 2);
                if (Input.GetKeyDown(KeyCode.RightArrow) && FindObjectOfType<AudioManager>().fullscreen) {
                    FindObjectOfType<AudioManager>().fullscreen = false;
                    FindObjectOfType<AudioManager>().Play("Select");
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow) && !FindObjectOfType<AudioManager>().fullscreen) {
                    FindObjectOfType<AudioManager>().fullscreen = true;
                    FindObjectOfType<AudioManager>().Play("Select");
                }
            }
        }
        for (int i = 0; i < 10; i++) {
            if (i < FindObjectOfType<AudioManager>().musicVolume) {
                GameObject.Find("Music Block (" + i + ")").GetComponent<Image>().color = black;
            }
            else {
                GameObject.Find("Music Block (" + i + ")").GetComponent<Image>().color = deselected;
            }
            if (i < FindObjectOfType<AudioManager>().soundVolume) {
                GameObject.Find("Sound Block (" + i + ")").GetComponent<Image>().color = black;
            }
            else {
                GameObject.Find("Sound Block (" + i + ")").GetComponent<Image>().color = deselected;
            }
        }
        if (FindObjectOfType<AudioManager>().fullscreen) {
            GameObject.Find("Yes").GetComponent<Image>().color = selected;
            GameObject.Find("No").GetComponent<Image>().color = deselected;
        }
        else {
            GameObject.Find("Yes").GetComponent<Image>().color = deselected;
            GameObject.Find("No").GetComponent<Image>().color = selected;
        }
    }
}
