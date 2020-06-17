using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float velocity = 5;
    public Vector3 vel;
    public float turnSpeed = 10;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Animator anim;
    public CharacterController controller;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector2 input;
    float angle;
    bool isGrounded;
    bool moving;

    Quaternion targetRotation;
    Transform cam;

    AudioSource audioData;
    
    void Start() {
        cam = Camera.main.transform;
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }

    void Update() {     
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && vel.y < 0) {
            vel.y = -2;
            anim.SetBool("isFalling", false);
        }
        else {
            anim.SetBool("isFalling", true);
        }
        if (Input.GetButtonDown("Jump") && isGrounded) {
            vel.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        
        //Get Input
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        CalculateDirection();
        Rotate();
        Move();
    }

    void CalculateDirection() {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    void Rotate() {
        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void Move() {
        if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0) {
            controller.Move(transform.forward * velocity * Time.deltaTime);
            moving = true;
            audioData.UnPause();
        }
        else {
            moving = false;
            audioData.Pause();
        }
        if (moving && isGrounded) {
            anim.SetBool("isRunning", true);
        }
        else {
            anim.SetBool("isRunning", false);
        }
        vel.y += gravity * Time.deltaTime;
        controller.Move(vel * Time.deltaTime);
    }
}