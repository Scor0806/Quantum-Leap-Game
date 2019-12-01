using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserTargeter : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject laser;
    public LayerMask groundDetector;

    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, radius, groundDetector))
        {
            Instantiate(laser, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
