using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour {
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            Cursor.visible = false;
            Screen.lockCursor = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}