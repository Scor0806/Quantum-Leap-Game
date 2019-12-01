using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //  INITIALIZATION
    public Transform firePoint;
    public GameObject bulletPrefab;
    public static float chargedTimer = 0f;

    private GameObject cameraShake;
    private CineCameraShake cam;

    private void Awake()
    {
        cameraShake = GameObject.FindGameObjectWithTag("Camera");
        cam = cameraShake.GetComponent<CineCameraShake>();
    }

    void Shoot() {
        //  Shooting Logic
        //StartCoroutine(ShakeDuration());
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

     // Update is called once per frame
    void Update()
    {
        //  CHECK FOR CHARGED BLAST
        if (Input.GetKey(KeyCode.Space)) {
            chargedTimer += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            SoundManagerScript.PlaySound("ShotFired1");
            Shoot();
            chargedTimer = 0f;
        }  
    }

    IEnumerator ShakeDuration()
    {
        cam.activated = true;
        yield return new WaitForSeconds(0.2f);
        cam.activated = false;
    }
}
