using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator black;
    public Animator blackTop;
    public GameObject player;
    public GameObject bird;
    public GameObject cam;
    public GameObject camTarget;

    private void Start() {
        black.SetBool("Out", true);
        //blackTop.SetBool("Out", false);
        Cursor.visible = false;
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

    public void Relocation(Vector3 position, bool mouseControl, float cameraAngle) {
        StartCoroutine(LoadAsynchronously(position, mouseControl, cameraAngle));
    }

    IEnumerator LoadAsynchronously(int index) {
        black.SetBool("Out", false);
        yield return new WaitForSeconds(2f);
        blackTop.SetBool("Out", true);
        yield return new WaitForSeconds(2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }

    IEnumerator LoadAsynchronously(Vector3 position, bool mouseControl, float cameraAngle) {
        black.SetBool("Out", false);
        yield return new WaitForSeconds(2f);
        blackTop.SetBool("Out", true);
        yield return new WaitForSeconds(2f);
        player.transform.position = position;
        bird.transform.position = new Vector3(position.x, position.y, position.z - 1.5f);
        camTarget.transform.position = new Vector3(position.x, position.y + 2f, position.z);
        cam.GetComponent<MouseCameraController>().mouseControl = mouseControl;
        cam.GetComponent<MouseCameraController>().angle = cameraAngle;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }

    IEnumerator QuitGame() {
        GetComponent<Animator>().SetBool("Out", false);
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
