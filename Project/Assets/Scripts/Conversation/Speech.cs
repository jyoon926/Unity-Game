using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour
{
 
    public AudioSource audioSource;
    public AudioClip[] playerAudioClips;
    public AudioClip[] birdAudioClips;
    public bool playing;
    void Update() {
        playing = audioSource.isPlaying;
    }
    public void PlayRandom(string name) {
        if (name.Equals("Player")) {
            audioSource.clip = playerAudioClips[Random.Range(0, playerAudioClips.Length)];
            audioSource.volume = 0f;
        }
        if (name.Equals("Bird")) {
            audioSource.clip = birdAudioClips[Random.Range(0, birdAudioClips.Length)];
            audioSource.volume = 0.0f;
        }
        audioSource.Play();
    }
}