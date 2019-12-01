using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask m_LayerMask;
    private PlayerStats player;
    private Animator anim;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapBox(transform.position, transform.localScale, 0f, m_LayerMask))
        {
            Debug.Log("Player uses key");
            if (player.hasKey()) { 
                anim.SetTrigger("OpenDoor");
                player.useKey();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
