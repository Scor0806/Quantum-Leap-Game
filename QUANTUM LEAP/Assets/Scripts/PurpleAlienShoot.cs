using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleAlienShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Shoot()
    {
        //  Shooting Logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
