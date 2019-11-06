
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public Vector3 pos1, pos2;
    public float speed;
    public Vector3 startpos;
    public Vector3 velocity;
    Vector3 nextPos;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

    private bool moving;
    void Start()
    {
        pos1 = childTransform.localPosition;
        pos2 = transformB.localPosition;
        nextPos = pos2;   
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeDirection();
        }
    }

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);
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
    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }
}
