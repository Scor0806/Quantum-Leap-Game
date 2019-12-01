using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chests : MonoBehaviour
{
    // Start is called before the first frame update
    pickUps drop;
    public GameObject xpDrop;
    public GameObject armorDrop;
    private Animator anim;
    private bool opened;

    private float openRange = 2f;
    void Start()
    {
        //xpDrop = Resources.Load<GameObject>("Prefabs/XP");
        //armorDrop = Resources.Load<GameObject>("Prefabs/armor_upgrade_3");
        anim = GetComponent<Animator>();
        opened = false;
    }

    private void Update()
    {
     
            OpenChest();
 
    }

    // Update is called once per frame
    public void OpenChest()
    {
        if(Physics2D.OverlapCircle(transform.position, openRange, LayerMask.GetMask("Player"))){
            if (Input.GetKeyDown(KeyCode.LeftShift) && !opened)
            {
                anim.SetTrigger("Open");
                int lootCount = UnityEngine.Random.Range(3, 5);

                for(int i = 0; i <= lootCount; i++)
                {
                    int lootProb = UnityEngine.Random.Range(0, 10);
                    if(lootProb > 8)
                    {
                        //drop armor
                        Instantiate(armorDrop, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        //drop coins
                        Instantiate(xpDrop, transform.position, Quaternion.identity);

                    }
                }
                opened = true;
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, openRange);
    }
}
