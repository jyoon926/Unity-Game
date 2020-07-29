using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public LevelLoader levelLoader;
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            if (!FindObjectOfType<AudioManager>().IsPlaying("Splash"))
            {
                FindObjectOfType<AudioManager>().Play("Splash");
            }
            levelLoader.RestartLevel();
        }
    }
}
