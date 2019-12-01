using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Coin : MonoBehaviour
{
    
        //  INITIALIZATION
        
        [SerializeField] Text coin;

        // Start is called before the first frame update
        void Start()
        {
            coin.text = "Auras:0";
        }

        // Update is called once per frame
        void Update()
        {
            coin.text = "Auras: " + PlayerStats.playerStats.CurrentXP;
        }
    
}
