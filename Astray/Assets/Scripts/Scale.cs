using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public float width;
    public float height;
    public GameObject image;
    public void Selected() {
        var imageRectTransform = image.transform as RectTransform;
        imageRectTransform.sizeDelta = new Vector2 (width + width / height * 2, height + 2);
    }

    public void Deselected() {
        var imageRectTransform = image.transform as RectTransform;
        imageRectTransform.sizeDelta = new Vector2 (width, height);
    }
    public void Clicked() {
        StartCoroutine(Click());
    }
    IEnumerator Click() {
        var imageRectTransform = image.transform as RectTransform;
        imageRectTransform.sizeDelta = new Vector2 (width, height);
        yield return new WaitForSeconds(0.1f);
        imageRectTransform.sizeDelta = new Vector2 (width + width / height * 2, height + 2);
        yield return new WaitForSeconds(2f);
        imageRectTransform.sizeDelta = new Vector2 (width + width / height * 2, height + 2);
    }
}
