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
    public float jumpStrength;
    bool jumping = false;
    private int n;
    public InDialogue playerDialogue;
    AudioSource audioData;
    private bool platformJump;
    private float heightOfJump;
    private float selfHeightOfJump;
    private float playerJumpDistance;
    Vector3 previousJumpPosition;
    Vector3 playerPos;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        platformJump = false;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundChecker.position, 0.01f, Ground);
        float targetAngle = Mathf.Atan2(Player.transform.position.z - transform.position.z, Player.transform.position.x - transform.position.x) * 180 / Mathf.PI;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, -1 * targetAngle + 90, ref turnSmoothVelocity, turnSmoothTime);
        distance = Vector2.Distance(new Vector2(Player.transform.position.x, Player.transform.position.z), new Vector2(transform.position.x, transform.position.z));

        //Jump when detects wall
        if (Physics.CheckSphere(WallChecker.position, 0.01f, Ground) && isGrounded) {
            Body.AddForce(new Vector3(0, jumpStrength, 0), ForceMode.Impulse);
        }

        //Jump when player jumps
        if (!playerDialogue.inDialogue && Input.GetButtonDown("Jump") && PlayerController._isGrounded && PlayerController.jumpCount < 2) {
            PlayerController.ResetJumpCount();
            StartCoroutine(Jump(Player.transform.position, transform.position));
        }

        //Rotation towards player
        if (distance > DistanceFromPlayer) {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        //Platform Jump
        if (platformJump) {
            if (playerJumpDistance - Vector2.Distance(new Vector2(previousJumpPosition.x, previousJumpPosition.z), new Vector2(transform.position.x, transform.position.z)) < 0f) {
                Body.AddForce(new Vector3(0, jumpStrength - Body.velocity.y, 0f), ForceMode.Impulse);
                PlayerController.ResetJumpCount();
                previousJumpPosition = transform.position;
                platformJump = false;
            }
        }
    }

    private void FixedUpdate() {
        //Foundational movement
        if (distance > DistanceFromPlayer) {
            float speed = (distance * 1.2f) + 3f;
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

    IEnumerator Jump(Vector3 playerPos, Vector3 originalPos) {
        float d = Vector2.Distance(new Vector2(playerPos.x, playerPos.z), new Vector2(originalPos.x, originalPos.z));
        while (d > Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(originalPos.x, originalPos.z)))
        {
            yield return null;
        }
        if (isGrounded)
        {
            previousJumpPosition = transform.position;
            Body.AddForce(new Vector3(0, jumpStrength - Body.velocity.y, 0), ForceMode.Impulse);
            //jumping = true;
            //StartCoroutine(ResetJump());
        }
    }

    public void PlatformJump(float height, float distance, Vector3 playerpos) {
        heightOfJump = height;
        platformJump = true;
        selfHeightOfJump = transform.position.y;
        playerJumpDistance = distance;
        playerPos = playerpos;
    }
    IEnumerator ResetJump() {
        yield return new WaitForSeconds(0.25f);
        jumping = false;
    }
}
