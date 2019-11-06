using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Shoot() {
        //  Shooting Logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

     // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            SoundManagerScript.PlaySound("ShotFired1");
            Shoot();
        }  
    }
}
