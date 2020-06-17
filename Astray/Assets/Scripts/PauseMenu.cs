using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup canvas;
    public bool pause;
    public MouseCameraController camera;
    public RigidBodyCharacterController playerController;
    public AudioMixer audioMixer;

    void Start()
    {
        canvas.alpha = 0;
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!pause) {
                Pause();
            }
            else {
                Resume();
            }
        }
    }

    public void Pause() {
        StartCoroutine(StartFade(audioMixer, "Volume", 0.5f, -80f));
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        Cursor.visible = true;
        Screen.lockCursor = false;
        Cursor.lockState = CursorLockMode.Confined;
        camera.enabled = false;
        playerController.enabled = false;
        pause = true;
    }
    
    public void Resume() {
        StartCoroutine(StartFade(audioMixer, "Volume", 0.5f, 1f));
        pause = false;
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        Cursor.visible = false;
        Screen.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        camera.enabled = true;
        playerController.enabled = true;
    }

    public IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }
}