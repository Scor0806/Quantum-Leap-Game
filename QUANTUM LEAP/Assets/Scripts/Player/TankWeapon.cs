using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

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
            anim.SetBool("isShooting", true);
            SoundManagerScript.PlaySound("ShotFired1");
            Shoot();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("isShooting", false);
        }
    }
}
