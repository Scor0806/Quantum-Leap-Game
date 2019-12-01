using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charged_Blast : MonoBehaviour {

    //  INITIALIZATION

    public float chargeTimer = 0;
    public KeyCode chargeBlast;
    public Transform firePoint;
    public GameObject chargedBlast;
    private CharacterController2D controller;
    private Transform enemy;
    private GameObject blast;
    //public float interval;

    public float speed = 30f;
    public int damage = 150;
    public Rigidbody2D rb;

    public GameObject impactEffect;

    //public float lifetime;

       
    

    void ChargedShot() {
        //  CREATE CHARGED BLAST
        if (PlayerStats.playerStats.CurrentXP > 0)
        {
            Instantiate(chargedBlast, firePoint.position, firePoint.rotation);
            PlayerStats.playerStats.CurrentXP -= 1;
        }
    }


    // Update is called once per frame
    void Update() {

        //  ACCRUE CHARGE TIME
        if (Input.GetKey(chargeBlast)) {
            chargeTimer += Time.deltaTime;
        }
        //  DEPLOY CHARGE && RESET TIMERS
        if ((Input.GetKeyUp(chargeBlast)) && (chargeTimer > .3)) {
            ChargedShot();
            GetComponent<Rigidbody2D>().velocity = new Vector3(-0.5f, 0, 0);
            chargeTimer = 0;
            Weapon.chargedTimer = 0;
        }
        if ((Input.GetKeyUp(chargeBlast)) && (chargeTimer < .3)) {
            chargeTimer = 0;
        }


    }




}
