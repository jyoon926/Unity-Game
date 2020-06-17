using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemScript : MonoBehaviour
{
    public Animator transition;
    public RigidBodyCharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotate());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r")) {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }
    }

    IEnumerator Rotate() {
        yield return new WaitForSeconds(0);
        player.angle = 140f;
    }

    IEnumerator LoadLevel(int index) {
        transition.SetTrigger("Exit");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
}
