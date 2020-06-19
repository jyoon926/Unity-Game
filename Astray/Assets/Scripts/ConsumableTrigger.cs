using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableTrigger : MonoBehaviour
{
    public Animator animator;
    private bool inside = false;
    public string type;
    public RigidBodyCharacterController player;
    public GameObject item;
    public Animator itemAnimator;
    public GameObject parent;
    public Crystals crystalsController;
    private void Update() {
        animator.SetBool("In", inside);
        if (inside && Input.GetKeyDown("e")) {
            if (player.platforms < 10 && player.energyTarget < 100f) {
                player.Eat();
                if (type.Equals("platform"))
                {
                    player.platforms++;
                    crystalsController.Check(player.platforms);
                }
                else if (type.Equals("berry"))
                {
                    if (player.energyTarget <= 80f)
                        player.energyTarget += 20f;
                    else
                        player.energyTarget = 100f;
                }
                StartCoroutine(Pressed());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            inside = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.name == "Player")
            inside = false;
    }

    IEnumerator Pressed() {
        inside = false;
        itemAnimator.SetBool("Disappear", true);
        yield return new WaitForSeconds(0.25f);
        Destroy(item);
        Destroy(parent);
    }
}
