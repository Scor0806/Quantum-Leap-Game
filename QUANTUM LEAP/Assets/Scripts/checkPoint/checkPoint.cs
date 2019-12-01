using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    private PlayerStats player;
    private Animator anim;
    public bool checkPointVisited;
    private LayerMask playerLayer;
    public float checkPointRadius;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        anim = GetComponent<Animator>();
        checkPointVisited = false;
        playerLayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, checkPointRadius, playerLayer))
        {
            checkPointVisited = true;
            anim.SetBool("visited", checkPointVisited);
            player.SetCheckPoint(transform.position);
            player.CurrentHealth = 100;


        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkPointRadius);
    }
}
