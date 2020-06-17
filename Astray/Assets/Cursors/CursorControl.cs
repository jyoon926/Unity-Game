using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public static CursorControl instance;

    public Texture2D pointer, crosshair;
    private Vector2 mouse;
    public float width;

    private void Awake() {
        instance = this;
        width = 80f;
        DontDestroyOnLoad(gameObject);
    }
    
    public void ActivatePointer() {
        //Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
    }

    private void Update() {
        mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        Cursor.visible = false;
    }

    private void OnGUI() {
        GUI.DrawTexture(new Rect(mouse.x, mouse.y, width, width), pointer);
    }
}
