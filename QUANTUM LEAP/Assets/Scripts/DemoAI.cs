using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAI : MonoBehaviour{
    public float speed = 20f;
    private Animator anim;
    public float retreatDistance;
    public float stoppingDistance;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public bool spotted = false;
    public float attackRange;
    public float longRange;
    public Transform rangePoint;
    public LayerMask whatIsTarget;
    private float currentTarget;

    public GameObject projectile;
    public Transform player, sightStart, sightEnd, firePoint;
    private float MAX_DISTANCE = 5f;

    //alexis
    private Transform target;
    public float distanceFromPlayer = 3; //can be changed in inspector
    private Vector3 targetPos;
    private Vector3 thisPos;


    //moving only if in range
    public float lookRadius = 5f;
    private float attackRate = 3f;
    private float nextAttack = 0;

    // void OnDrawGizmosSelected(){
    //     Gizmos color = Color.red;
    //     Gizmos DrawWireSphere(transform.position, lookRadius);
    // }



    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        anim = GetComponent<Animator>();
        timeBtwShots = startTimeBtwShots;
        //Debug.Log("Player is " + Mathf.Abs(this.transform.position.x - player.position.x) + " units away from enemy");
    }

    // Update is called once per frame
    bool RayCasting(){
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);

        spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));

        return spotted;
    }

    void Behaviours() {
        
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
        currentTarget = Mathf.Abs(this.transform.position.x - player.position.x);
        //if inside longrange then follow
        if (Physics2D.OverlapCircle(sightStart.position, longRange, whatIsTarget))
        {
            
            StartCoroutine(WaitAFew());
        }
        //if inside attackrange then shoot
        if (Physics2D.OverlapCircle(sightStart.position, longRange, whatIsTarget) &&
            Physics2D.OverlapCircle(sightStart.position, attackRange, whatIsTarget))
        {
            Shoot();
        }

        }
  IEnumerator WaitAFew()
    {

            //waiting so the enemy doesnt follow right away
            yield return new WaitForSeconds(0.3f);

            //flips direction of the enemy to follow in another direction
            if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(4f,4f,1f);
                //transform.localScale = new Vector3(4f, 4f, 1f);
                //Vector3 currRot = transform.eulerAngles;
                //transform.localxscale = -1;
            }
            else
            {
                transform.localScale = new Vector3(-4f,4f,1f);

            }
            //move towards player 
            if (Vector2.Distance(transform.position, target.position) > distanceFromPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        
    }
    
    IEnumerator ShootingCoolDown()
    {
        yield return new WaitForSeconds(3f);
        anim.SetBool("Firing", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(sightStart.position, longRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sightStart.position, attackRange);

    }

    public void Shoot()
    {
        //Debug.Log("Enenmy Firing");
        if (Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            anim.SetBool("Firing", true);
            Instantiate(projectile, firePoint.position, firePoint.rotation);
            StartCoroutine(ShootingCoolDown());
        }
    }


  
    public void playerDetetion()
    {
        Collider2D enemiesToDamage = Physics2D.OverlapCircle(sightStart.position, longRange, whatIsTarget);
    }
}
