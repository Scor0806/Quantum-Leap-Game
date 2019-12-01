using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private float speed = 3.5f;
    private Transform target;
    private PlayerStats player;

    public float attackRange;
    public float sightRange;
    public Transform sightStart;
    private LayerMask whatIsTarget;

    private float distanceFromPlayer = 2f;
    private float attackRate = 1.5f;
    private float nextAttack = 0f;

    private Rigidbody2D rb;
    private RigidbodyConstraints2D rotate;
    void Awake()
    {
        whatIsTarget = LayerMask.GetMask("Player");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim = GetComponent<Animator>();
      
       
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(speed));
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (Physics2D.OverlapCircle(transform.position, sightRange, whatIsTarget))
            {
                StartCoroutine(Follow());
            }
            if (Physics2D.OverlapCircle(transform.position, attackRange, whatIsTarget) &&
                Physics2D.OverlapCircle(transform.position, sightRange, whatIsTarget))
            {
                Attack();
            }
        }
    }
    IEnumerator Follow()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Moving", true);
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (target.position.x > transform.position.x)
            {
                //transform.Rotate(0f, 180f, 0f);
                transform.localScale = new Vector3(-7f, 7f, 1f);
            }
            else
            {
                //transform.Rotate(0f, 180f, 0f);
                transform.localScale = new Vector3(7f, 7f, 1f);
            }
            if (Vector2.Distance(transform.position, target.position) > distanceFromPlayer)
            {
                anim.SetBool("inRange", true);
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("inRange", false);

            }
        }
        
    }

    public void Attack()
    {
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;

            player.TakeDamage(8);
            anim.SetBool("isAttacking", true);
            StartCoroutine(AttackCoolDown());
        }
    }

    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("isAttacking", false);
    }

    void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        //Rigidbody rbdy = collision.gameObject.GetComponent<Rigidbody>();

        //Stop Moving/Translating
        rb.velocity = Vector3.zero;

        //Stop rotating
        rb.angularVelocity = 0;
    }
}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(sightStart.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sightStart.position, attackRange);

    }
}
