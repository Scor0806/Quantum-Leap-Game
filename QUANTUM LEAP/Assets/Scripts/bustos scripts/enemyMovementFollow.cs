using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class enemyMovementFollow : MonoBehaviour
{
    public float speed; //fast boy
    private Transform target; //chasing player
    private Transform badGuy; //enemy
    public float distanceFromPlayer = 3;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    private Animator anim;

    private float distanceAway;

    public float fieldOOfViewAngle = 110f;


//------------------



    //public bool playerInSight

    void Start(){

        //finding the position of whatever has the tag player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        badGuy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();

        anim = GetComponent<Animator>();

        
    }

    private void Flip() {
        Vector3 targetPos = target.transform.position;
        Vector3 targetPosFlattened = new Vector3(0, targetPos.y, 0);
        transform.LookAt(targetPosFlattened);
    }

    IEnumerator WaitAFew()
    {

            //waiting so the enemy doesnt follow right away
            yield return new WaitForSeconds(0.3f);

            //flips direction of the enemy to follow in another direction
            if (target.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(4f,4f,1f);
                //transform.localScale = new Vector3(4f, 4f, 1f);
                //Vector3 currRot = transform.eulerAngles;
                //transform.localxscale = -1;
            }
            else
            {
                transform.localScale = new Vector3(-4f,4f,1f);

            }
            //move towards player 
            if (Vector2.Distance(transform.position, target.position) > distanceFromPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        
    }

    void Update()
    {
        StartCoroutine(WaitAFew());
        //Debug.Log("Enemy Speed: " + speed);
        anim.SetFloat("Speed", Mathf.Abs(speed));


    }
    
    
}