using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystals : MonoBehaviour
{
    public RigidBodyCharacterController player;
    private int crystals;
    public RectTransform[] images;

    private void Start() {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].anchoredPosition = new Vector3(11f * i, 0f, 0f);
        }
        Check(player.platforms);
    }
    //private void Update() {
    //}
    public void Check(int n) {
        crystals = n;
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
