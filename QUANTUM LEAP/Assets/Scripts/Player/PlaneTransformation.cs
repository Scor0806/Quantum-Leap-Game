using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTransformation : MonoBehaviour
{
    private bool transformed = false;
    private Sprite currentSprite;
    private Rigidbody2D rb;
    public Sprite planeSprite;
    public RuntimeAnimatorController transformAnimController;
    public RuntimeAnimatorController currentAnimController;
    private float prevDir;

    Animator anim;

    public RuntimeAnimatorController transition;
    public RuntimeAnimatorController reverseTransition;

    CharacterController2D currentController;
    PlayerMovement currentMovement;
    PlaneBehavior planeContoller;
    PlaneWeapon planeWeapon;
    Weapon weapon;
    PlayerMelee playerMelee;

    private GameObject cameraShake;
    private CineCameraShake cam;

    private void Awake()
    {

        cameraShake = GameObject.FindGameObjectWithTag("Camera");
        cam = cameraShake.GetComponent<CineCameraShake>();

        currentController = GetComponent<CharacterController2D>();
        currentMovement = GetComponent<PlayerMovement>();
        planeContoller = GetComponent<PlaneBehavior>();
        weapon = GetComponent<Weapon>();
        playerMelee = GetComponent<PlayerMelee>();
        rb = GetComponent<Rigidbody2D>();

        planeContoller.enabled = false;

        planeWeapon = GetComponent<PlaneWeapon>();
        planeWeapon.enabled = false;
        Debug.Log("Is plane enabled? " + planeContoller.enabled);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        anim = GetComponent<Animator>();
        planeContoller.enabled = false;
        planeWeapon.enabled = false;
        cam = cameraShake.GetComponent<CineCameraShake>();
    }
    IEnumerator transitionTime()
    {
        anim.runtimeAnimatorController = transition;
        currentController.enabled = false;
        currentMovement.enabled = false;

        yield return new WaitForSeconds(0.5f);

        GetComponent<SpriteRenderer>().sprite = planeSprite;
        //currentController.enabled = true;
        //currentMovement.enabled = true;
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
        if (Input.GetKeyDown(KeyCode.X) && !transformed && anim.runtimeAnimatorController == currentAnimController)
        {
            //Transform
            transformed = true;
            cam.ZoomOut();
            GetComponent<SpriteRenderer>().sprite = planeSprite;
            StartCoroutine(transitionTime());
            StartTransform();

        }
        else if (Input.GetKeyDown(KeyCode.X) && transformed)
        {
            //Transform back
            transformed = false;
            cam.ZoomIn();
            GetComponent<SpriteRenderer>().sprite = currentSprite;
            StartCoroutine(transitionBackTime());
            EndTransform();
        }

        
    }

    void StartTransform()
    {

        activateNewCharacteristics();
    }

    void EndTransform()
    {
        restoreCharacter();
        rb.gravityScale = 3f;
    }

    void restoreCharacter()
    {
        planeWeapon.enabled = false;
        planeContoller.enabled = false;
        currentMovement.enabled = true;
        currentController.enabled = true;
        weapon.enabled = true;
        playerMelee.enabled = true;
        rb.gravityScale = 3f;
    }

    void activateNewCharacteristics()
    {
        playerMelee.enabled = false;
        weapon.enabled = false;
        currentMovement.enabled = false;
        currentController.enabled = false;
        planeWeapon.enabled = true;
        planeContoller.enabled = true;
        rb.gravityScale = 0f;
    }
}

