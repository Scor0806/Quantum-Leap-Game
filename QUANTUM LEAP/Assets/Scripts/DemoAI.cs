using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAI : MonoBehaviour{
    private float speed = 3f;
    private Animator anim;
    private bool spotted = false;
    public float attackRange;
    public float longRange;
    public LayerMask whatIsTarget;
    private float currentTarget;

    public Transform firePoint;
    public GameObject projectile;
    private Transform player;
    private float MAX_DISTANCE = 5f;

    //alexis
    private Transform target;
    private float distanceFromPlayer = 3; //can be changed in inspector
    private Vector3 targetPos;
    private Vector3 thisPos;


    //moving only if in range
    private float attackRate = 3f;
    private float nextAttack = 0;

    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        anim = GetComponent<Animator>();
        anim.SetBool("Moving", false);
        //Debug.Log("Player is " + Mathf.Abs(this.transform.position.x - player.position.x) + " units away from enemy");
    }

    private void Flip(){
        Vector3 targetPos = player.transform.position;
        Vector3 targetPosFlattened = new Vector3(0, targetPos.y, 0);
        transform.LookAt(targetPosFlattened);
        //transform.Rotate(0f, 180f, 0f);
    }

    void Update(){
        //StartCoroutine(WaitAFew());
        anim.SetFloat("Speed", Mathf.Abs(speed));
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            currentTarget = Mathf.Abs(this.transform.position.x - player.position.x);
            //if inside longrange then follow
            if (Physics2D.OverlapCircle(transform.position, longRange, whatIsTarget))
            {

                StartCoroutine(WaitAFew());
            }
            //if inside attackrange then shoot
            if (Physics2D.OverlapCircle(transform.position, longRange, whatIsTarget) &&
                Physics2D.OverlapCircle(transform.position, attackRange, whatIsTarget))
            {
                Shoot();
            }
        }
    }
  IEnumerator WaitAFew()
    {

            //waiting so the enemy doesnt follow right away
            yield return new WaitForSeconds(0.3f);
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            anim.SetBool("Moving", true);
            //flips direction of the enemy to follow in another direction
            if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(4f, 4f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-4f, 4f, 1f);

            }
            //move towards player 
            if (Vector2.Distance(transform.position, target.position) > distanceFromPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }
    
    IEnumerator ShootingCoolDown()
    {
        yield return new WaitForSeconds(3f);
    }

    IEnumerator AnimationCoolDown()
    {
        yield return new WaitForSeconds(.25f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, longRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

    public void Shoot()
    {
        //Debug.Log("Enenmy Firing");
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            StartCoroutine(AnimationCoolDown());
            StartCoroutine(ShootingCoolDown());
        }
    }
}
