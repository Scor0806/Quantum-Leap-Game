using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class pickUps : MonoBehaviour
{
    public enum pickUpObj {XP, POWERUP, HP};
    public pickUpObj currentObj;
    public int xpValue;
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
                PlayerStats.playerStats.xp += xpValue;
                Debug.Log(PlayerStats.playerStats.xp);
            }
            if(currentObj == pickUpObj.HP)
            {
                PlayerStats.playerStats.health += hpValue;
                Debug.Log(PlayerStats.playerStats.xp);
            }
            Destroy(gameObject);
        }
    }
 
    // void Update(){
    //     transform.Translate(0,(Time.deltaTime * speed),0);

    // }



}
