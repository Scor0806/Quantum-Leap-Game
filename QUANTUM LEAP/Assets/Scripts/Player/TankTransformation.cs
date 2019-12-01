using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTransformation : MonoBehaviour
{
    private bool transformed = false;
    private Sprite currentSprite;
    private Rigidbody2D rb;
    public Sprite tankSprite;
    public RuntimeAnimatorController transformAnimController;
    public RuntimeAnimatorController currentAnimController;

    public RuntimeAnimatorController transition;
    public RuntimeAnimatorController reverseTransition;

    private float prevDir;

    Animator anim;

    CharacterController2D currentController;
    PlayerMovement currentMovement;
    TankBehavior tankContoller;
    TankWeapon tankWeapon;
    Weapon weapon;
    PlayerMelee playerMelee;

    AnimationClip test;

    private void Awake()
    {
        currentSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        anim = GetComponent<Animator>();

        currentController = GetComponent<CharacterController2D>();
        currentMovement = GetComponent<PlayerMovement>();
        tankContoller = GetComponent<TankBehavior>();
        weapon = GetComponent<Weapon>();
        playerMelee = GetComponent<PlayerMelee>();
        rb = GetComponent<Rigidbody2D>();

        tankContoller.enabled = false;

        tankWeapon = GetComponent<TankWeapon>();
        tankWeapon.enabled = false;

    }


    IEnumerator transitionTime()
    {
        anim.runtimeAnimatorController = transition;
        currentController.enabled = false;
        currentMovement.enabled = false;
        
        yield return new WaitForSeconds(0.5f);

        GetComponent<SpriteRenderer>().sprite = tankSprite;
        currentController.enabled = true;
        currentMovement.enabled = true;
        anim.runtimeAnimatorController = transformAnimController;


    }

    IEnumerator transitionBackTime()
    {
        anim.runtimeAnimatorController = reverseTransition;
        currentController.enabled = false;
        currentMovement.enabled = false;

        yield return new WaitForSeconds(0.5f);

        GetComponent<SpriteRenderer>().sprite = currentSprite;
        currentController.enabled = true;
        currentMovement.enabled = true;
        anim.runtimeAnimatorController = currentAnimController;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !transformed && anim.runtimeAnimatorController == currentAnimController)
        {
            //Transform
            transformed = true;
            GetComponent<SpriteRenderer>().sprite = tankSprite;
            StartCoroutine(transitionTime());
            StartTransform();
            

        }
        else if (Input.GetKeyDown(KeyCode.C) && transformed)
        {
            //Transform back
            transformed = false;
            GetComponent<SpriteRenderer>().sprite = currentSprite;
            StartCoroutine(transitionBackTime());
            EndTransform();
        }
    }

    void StartTransform()
    {
        tankWeapon.enabled = true;
        weapon.enabled = false;
        playerMelee.enabled = false;
    }

    void EndTransform()
    {
        tankWeapon.enabled = false;
        weapon.enabled = true;
        playerMelee.enabled = true;
    }
}
