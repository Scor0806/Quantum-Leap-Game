using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Movement : MonoBehaviour
{
    public Animator anim;
    private float speed = 20f;
    private Transform player, target;

    public float attackRange;
    public float detonateRange;
    public float sightRange;
    public Transform rangePoint, sightStart;
    public LayerMask whatIsTarget;

    public float distanceFromPlayer = 3;
    public AIPath pathFinder;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pathFinder = gameObject.GetComponent<AIPath>();
        pathFinder.enabled = false;
    }

    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(speed));
        //currentTarget = Mathf.Abs(this.transform.position.x - player.position.x);
        //if inside longrange then follow
        //pathFinder.enabled = false;
        if (Physics2D.OverlapCircle(sightStart.position, sightRange, whatIsTarget))
        {

            StartCoroutine(Follow());
        }
        //if inside blast zone then detonate
        if (Physics2D.OverlapCircle(sightStart.position, sightRange, whatIsTarget) &&
            Physics2D.OverlapCircle(sightStart.position, attackRange, whatIsTarget))
        {
            StartCoroutine(GetReadyToDetonate());
        }
        if (Physics2D.OverlapCircle(sightStart.position, sightRange, whatIsTarget) &&
            Physics2D.OverlapCircle(sightStart.position, attackRange, whatIsTarget) &&
            Physics2D.OverlapCircle(sightStart.position, detonateRange, whatIsTarget))
        {
            Detonate();
            
        }
     }
    IEnumerator GetReadyToDetonate()
    {
        yield return new WaitForSeconds(0f);
        anim.SetBool("inAttackRange", true);
        anim.SetBool("inRange", true);
    }
    IEnumerator Follow()
    {

        //waiting so the enemy doesnt follow right away
        yield return new WaitForSeconds(0f);
        if (target.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-4f, 4f, 1f);
        }
        else
        {

            transform.localScale = new Vector3(4f, 4f, 1f);

        }
        anim.SetBool("inAttackRange", false);
        anim.SetBool("inRange", true);
        pathFinder.enabled = true;

    }
    void Detonate()
    {
        player.GetComponent<PlayerStats>().TakeDamage(20);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(sightStart.position, sightRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sightStart.position, attackRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sightStart.position, detonateRange);

    }
}
