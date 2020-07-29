using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Animator eanim;
    private bool inside = false;
    public GameObject item;
    private void Start() {
    }
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            eanim.SetBool("Up", true);
            inside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player") {
            eanim.SetBool("Up", false);
            inside = false;
        }
    }
    private void Update() {
        if (inside && Input.GetKeyDown(KeyCode.E)) {
            StartCoroutine(Destroy());
        }
    }
    IEnumerator Destroy() {
        eanim.SetBool("Up", false);
        item.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }
}
