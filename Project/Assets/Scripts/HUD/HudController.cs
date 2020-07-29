using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite d;
    public Vector2 sheight;
    public Vector2 dheight;
    private int n;
    void Start()
    {
        n = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown("1")) {
            Select(1);
        }
        if (Input.GetKeyDown("2")) {
            Select(2);
        }
        if (Input.GetKeyDown("3")) {
            Select(3);
        }
        if (Input.GetKeyDown("4")) {
            Select(4);
        }
    }

    private void Select(int i) {
        if (n != i) {
            FindObjectOfType<AudioManager>().Play("Select");
            ImageFromNumber(i).sprite = SpriteFromNumber(i);
            ImageFromNumber(i).rectTransform.sizeDelta = sheight;
            ImageFromNumber(n).sprite = d;
            ImageFromNumber(n).rectTransform.sizeDelta = dheight;
        }
        n = i;
    }

    private Image ImageFromNumber(int i) {
        if (i == 1)
            return slot1;
        if (i == 2)
            return slot2;
        if (i == 3)
            return slot3;
        else
            return slot4;
    }

    private Sprite SpriteFromNumber(int i) {
        if (i == 1)
            return s1;
        if (i == 2)
            return s2;
        if (i == 3)
            return s3;
        else
            return s4;
    }
}