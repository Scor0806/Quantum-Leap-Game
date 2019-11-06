using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private PlayerStats player;
	public int health = 100;
    //add animiation
    private Rigidbody2D myrigidbody2D;
    public float knockback;
    public float knockback_length;
    public float knockback_count;
    public bool knockFromLeft;
    public bool knockFromRight;
    public Transform dmgPosition;

	public GameObject deathEffect;
    public GameObject xp;

    [SerializeField]
    private GameObject sPreFab;



    private void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        dmgPosition = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    public void TakeDamage (int damage)
	{
        health -= damage;
        if (health <= 0)
		{
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
        if (other.tag == "Player")
        {
            Vector3 direction = (this.transform.position - other.transform.position).normalized;
            StartCoroutine(player.Knockback(0.02f, 300, direction));
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
