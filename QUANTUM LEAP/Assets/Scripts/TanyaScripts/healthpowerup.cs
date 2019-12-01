using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthpowerup : MonoBehaviour
{
    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
			if (PlayerStats.playerStats.CurrentXP > 0)
			{
				PlayerStats.playerStats.CurrentHealth = 100;
				PlayerStats.playerStats.CurrentXP -= 2;
			}
            }

        
    }
    
    
}
