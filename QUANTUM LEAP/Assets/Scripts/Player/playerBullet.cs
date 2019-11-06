using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{

    public float speed = 20f;
    private int damage;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    //private Enemy target;
    private int maxDamage = 30;
    private int minDamage = 10;




    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.right * speed;
        damage = UnityEngine.Random.Range(minDamage, maxDamage);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            SoundManagerScript.PlaySound("ShotFired1");
            Enemy target = hitInfo.GetComponent<Enemy>();
            target.TakeDamage(damage);

            Instantiate(impactEffect, transform.position, transform.rotation);
            dmgPopup.Create(hitInfo.GetComponent<Enemy>().GetPosition(), damage, false);
            Destroy(gameObject);
        }
        else if (!hitInfo.CompareTag("Range"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
   

}
