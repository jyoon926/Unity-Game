using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public RigidBodyCharacterController player;
    public RectTransform img;
    void Update()
    {
        img.sizeDelta = new Vector2(Mathf.RoundToInt(player.energy), 1f);
    }
}
