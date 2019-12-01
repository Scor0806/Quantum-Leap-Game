using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerStats player;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerStats>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player pickup key
        if (collision.CompareTag("Player"))
        {
            player.pickUpKey();
            Destroy(gameObject);
        }
    }
}
