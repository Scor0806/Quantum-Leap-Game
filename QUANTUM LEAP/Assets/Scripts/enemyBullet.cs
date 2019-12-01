using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour {
    private GameObject playerPosition;
	public float speed = 20f;
	private int damage;
	public Rigidbody2D rb;
	public GameObject impactEffect;
    private int maxDamage = 10;
    private int minDamage = 5;

    private DemoAI pos;

    public float lifetime;

	// Use this for initialization
	void Start () {
        Destroy(gameObject,lifetime);

        damage = UnityEngine.Random.Range(minDamage, maxDamage);
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        if(playerPosition.transform.position.x < transform.position.x)
        {
            rb.velocity = -transform.right * speed;
            Debug.Log("Left");
        }
        else if(playerPosition.transform.position.x > transform.position.x)
        {
            rb.velocity = transform.right * speed;
            Debug.Log("Right");
        }
        //rb = GetComponent<Rigidbody2D>();
        //rb.velocity = -playerPosition.transform.right * speed;
        //transform.LookAt(playerPosition.transform);
        
        //Vector2 sp = playerPosition.transform.position;
        //Vector2 dir = sp.normalized;
        //rb.AddForce(dir * speed);
        //rb.velocity = (dir * speed)/ 2;
        //Debug.Log(rb.velocity);
    }


	void OnTriggerEnter2D (Collider2D hitInfo)
	{
        //Enemy enemy = hitInfo.GetComponent<Enemy>();
		if (hitInfo.CompareTag("Player"))
		{
            SoundManagerScript.PlaySound("ShotFired1");
            PlayerStats player = hitInfo.GetComponent<PlayerStats>();
            player.TakeDamage(damage);

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
	
}
