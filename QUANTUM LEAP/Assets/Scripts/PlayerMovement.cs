using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //test
    private laserStrike laserAttack;
    public GameObject cameraShake;
    private CineCameraShake cam;

	public CharacterController2D controller;
	public Animator animator;
    public Transform firePoint;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
    bool grounded = false;

    private bool isJumping;         //  Keeps track of jumping animation of character
    private bool fpChanged;         //  Keeps track in the change in transform of firePoint

    private void Awake()
    {
        cam = cameraShake.GetComponent<CineCameraShake>();
    }

    //  CALLED BEFORE FIRST FRAME UPDATE
    void Start(){

        //  START INITIALIZATIONS
        isJumping = animator.GetBool("IsJumping");

        laserAttack = GetComponent<laserStrike>();
    }

    // UPDATE CALLED ONCE PER FRAME
    void Update () {
        /*if (transform.right.x > 0)
        {
            Debug.Log("Flip right");
        }
        else if (transform.right.x < 0)
        {
            Debug.Log("flip left");
        }*/
        //  UPDATE INITIALIZATIONS
        isJumping = animator.GetBool("IsJumping");
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            laserAttack.ThrowTargeter();
        }

        //  JUMP
		if (Input.GetButtonDown("Jump") )
		{
			jump = true;
            crouch = false;
            grounded = false;
			animator.SetBool("IsJumping", jump);
            animator.SetBool("IsGrounded", false);
        }

        if (Input.GetButton("Jump"))
        {
            jump = true;
            crouch = false;
            grounded = false;
            animator.SetBool("IsJumping", jump);
            animator.SetBool("IsGrounded", false);
        }

        // TRANSFORM INTO TANK
        if (Input.GetKeyDown(KeyCode.X))
        {
            //animator
        }

        //  CROUCH
        if (Input.GetButtonDown("Crouch") && !Input.GetButtonDown("Jump"))
        {
            crouch = true;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        //  CHANGE IN FIREPOINT POSITION
        if (Input.GetKeyDown(KeyCode.S))
        {
            firePoint.position += new Vector3(0, -.5f, -0);
            fpChanged = true;
        }
        if (Input.GetKeyUp(KeyCode.S) && fpChanged)
        {
            firePoint.position += new Vector3(0, .5f, 0);
            fpChanged = false;
        }

    }

	public void OnLanding ()
	{
        grounded = false; 
		animator.SetBool("IsJumping", false);
        animator.SetBool("IsGrounded", true);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
    }

	public void FixedUpdate ()
	{
		// MOVE CHARACTER
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
    }
    
    IEnumerator ShakeDuration()
    {
        cam.activated = true;
        yield return new WaitForSeconds(1f);
        cam.activated = false;
    }
}