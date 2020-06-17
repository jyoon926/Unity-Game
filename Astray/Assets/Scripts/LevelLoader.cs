using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject button;
    public Animator transition;
    public Animator panelAnimator;
    public float transitionTime = 1f;
    public AudioManager audio;

    public GameObject panel;
    // Update is called once per frame
    
    private void Start() {
        //CursorControl.instance.ActivatePointer();
    }

    public void LoadNextLevel() {
        Debug.Log("Enter");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Exit() {
        Debug.Log("Exit");
        Application.Quit();
    }

    IEnumerator LoadLevel(int index) {
        transition.SetTrigger("Exit");
        yield return new WaitForSeconds(3);
        CursorControl.instance.width = 0f;
        if (index == 1) {
            panelAnimator.SetTrigger("Panel");
            yield return new WaitForSeconds(5);
        }
        SceneManager.LoadScene(index);
        
        audio.Play("Wind");
    }
}
