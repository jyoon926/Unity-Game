using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMarkPosition : MonoBehaviour
{
    public Transform player;
    public Transform bird;
    public Transform cameraTarget;
    public Vector2 cornerPosition;
    float cornerAngle;
    public float opacity;
    private void Start() {
        cornerAngle = Mathf.Atan2(cornerPosition.y, cornerPosition.x) * Mathf.Rad2Deg;
    }
    void Update()
    {
        Vector2 birdScreenPosition = Camera.main.WorldToScreenPoint(bird.position);
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(player.position);
        float angle = Mathf.Atan2(birdScreenPosition.y - 112.5f, birdScreenPosition.x - 200f) * Mathf.Rad2Deg + 180f;
        angle += 180f;
        if (angle >= 360f) {
            angle -= 360f;
        }
        Vector2 targetPosition;
        if (birdScreenPosition.x < 200f + cornerPosition.x && birdScreenPosition.x > 200f - cornerPosition.x && birdScreenPosition.y < 112.5f + cornerPosition.y && birdScreenPosition.y > 112.5f - cornerPosition.y) {
            targetPosition = new Vector2(birdScreenPosition.x - 200f, (birdScreenPosition.y + 30f) - 112.5f);
        }
        else {
            if (angle < cornerAngle || angle > (360f - cornerAngle)) {
                targetPosition = new Vector2(cornerPosition.x, Mathf.Tan(angle * Mathf.Deg2Rad) * (cornerPosition.x));
            }
            else if (angle >= cornerAngle && angle <= 180f - cornerAngle) {
                targetPosition = new Vector2(cornerPosition.y / Mathf.Tan(angle * Mathf.Deg2Rad), cornerPosition.y);
            }
            else if (angle > 180f - cornerAngle && angle < 180f + cornerAngle) {
                targetPosition = new Vector2(-cornerPosition.x, Mathf.Tan(angle * Mathf.Deg2Rad) * (-cornerPosition.x));
            }
            else {
                targetPosition = new Vector2(-(cornerPosition.y / Mathf.Tan(angle * Mathf.Deg2Rad)), -cornerPosition.y);
            }
        }

        float targetOpacity = Mathf.Lerp(GetComponent<CanvasGroup>().alpha, opacity, Time.deltaTime * 10f);
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, targetPosition, Time.deltaTime * 20f);
        GetComponent<CanvasGroup>().alpha = targetOpacity;
    }
}
