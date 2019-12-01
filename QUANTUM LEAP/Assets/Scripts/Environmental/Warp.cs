using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    private GameObject player;
    public Transform warpPoint;
    public float warpRadius;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        warpPoint = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(warpPoint.transform.position, warpRadius, LayerMask.GetMask("Player")))
        {
            anim.SetTrigger("warp");
            StartCoroutine(WarpPlayer());
        }
    }

    IEnumerator WarpPlayer()
    {
        yield return new WaitForSeconds(0.85f);
        player.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(warpPoint.transform.position, warpRadius);
    }
}
