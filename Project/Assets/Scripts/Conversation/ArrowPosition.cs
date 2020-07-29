using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPosition : MonoBehaviour
{
    public Transform target;
    public RectTransform parent;
    public RectTransform box;
    void LateUpdate()
    {
        //Position of target relative to center of screen
        float pos = Camera.main.WorldToScreenPoint(target.position).x - (Camera.main.pixelWidth / 2f);
        //Difference between target and parent positions
        pos -= (parent.anchoredPosition.x - (Camera.main.pixelWidth / 2f));
        //Convert position to usable scale
        pos = (pos / (Camera.main.pixelWidth / 2f)) * 4050f;
        //Clamp to width of parent
        pos = Mathf.Clamp(pos, box.sizeDelta.x * -0.5f + 200f, box.sizeDelta.x * 0.5f - 200f);
        //Assign position to rect transform
        if (GetComponent<RectTransform>().anchoredPosition.x != pos) {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(pos, -399.9f);
        }
    }
}