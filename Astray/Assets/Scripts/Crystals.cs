using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystals : MonoBehaviour
{
    public RigidBodyCharacterController player;
    private int crystals;
    public GameObject[] images;

    private void Start() {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].transform.position = new Vector3(images[i].transform.position.x + (50f * i), images[i].transform.position.y, 0f);
        }
    }
    private void Update() {
        crystals = player.platforms;
        for (int i = 0; i < images.Length; i++)
        {
            if (i < crystals) {
                images[i].GetComponent<Animator>().SetBool("Appear", true);
            }
            else {
                images[i].GetComponent<Animator>().SetBool("Appear", false);
            }
        }
    }
}
