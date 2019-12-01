using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public enum enemyType {RANGED, MELEE};
    public enemyType currentType;
    private PlayerStats player;
	public int health = 100;
    //add animiation
    private Rigidbody2D myrigidbody2D;
    public float knockback;
    public float knockback_length;
    public float knockback_count;
    public bool knockFromLeft;
    public bool knockFromRight;

	public GameObject deathEffect;
    private GameObject DamagePopup;

    [SerializeField]
    private GameObject sPreFab;
    private GameObject playerPosition;
    private Transform playerLocation;



    private void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void TakeDamage (int damage)
	{
        health -= damage;
        dmgPopup.Create(transform.position, damage, false);
        Vector2 direction = (playerLocation.transform.position - transform.position);
        myrigidbody2D.AddForce(-direction * 20);
        if (health <= 0)
		{
            Score.scoreValue += 10;
            Instantiate(sPreFab, transform.position,transform.rotation);
            Die();
        }
        //Instantiate(xp, transform.position, Quaternion.identity);

    }

    void Die ()
	{
        if (gameObject != null)
        {
            SoundManagerScript.PlaySound("EnemyDeath1");
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            //Instantiate(xp, transform.position, transform.rotation);
            Destroy(gameObject);
            // Instantiate(sPreFab, transform.position, transform.rotation);
            
        }
	}

    public void hitByMelee()
    {
        Debug.Log("Enemy hit by melee attack !");
        if (knockback_count <= 0)
        {
            
        }
        else
        {
            if (knockFromRight)
            {
                myrigidbody2D.velocity = new Vector2(-knockback, knockback);
            }
            else
            {
                myrigidbody2D.velocity = new Vector2(knockback, -knockback);
            }
            knockback_count -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && currentType == enemyType.RANGED)
        {
            Vector3 direction = (this.transform.position - other.transform.position).normalized;
            playerPosition = GameObject.FindGameObjectWithTag("Player");
            /*if (playerPosition.transform.position.x < transform.position.x)
            {
                playerPosition.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500, 250));
            }
            else if (playerPosition.transform.position.x > transform.position.x)
            {
                playerPosition.GetComponent<Rigidbody2D>().AddForce(new Vector2(500, 250));

            }*/
            //playerPosition.GetComponent<PlayerStats>().TakeDamage(10);
            StartCoroutine(playerPosition.GetComponent<PlayerStats>().Knockback(0.5f, 2, transform));
            StartCoroutine(AttackCool());
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public IEnumerator AttackCool()
    {
        GetComponent<DemoAI>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        GetComponent<DemoAI>().enabled = true;
    }
}
