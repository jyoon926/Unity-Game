using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private void Start() {
        GameObject.FindGameObjectWithTag("Black").GetComponent<Animator>().SetBool("Out", true);
        //Cursor.visible = false;
        //Screen.lockCursor = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void LoadLevel() {
        StartCoroutine(LoadAsynchronously(1));
    }

    public void RestartLevel() {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }

    public void Quit() {
        StartCoroutine(QuitGame());
    }

    IEnumerator LoadAsynchronously(int index) {
        GameObject.FindGameObjectWithTag("Black").GetComponent<Animator>().SetBool("Out", false);
        yield return new WaitForSeconds(2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }

    IEnumerator QuitGame() {
        GameObject.FindGameObjectWithTag("Black").GetComponent<Animator>().SetBool("Out", false);
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
