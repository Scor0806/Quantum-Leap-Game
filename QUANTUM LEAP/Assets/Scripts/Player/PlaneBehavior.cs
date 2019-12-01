using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float maxVelocity = 15;
    public float rotationSpeed = 50;
    private bool facingRight;

    private Animator anim;
    private CharacterController2D characterController;
    void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController2D>();
        
    }


    // Update is called once per frame
    void Update()
    {
        
        float yAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftArrow) && facingRight)
        {
            characterController.m_FacingRight = facingRight;
            Flip();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !facingRight)
        {
            characterController.m_FacingRight = facingRight;
            Flip();
        }
        ClampVelocity();
        ThrustForward(xAxis * 10);
        ThrustUp(yAxis * 10);
    }
    private void FixedUpdate()
    {
        ClampVelocity();
    }

    void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
        //Debug.Log(rb.velocity);

    }

    void ThrustForward(float amount)
    {
        Vector2 force = transform.right * amount;
        
        if (facingRight)
        {
            rb.AddForce(force);
        }
        else if (!facingRight)
        {
            rb.AddForce(-1 * force);
        }

        if (transform.right.x < 0)
        {
            facingRight = false;
            //Flip();
        }
        else if (transform.right.x > 0)
        {
            facingRight = true;
            //Flip();
        }
        anim.SetFloat("speed", force.x);
        //Debug.Log
    }

    void ThrustUp(float amount)
    {
        Vector2 force = transform.up * amount;
        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, 0, amount);
    }

    private void FaceCorrectDirection()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void OnEnable()
    {
        
        if (transform.right.x < 0)
        {
            facingRight = false;
            characterController.m_FacingRight = facingRight;
        }
        else if (transform.right.x > 0)
        {
            facingRight = true;
            characterController.m_FacingRight = facingRight;
        }
        Debug.Log("Transform Facing Right? " + facingRight);
    }
    void Flip()
    {
        facingRight = !facingRight;
        characterController.m_FacingRight = !characterController.m_FacingRight;

        transform.Rotate(0, 180f, 0);
    }
}
