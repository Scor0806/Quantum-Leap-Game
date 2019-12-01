using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introTransition : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement movement;
    private CharacterController2D characterController;

    void Start()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapBox(transform.position, transform.localScale * 100, 0f, LayerMask.GetMask("Player")))
        {
            movement.enabled = false;
            characterController.enabled = false;
        }
        else
        {
            movement.enabled = true;
            characterController.enabled = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, transform.localScale * 100);
    }
}
