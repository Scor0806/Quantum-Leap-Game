using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private bool following = false;
    public float sightRadius, meleeRadius;
    public float bossSpeed = 5f;
    Animator anim;
    PlayerStats player;
    private float attackRate = 3f;
    private float nextAttack = 0f;
    bool facingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(bossSpeed));
        StartCoroutine(Follow());
        ShootRangeCheck();
        MeleeRangeCheck();
    }

    private void ShootRangeCheck()
    {
        if (Physics2D.OverlapCircle(transform.position, sightRadius, LayerMask.GetMask("Player")) && 
            !Physics2D.OverlapCircle(transform.position, meleeRadius, LayerMask.GetMask("Player")))
        {
            //shoot
            StartCoroutine(Fire());
            
        }
    }

    private void MeleeRangeCheck()
    {
        if (Physics2D.OverlapCircle(transform.position, meleeRadius, LayerMask.GetMask("Player")))
        {
            //melee
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                StartCoroutine(Melee());
                player.TakeDamage(15);
            }
        }
    }

    IEnumerator Follow()
    {
        following = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (player.transform.position.x > transform.position.x && !facingRight)
            {
                //transform.Rotate(0f, 180f, 0f);
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, 1f);
                facingRight = true;
            }
            else if(player.transform.position.x < transform.position.x && facingRight)
            {
                //transform.Rotate(0f, 180f, 0f);
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, 1f);
                facingRight = false;
            }
            if (Vector2.Distance(transform.position, player.transform.position) > 2.5f)
            {
                anim.SetBool("Moving", true);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, bossSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("Moving", false);
            }
        }
            
            following = false;
    }

    IEnumerator Fire()
    {
        anim.SetBool("inShotRange", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("inShotRange", false);
    }

    IEnumerator Melee()
    {
        anim.SetBool("inMeleeRange", true);
        anim.SetBool("Moving", false);
        yield return new WaitForSeconds(1f);
        anim.SetBool("inMeleeRange", false);
        anim.SetBool("Moving", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, meleeRadius);
    }
}
