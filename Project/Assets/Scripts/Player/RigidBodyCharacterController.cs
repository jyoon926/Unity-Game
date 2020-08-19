using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyCharacterController : MonoBehaviour
{
    public bool CanJump;
    public float Speed;
    public float JumpHeight = 2f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float GroundDistance = 0.2f;
    public float jumpStrength;
    public LayerMask Ground;
    public Transform _camera;
    public float startAngle;
    public ParticleSystem dust;

    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    public bool _isGrounded = true;
    public bool _againstWall;
    public bool _isSwimming;
    public Transform _groundChecker;
    public Transform _wallChecker;

    private Vector3 direction;
    public float angle;
    private Animator anim;
    public string groundMaterial;
    private bool dustPlay;
    public bool falling = false;
    public bool AirControl;
    public float delay;
    public bool canControl;
    public bool learnedToSprint;
    public bool learnedToJump;
    float stopwatch;
    public bool foundRoad;
    public ConversationManager conversationManager;
    public Conversation lostAtStart;
    void Start()
    {
        stopwatch = 0f;
        learnedToSprint = false;
        learnedToJump= false;
        _body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //transform.rotation = Quaternion.Euler(0f, startAngle, 0f);
        dust.Play();
        canControl = true;
    }

    void Update()
    {
        if (!foundRoad)
            stopwatch += Time.deltaTime;
        if (stopwatch > 60f && !foundRoad && 
        !conversationManager.start) {
            conversationManager.StartDialogue(lostAtStart, 0f);
            foundRoad = true;
        }
        if (canControl) {
            _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground);
            _againstWall = Physics.CheckSphere(_wallChecker.position, 0.4f, Ground);
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            //Set angle and play animations
            if (!AirControl) {
                if (_isGrounded || _againstWall) {
                    if (transform.position.y > -5 && (Mathf.Abs(direction.x) > 0 || Mathf.Abs(direction.z) > 0f)) {
                        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    }
                }
            }
            else {
                if (transform.position.y > -5 && (Mathf.Abs(direction.x) > 0 || Mathf.Abs(direction.z) > 0f)) {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                }
            }
            if (CanJump) {
                //Grounded jumping
                if (_isGrounded) {
                    anim.SetBool("jumping", false);
                    if (Input.GetButtonDown("Jump")) {
                        if (!learnedToJump)
                            learnedToJump = true;
                        FindObjectOfType<AudioManager>().Play("Jump");
                        _body.AddForce(new Vector3(0, jumpStrength - _body.velocity.y, 0), ForceMode.Impulse);
                        falling = true;
                    }
                }
                else {
                    anim.SetBool("jumping", true);
                }
            }
            //Rotate player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            if (transform.position.y > -5 && ((Mathf.Abs(direction.x) > 0 || Mathf.Abs(direction.z) > 0f))) {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(_groundChecker.position.x, _groundChecker.position.y + 0.1f, _groundChecker.position.z), Vector3.down, out hit)) {
                    if (hit.collider.gameObject.tag == "Grass") {
                        groundMaterial = "Grass";
                    }
                    else if (hit.collider.gameObject.tag == "Concrete")
                    {
                        groundMaterial = "Concrete";
                    }
                    else if (hit.collider.gameObject.tag == "Wood")
                    {
                        groundMaterial = "Wood";
                    }
                }
                if (Input.GetKey(KeyCode.LeftShift)) {
                    if (!learnedToSprint)
                        learnedToSprint = true;
                    if (Speed < 10f) {
                        Speed += 0.1f;
                    }
                    if (Speed > 9.5f)
                        Speed = 10f;
                    anim.SetBool("running", true);
                    if (_isGrounded)
                        dustPlay = true;
                    else
                        dustPlay = false;
                }
                if (!Input.GetKey(KeyCode.LeftShift)) {
                    dustPlay = false;
                    if (Speed > 4f) {
                        Speed -= 0.1f;
                    }
                    if (Speed < 4.5f)
                        Speed = 4f;
                    anim.SetBool("running", false);
                }
                time += Time.deltaTime;
                Footstep();
            }
            else if (Mathf.Abs(direction.x) == 0 || Mathf.Abs(direction.z) == 0f) {
                dustPlay = false;
                time = delay;
                if (Speed > 4f) {
                    Speed -= 0.1f;
                }
                if (Speed < 4.5f)
                    Speed = 4f;
            }
            
            delay = Mathf.Clamp((Mathf.Clamp(Speed - 4f, 0f, 6f) / 6f) * -1f + 1f, 0f, 1f);
        }
        else {
            anim.SetBool("walking", false);
            anim.SetBool("running", false);
            anim.SetBool("jumping", false);
        }
    }

    //Move the player
    private void FixedUpdate() {
        if (canControl) {
            if (transform.position.y > -5 && ((Mathf.Abs(direction.x) > 0 || Mathf.Abs(direction.z) > 0f))) {
                Vector3 dir = transform.forward.normalized * Speed * Time.deltaTime;
                _body.MovePosition(transform.position + dir);
                anim.SetBool("walking", true);
            }
            else {
                anim.SetBool("walking", false);
            }
        }
        if (dustPlay) {
            if(!dust.isPlaying) {
                dust.Play();
            }
        }
        else {
            if(dust.isPlaying) {
                dust.Stop();
            }
        }
        if (falling) {
            StartCoroutine(Land());
        }
    }

    bool step = true;
    float time = 0f;

    void Footstep() {
        if (step) {
            if (!FindObjectOfType<AudioManager>().IsPlaying("Footstep Concrete") && !FindObjectOfType<AudioManager>().IsPlaying("Footstep Grass") && !FindObjectOfType<AudioManager>().IsPlaying("Footstep Wood") && _isGrounded) {
                FindObjectOfType<AudioManager>().Play("Footstep " + groundMaterial);
                time = 0f;
            }
            step = false;
        }
        if (time > delay * 0.25f + 0.2f)
            step = true;
    }

    IEnumerator Stop() {
        if(!dust.isPlaying) {
            dust.Play();
        }
        yield return new WaitForSeconds(0.25f);
        if (dust.isPlaying)
            dust.Stop();
    }
    IEnumerator Land() {
        yield return new WaitForSeconds(0.2f);
        if (_isGrounded) {
            falling = false;
            if (!FindObjectOfType<AudioManager>().IsPlaying("Footstep Concrete") && !FindObjectOfType<AudioManager>().IsPlaying("Footstep Grass") && !FindObjectOfType<AudioManager>().IsPlaying("Footstep Wood")) {
                FindObjectOfType<AudioManager>().Play("Footstep " + groundMaterial);
            }
            dust.Play();
        }
    }
    public void FoundRoad() {
        foundRoad = true;
    }
}