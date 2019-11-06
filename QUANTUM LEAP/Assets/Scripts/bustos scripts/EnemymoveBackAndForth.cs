using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemymoveBackAndForth : MonoBehaviour
{
    public bool MoveRight;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if(MoveRight){
            transform.Translate((2 * Time.deltaTime * speed), 0, 0);
            transform.localScale = new Vector2(-4,4);
            
        }
        else{
            transform.Translate((-2 * Time.deltaTime * speed), 0,0);
            transform.localScale = new Vector2(4,4);
        }
        
    }

    void OnTriggerEnter2D(Collider2D trig){
        if(trig.gameObject.CompareTag("turn")){
            if(MoveRight){
                MoveRight = false;
            }
            else{
                MoveRight = true;
            }
        }
    }
}
