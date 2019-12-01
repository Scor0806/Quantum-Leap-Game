using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public Animator anim;
    public GameObject cameraShake;
    private CineCameraShake cam;

    private float timeBetweenAttack;
    public float startTimeBetweenAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    private LayerMask target;
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        cam = cameraShake.GetComponent<CineCameraShake>();
        anim = GetComponent<Animator>();
        target = LayerMask.GetMask("boxingBag");
    }

    // Update is called once per frame
    void Update()
    {
        //then you can melee
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("IsMelee", true);
            //Debug.Log("Melee!");
            if (Physics2D.OverlapCircle(attackPos.position, attackRange, target))
            {
                //Debug.Log("Hit boxing bag");
                Collider2D bag = Physics2D.OverlapCircle(attackPos.position, attackRange, target);
                bag.GetComponent<boxingBag>().hitByMelee();

            }
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                StartCoroutine(ShakeDuration());
                Debug.Log(enemiesToDamage.Length + " Enemies nearby");
                damage = UnityEngine.Random.Range(0, 30);
                /*bool isCritical = UnityEngine.Random.Range(0, 100) < 15;
                if (isCritical)
                {
                    //damage *= 2;
                }*/
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                enemiesToDamage[i].GetComponent<Enemy>().hitByMelee();
                dmgPopup.Create(enemiesToDamage[i].GetComponent<Enemy>().GetPosition(), damage, false);
            }
        }
        else if(Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("IsMelee", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    IEnumerator ShakeDuration()
    {
        cam.activated = true;
        yield return new WaitForSeconds(0.5f);
        cam.activated = false;
    }
}
