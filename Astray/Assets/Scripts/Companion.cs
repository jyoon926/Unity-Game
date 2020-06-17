using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public GameObject Player;
    public RigidBodyCharacterController PlayerController;
    public Rigidbody Body;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Animator animator;
    public float DistanceFromPlayer;
    private float distance;
    private bool isGrounded = true;
    public Transform GroundChecker;
    public Transform WallChecker;
    public LayerMask Ground;
    public LayerMask Platform;
    bool jumping = false;
    private int n;
    public InDialogue playerDialogue;
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundChecker.position, 0.01f, Ground);
        float targetAngle = Mathf.Atan2(Player.transform.position.z - transform.position.z, Player.transform.position.x - transform.position.x) * 180 / Mathf.PI;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, -1 * targetAngle + 90, ref turnSmoothVelocity, turnSmoothTime);
        distance = Vector3.Distance(Player.transform.position, transform.position);

        if (Physics.CheckSphere(WallChecker.position, 0.01f, Ground) && !jumping) {
            Body.AddForce(new Vector3(0, 15f, 0), ForceMode.Impulse);
            jumping = true;
            StartCoroutine(ResetJump());
        }

        if (!playerDialogue.inDialogue && Input.GetButtonDown("Jump") && PlayerController._isGrounded) {
            StartCoroutine(Jump());
        }

        if (Physics.CheckSphere(GroundChecker.position, 0.01f, Platform)) {
            Body.AddForce(new Vector3(0, 20f, 0), ForceMode.Impulse);
        }

        if (distance > DistanceFromPlayer) {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            float speed = distance + 2f;
            Vector3 direction = transform.forward.normalized * speed * Time.deltaTime;
            Body.MovePosition(transform.position + direction);
            if (isGrounded)
            {
                animator.SetBool("isRunning", true);
                audioData.UnPause();
            }
            else
            {
                animator.SetBool("isRunning", false);
                audioData.Pause();
            }
        }
        else {
            animator.SetBool("isRunning", false);
            audioData.Pause();
        }
    }

    IEnumerator Jump() {
        yield return new WaitForSeconds(distance / 10);
        if (!jumping)
        {
            if (Body.velocity.y < 0) {
                Body.AddForce(new Vector3(0, 15f + Body.velocity.magnitude, 0), ForceMode.Impulse);
            }
            else {
                Body.AddForce(new Vector3(0, 15f - Body.velocity.magnitude, 0), ForceMode.Impulse);
            }
            jumping = true;
            StartCoroutine(ResetJump());
        }
    }

    public void PlatformJump() {
        StartCoroutine(InstantJump());
    }
    IEnumerator InstantJump() {
        Debug.Log("Jump");
        yield return new WaitForSeconds(distance / 10);
        if (Body.velocity.y < 0) {
            Body.AddForce(new Vector3(0, 15f + Body.velocity.magnitude, 0), ForceMode.Impulse);
        }
        else {
            Body.AddForce(new Vector3(0, 15f - Body.velocity.magnitude, 0), ForceMode.Impulse);
        }
    }
    IEnumerator ResetJump() {
        yield return new WaitForSeconds(1);
        jumping = false;
    }
}
