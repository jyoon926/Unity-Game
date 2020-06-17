using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystelCount : MonoBehaviour
{
    public Text text;
    public RigidBodyCharacterController player;

    void Update()
    {
        text.text = player.platforms.ToString();
    }
}
