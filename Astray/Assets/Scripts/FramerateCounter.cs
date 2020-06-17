using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FramerateCounter : MonoBehaviour
{
    private int frameCounter = 0;
    private float timeCounter = 0f;
    private float refreshTime = 0.1f;
    public Text text;

    void Update()
    {
        if (timeCounter < refreshTime) {
            timeCounter += Time.deltaTime;
            frameCounter++;
        }
        else {
            float lastFramerate = frameCounter / timeCounter;
            frameCounter = 0;
            timeCounter = 0.0f;
            text.text = lastFramerate.ToString("n0") + " fps";
        }
    }
}
