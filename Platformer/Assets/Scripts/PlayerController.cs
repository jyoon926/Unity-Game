using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpStrength;
    public Transform groundCheck;
    public LayerMask ground;
    public LayerMask player1Layer;
    public LayerMask player2Layer;
    public bool isGrounded = false;
    public Rigidbody2D r2d;
    float moveDirection = 0;
    public Animator animator;
    bool facingRight;
    bool jumping;
    int jumpCount;
    public int player;

    void Start() {
        facingRight = true;
        Physics.IgnoreLayerCollision(9, 10);
    }
    void Update() {
        if (player == 1) {
            moveDirection = Input.GetAxisRaw("Player 1");
            if (Input.GetKeyDown(KeyCode.W) && jumpCount < 2) {
                r2d.AddForce(Vector2.up * (jumpStrength - r2d.velocity.y), ForceMode2D.Impulse);
                animator.SetBool("Jump", true);
                StartCoroutine(ResetJump());
                if (jumpCount == 1) {
                    animator.SetBool("Double", true);
                }
                jumpCount++;
            }
        }
        if (player == 2) {
            moveDirection = Input.GetAxisRaw("Player 2");
            if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2) {
                r2d.AddForce(Vector2.up * (jumpStrength - r2d.velocity.y), ForceMode2D.Impulse);
                animator.SetBool("Jump", true);
                StartCoroutine(ResetJump());
                if (jumpCount == 1) {
                    animator.SetBool("Double", true);
                }
                jumpCount++;
            }
        }
        
        if (Mathf.Abs(moveDirection) > 0) {
            animator.SetBool("Run", true);
        }
        else {
            animator.SetBool("Run", false);
        }
        if (moveDirection < 0 && facingRight) {
            Flip();
        }
        if (moveDirection > 0 && !facingRight) {
            Flip();
        }
        if (jumping && isGrounded) {
            animator.SetBool("Jump", false);
            jumping = false;
            jumpCount = 0;
        }
    }

    void Flip() {
        facingRight = !facingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void FixedUpdate() {
        if (player == 1) {
            isGrounded = Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheck.position.y), 0.1f, ground) || Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheck.position.y), 0.1f, player2Layer);
        }
        if (player == 2) {
            isGrounded = Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheck.position.y), 0.1f, ground) || Physics2D.OverlapCircle(new Vector2(groundCheck.position.x, groundCheck.position.y), 0.1f, player1Layer);
        }
        r2d.velocity = new Vector2((moveDirection) * speed, r2d.velocity.y);
    }

    IEnumerator ResetJump() {
        yield return new WaitForSeconds(0.1f);
        jumping = true;
    }
}