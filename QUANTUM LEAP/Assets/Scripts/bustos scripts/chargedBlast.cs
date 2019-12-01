using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargedBlast : MonoBehaviour
{
  
    public float speed = 20f;
    public int damage;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    //private Enemy target;
    public float lifetime;




    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject,lifetime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            SoundManagerScript.PlaySound("ShotFired1");
            Enemy target = hitInfo.GetComponent<Enemy>();
            target.TakeDamage(damage);

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (!hitInfo.CompareTag("Range"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
