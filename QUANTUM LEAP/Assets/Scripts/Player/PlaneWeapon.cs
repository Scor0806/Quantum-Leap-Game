using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool shooting = false;

    float timerMax = 0.5f;
    float timer = 0;

    void Shoot()
    {
        //  Shooting Logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SoundManagerScript.PlaySound("ShotFired1");

            if (!shooting) { 
                StartCoroutine(BurstShot());
            }

        }
    }

    IEnumerator BurstShot()
    {
        shooting = true;
        Shoot();
        yield return new WaitForSeconds(0.12f);
        Shoot();
        yield return new WaitForSeconds(0.12f);
        Shoot();
        shooting = false;
    }
}
