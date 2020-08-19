using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public Transform target;
    public float offset;
    private Camera cam;
    
    private void Start() {
        cam = Camera.main;
    }
    void LateUpdate()
    {
        Vector3 pos = cam.WorldToScreenPoint(new Vector3(target.position.x, target.position.y + offset, target.position.z));
        Vector2 offsetPos = new Vector2(Mathf.Lerp(GetComponent<RectTransform>().anchoredPosition.x, pos.x - 200f, Time.deltaTime * 5f), Mathf.Lerp(GetComponent<RectTransform>().anchoredPosition.y, pos.y - 112.5f, Time.deltaTime * 5f));
        if (GetComponent<RectTransform>().anchoredPosition != offsetPos) {
            GetComponent<RectTransform>().anchoredPosition = offsetPos;
        }
    }
}
