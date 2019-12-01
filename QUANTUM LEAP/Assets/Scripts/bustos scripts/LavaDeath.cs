using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LavaDeath : MonoBehaviour
{
    public int lavaDa;
    private PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }



}
