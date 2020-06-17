using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    public Animator transition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    
    IEnumerator LoadLevel(int index) {
        transition.SetTrigger("Exit");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(index);
    }
}
