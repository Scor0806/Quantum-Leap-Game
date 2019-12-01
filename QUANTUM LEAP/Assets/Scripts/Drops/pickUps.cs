using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class pickUps : MonoBehaviour
{
    public enum pickUpObj {XP, POWERUP, HP, armor_upgrade_3};
    public pickUpObj currentObj;
    public float xpValue;
    public int hpValue;
    public float speed;




    // public Rigidbody2D rb;
    // public float moveSpeed;
    // public float jumpPower;
    // float targetMoveSpeed;

    //isgrounded
    //private bool grounded = false;
    //private Rigidbody2D rb2d;

    //private Transform GroundCheck;
    // Start is called before the first frame update

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(currentObj == pickUpObj.XP)
            {
                Debug.Log("Picked up xp");
                SoundManagerScript.PlaySound("coinsound");
                PlayerStats.playerStats.CurrentXP += xpValue;
                Debug.Log(PlayerStats.playerStats.xp);
            }
            if(currentObj == pickUpObj.HP)
            {
                PlayerStats.playerStats.health += hpValue;
                Debug.Log(PlayerStats.playerStats.xp);
            }
            if(currentObj == pickUpObj.armor_upgrade_3)
            {
                SoundManagerScript.PlaySound("suitup");
                PlayerStats.playerStats.numberOfLives += 1;
            }
           Destroy(gameObject);
        }
        
    }


    // void Update(){
    //     transform.Translate(0,(Time.deltaTime * speed),0);

    // }



}
