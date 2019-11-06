using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
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

    public int xp;

    public Slider xpSlider;

    private int CurrentHealth
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

        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        varGameObject = GameObject.FindGameObjectWithTag("Player");

        xp = 0;

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
            CurrentHealth -= 10;
        }
    }

    IEnumerator Invulnerability()
    {
        invul = true;
        Debug.Log("Invulnerable!");
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("IsHurt", false);
        //varGameObject.GetComponent<PlayerMovement>().enabled = true;
        Debug.Log("No longer invulnerable!");
        invul = false;
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            rigidbody2D.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
        }

        yield return 0;
    }

    public void DeathCheck()
    {
        if (currentHealth <= 0)  //we need to a death function here implement this
        {
            //Debug.Log("Dead");
            FindObjectOfType<GameManager>().EndGame();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        animator.SetBool("IsHurt", true);
        StartCoroutine(Invulnerability());
    }
}