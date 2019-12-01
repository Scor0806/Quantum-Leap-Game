using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public GameOver gameOver;
    public static PlayerStats playerStats;
    public new Rigidbody2D rigidbody2D;
    public Animator animator;
    public int health;
    public Text healthText;
    public Slider healthSlider;
    private GameObject varGameObject;
    public float speed;
    public RectTransform healthTransform;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    private int currentHealth;
    public int maxHealth;
    public Image visualHealth;
    public float invulTime = 0.5f;
    private bool invul;
    private Vector3 checkPointSpawn = new Vector3(0, 0 , 0);
    private parallex cam;
    public int numberOfLives;
    public float xp;

    private bool key;


    public GameObject cameraShake;
    private CineCameraShake camShake;

    public Slider xpSlider;

    public float CurrentXP
    {
        get { return xp; }
        set
        {
            xp = value;
            HandleXP();
        }
    }
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            HandleHealth();
        }
    }
    void Awake()
    {
        camShake = cameraShake.GetComponent<CineCameraShake>();
        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
    }
    private void HandleXP()
    {

    }

    private void HandleHealth()
    {
        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);
        healthTransform.position = new Vector3(currentXValue, cachedY);

        if (currentHealth > maxHealth / 2) //greater than half health
        {
            visualHealth.color = new Color32((byte)MapValues(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
        }
        else
        {
            visualHealth.color = new Color32(255, (byte)MapValues(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);

        }
    }
    void Start()
    {
        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - 2 * healthTransform.rect.width;
        currentHealth = maxHealth;
        invul = false;
        key = false;

        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        varGameObject = GameObject.FindGameObjectWithTag("Player");
        //camShake = GetComponent<CameraShake>();

        xp = 0;
        numberOfLives = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (healthTransform.GetComponent<CanvasRenderer>().cullTransparentMesh == true)
        {
            healthTransform.GetComponent<CanvasRenderer>().cullTransparentMesh = false;

        }
        DeathCheck();
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!invul && other.tag == "Enemy" && currentHealth > 0)
        {
            SoundManagerScript.PlaySound("CharacterDamage");
            Debug.Log("Ouch, ran into enemy!" + currentHealth + ": HP left");

            //varGameObject.GetComponent<PlayerMovement>().enabled = false;
            animator.SetBool("IsHurt", true);
            //animator.SetBool("IsMoving", false);
            StartCoroutine(Invulnerability());
            CurrentHealth -= 5;
        }
    }

    IEnumerator ShakeDuration()
    {
        camShake.activated = true;
        yield return new WaitForSeconds(0.5f);
        camShake.activated = false;
    }

    IEnumerator Invulnerability()
    {
        invul = true;
        //camShake.Shake(0.1f, 0.2f);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("IsHurt", false);
        invul = false;
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Transform obj)
    {
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position);
            rigidbody2D.AddForce(-direction *knockbackPwr);
        }

        yield return 0;
    }

    public bool isAlive()
    {
        return currentHealth > 0;
    }

    public void DeathCheck()
    {
        //Debug.Log("Current health: " + currentHealth);
        if (currentHealth <= 0)  //we need to a death function here implement this
        {
            //Debug.Log(isAlive());
            //SceneManager.LoadScene("GameOver");
           
            //Destroy(gameObject);
            if(numberOfLives > 0)
            {
                //respawn at last check poin
                numberOfLives -= 1;
                CurrentHealth = 100;
                Debug.Log("Lives Left: " + numberOfLives);
                transform.position = GetLastCheckPoint();
            }
            else
            {
                SceneManager.LoadScene("GameOver");
                Destroy(gameObject);
            }
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        //Vector3 direction = (this.transform.position - other.transform.position).normalized;
        //GetComponent<Rigidbody2D>().velocity = new Vector3(-0.5f, 0.5f, 0);
        
        CurrentHealth -= damage;
        animator.SetBool("IsHurt", true);
        //rigidbody2D.AddForce(new Vector2(-transform.position * 1000, 0));
        StartCoroutine(Invulnerability());
    }


    /*to use these properly, whenever we die give the player
     * the option to play from the last checkpoint, then we can just instantiate a new
     * ARES at the GetLastCheckPoint (PlayableCharacter will have to be a prefab)
     */
    
    public void SetCheckPoint(Vector3 location)
    {
        checkPointSpawn = location;
        //Debug.Log("You will spawn here: " + checkPointSpawn + " on death");
    }

    public Vector3 GetLastCheckPoint()
    {
        return checkPointSpawn;
    }

    public void pickUpKey()
    {
        key = true;
    }

    public bool hasKey()
    {
        return key;
    }

    public void useKey()
    {
        key = false;
    }

}