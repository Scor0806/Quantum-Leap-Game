using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
    public Transform firePoint;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

    private bool isJumping;         //  Keeps track of jumping animation of character
    private bool fpChanged;         //  Keeps track in the change in transform of firePoint

    //  CALLED BEFORE FIRST FRAME UPDATE
    void Start(){

        //  START INITIALIZATIONS
        isJumping = animator.GetBool("IsJumping");
    }

    // UPDATE CALLED ONCE PER FRAME
    void Update () {

        //  UPDATE INITIALIZATIONS
        isJumping = animator.GetBool("IsJumping");
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //  JUMP
		if (Input.GetButtonDown("Jump") )
		{
			jump = true;
            crouch = false;
			animator.SetBool("IsJumping", jump);
            animator.SetBool("IsGrounded", false);
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


}