using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour {
    private GameObject playerPosition;
	public float speed = 20f;
	private int damage;
	public Rigidbody2D rb;
	public GameObject impactEffect;
    private int maxDamage = 30;
    private int minDamage = 10;

    private DemoAI pos;

	// Use this for initialization
	void Start () {
        damage = UnityEngine.Random.Range(minDamage, maxDamage);
        playerPosition = GameObject.FindGameObjectWithTag("Player");
        //rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.right * speed;
        transform.LookAt(playerPosition.transform);
        Vector2 sp = playerPosition.transform.position;
        Vector2 dir = sp.normalized;
        //rb.AddForce(dir * speed);
		rb.velocity = (dir * speed)/ 2;
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
