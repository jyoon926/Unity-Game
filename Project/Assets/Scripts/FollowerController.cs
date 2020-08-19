using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowerController : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float sorrow;
    public Slider slider;
    public GameObject Player;
    public RigidBodyCharacterController PlayerController;
    private Rigidbody Body;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Animator animator;
    public float DistanceFromPlayer;
    private float distance;
    private bool isGrounded = true;
    public Transform GroundChecker;
    public Transform WallChecker;
    public LayerMask Ground;
    public float jumpStrength;
    private bool jumping = false;
    private int n;
    public ExclamationMarkPosition exclamationMark;
    public ConversationManager conversationManager;

    void Start()
    {
        jumping = false;
        Body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sorrow = 0.1f;
    }

    void Update()
    {
        // if (sorrow < 1f) {
        //     sorrow += Time.deltaTime * 0.1f;
        // }
        isGrounded = Physics.CheckSphere(GroundChecker.position, 0.3f);
        float targetAngle = Mathf.Atan2(Player.transform.position.z - transform.position.z, Player.transform.position.x - transform.position.x) * 180 / Mathf.PI;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, -1 * targetAngle + 90, ref turnSmoothVelocity, turnSmoothTime);
        distance = Vector2.Distance(new Vector2(Player.transform.position.x, Player.transform.position.z), new Vector2(transform.position.x, transform.position.z));
        if (distance > 10f) {
            exclamationMark.opacity = 1;
        }
        else if (distance < 5f) {
            exclamationMark.opacity = 0;
        }

        if (PlayerController.CanJump) {
            //Jump when player jumps
            if (!jumping && n < 1 && Input.GetButtonDown("Jump") && PlayerController._isGrounded) {
                jumping = true;
                n++;
                StartCoroutine(Jump(Player.transform.position, transform.position));
            }
        }
        
        //Jump when detects wall
        if (isGrounded && !jumping && Physics.CheckSphere(WallChecker.position, 0.01f, Ground)) {
            Body.AddForce(new Vector3(0, jumpStrength - Body.velocity.y, 0), ForceMode.Impulse);
            jumping = true;
            n++;
            StartCoroutine(ResetJump());
        }

        //Rotation towards player
        if (distance > DistanceFromPlayer) {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        if (isGrounded)
            jumping = false;
    }

    private void FixedUpdate() {
        //Foundational movement
        if (distance > DistanceFromPlayer) {
            float speed = (distance * 1.2f) + 3f;
            Vector3 direction = transform.forward.normalized * Mathf.Clamp(speed, 0f, 20f) * Time.deltaTime;
            Body.MovePosition(transform.position + direction);
            if (isGrounded)
            {
                animator.SetBool("running", true);
            }
            else
            {
                animator.SetBool("running", false);
            }
        }
        else {
            animator.SetBool("running", false);
        }
    }

    IEnumerator Jump(Vector3 playerPos, Vector3 originalPos) {
        float d = Vector2.Distance(new Vector2(playerPos.x, playerPos.z), new Vector2(originalPos.x, originalPos.z));
        while (d > Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(originalPos.x, originalPos.z)))
        {
            yield return null;
        }
        if (isGrounded)
        {
            Body.AddForce(new Vector3(0, jumpStrength - Body.velocity.y, 0), ForceMode.Impulse);
            StartCoroutine(ResetJump());
        }
    }

    IEnumerator ResetJump() {
        yield return new WaitForSeconds(0.25f);
        jumping = false;
        n = 0;
    }
}
