using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyCharacterController : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float GroundDistance = 0.2f;
    public LayerMask Ground;
    public Animator animator;
    public Transform camera;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    public bool _isGrounded = true;
    public Transform _groundChecker;

    private Vector3 direction;
    public float angle;
    AudioSource audioData;

    public Transform platformSpawn;
    public GameObject platform;
    public int platforms = 2;

    public Companion[] companions;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground);
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        
        if (Mathf.Abs(direction.x) > 0 || Mathf.Abs(direction.z) > 0f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            if (_isGrounded) {
                animator.SetBool("isRunning", true);
                audioData.UnPause();
            }
            else {
                animator.SetBool("isRunning", false);
                audioData.Pause();
            }
        }
        else {
            animator.SetBool("isRunning", false);
            audioData.Pause();
        }
        if (_isGrounded && Input.GetButtonDown("Jump")) {
            if (_body.velocity.y < 0) {
                _body.AddForce(new Vector3(0, _body.velocity.magnitude + 15f, 0), ForceMode.Impulse);
            }
            else {
                _body.AddForce(new Vector3(0, 15f - _body.velocity.magnitude, 0), ForceMode.Impulse);
            }
        }
        if (!_isGrounded && Input.GetButtonDown("Jump") && platforms > 0) {
            GameObject clone = (GameObject)Instantiate(platform, new Vector3(platformSpawn.position.x, platformSpawn.position.y, platformSpawn.position.z), Quaternion.identity);
            CompanionJump();
            if (_body.velocity.y < 0) {
                _body.AddForce(new Vector3(0, _body.velocity.magnitude + 15f, 0), ForceMode.Impulse);
            }
            else {
                _body.AddForce(new Vector3(0, 15f - _body.velocity.magnitude, 0), ForceMode.Impulse);
            }
            Destroy (clone, 1.0f);
            platforms--;
        }
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    private void FixedUpdate() {
        if ((Mathf.Abs(direction.x) > 0 || Mathf.Abs(direction.z) > 0f)) {
            Vector3 dir = transform.forward.normalized * Speed * Time.deltaTime;
            _body.MovePosition(transform.position + dir);
        }
    }

    private void CompanionJump() {
        for (int i = 0; i < companions.Length; i++) {
            companions[i].PlatformJump();
        }
    }
}