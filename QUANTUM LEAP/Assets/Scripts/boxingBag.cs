using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxingBag : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Animator anim;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void hitByMelee()
    {
        Debug.Log("Hitititit");
        if((player.transform.position.x - transform.position.x) < 0)
        {
            anim.SetBool("left", true);
            StartCoroutine(LeftTimer());
        }
        else
        {
            anim.SetBool("right", true);
            StartCoroutine(RightTimer());
        }
        //anim.SetBool("left", false);
        //anim.SetBool("right", false);
    }
   IEnumerator LeftTimer()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("left", false);
    }
    IEnumerator RightTimer()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("right", false);
    }
}
