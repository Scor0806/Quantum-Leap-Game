
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    private Vector3 pos1, pos2;
    public float speed;
    Vector3 nextPos;

    [SerializeField]
    private Transform startPosition;

    [SerializeField]
    private Transform destination;

    private bool moving;
    void Start()
    {
        pos1 = startPosition.localPosition;
        pos2 = destination.position;
        nextPos = pos2;
        //Debug.Log("Transform " + transformB.position);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Vector3.Distance(startPosition.localPosition, nextPos) <= 0.1)
        {
            ChangeDirection();
        }
    }

    private void Move()
    {
        startPosition.localPosition = Vector3.MoveTowards(startPosition.localPosition, nextPos, speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        nextPos = nextPos != pos1 ? pos1 : pos2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moving = true;
            collision.collider.transform.SetParent(transform);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }

}
