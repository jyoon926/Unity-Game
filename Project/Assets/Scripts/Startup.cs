using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Startup : MonoBehaviour {
    
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            if (!FindObjectOfType<AudioManager>().IsPlaying("Theme 1")) {
                FindObjectOfType<AudioManager>().Play("Theme 1");
            }
            if (!FindObjectOfType<AudioManager>().IsPlaying("Wind")) {
                FindObjectOfType<AudioManager>().Play("Wind");
            }
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Period)) {
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart() {
        GameObject.FindGameObjectWithTag("Black").GetComponent<Animator>().SetBool("Out", false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}