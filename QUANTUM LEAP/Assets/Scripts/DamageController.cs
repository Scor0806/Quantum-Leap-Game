using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private float collisionDamage;
    [SerializeField] private HealthController healthController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            
            Damage();
        }
    }

    void Damage()
    {
        
        healthController.playerHealth = healthController.playerHealth - collisionDamage;
        healthController.UpdateHealth();
        this.gameObject.SetActive(false);
    }
}
