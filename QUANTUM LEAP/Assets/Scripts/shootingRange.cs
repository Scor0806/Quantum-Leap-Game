using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingRange : MonoBehaviour
{
    public GameObject enemy;
    private bool spotted;
    private float attackRate = 3f;
    private float nextAttack = 0;
    private void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        Debug.Log(enemy);
        spotted = false;
    }
    private void Update()
    {
        if (spotted)
        {
            Attack();
        }
    }
    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") && obj is BoxCollider2D ||
            obj.CompareTag("Player") && obj is CircleCollider2D)
        {
            //Debug.Log(enemy.GetComponent<DemoAI>().test());
            //enemy.GetComponent<DemoAI>().Shoot();
            spotted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") && obj is BoxCollider2D ||
            obj.CompareTag("Player") && obj is CircleCollider2D)
        {
            spotted = false;
        }
    }

    private void Attack()
    {
        if(Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            enemy.GetComponent<DemoAI>().Shoot();
        }
        
    }
}
