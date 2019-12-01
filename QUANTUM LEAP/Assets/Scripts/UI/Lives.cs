using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives: MonoBehaviour
{
    [SerializeField] Text lives;

    void Start()
    {
        lives.text = "Lives:0";
    }
    private void Update()
    {
        lives.text = "Lives:" + PlayerStats.playerStats.numberOfLives;
    }

}