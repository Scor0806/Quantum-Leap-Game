using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameoverObject;
    private GameObject playerObject;
    private PlayerStats player;
    private LevelManager levelManage;
    private bool dead = false;
    // Start is called before the first frame update
    void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerStats>();
        levelManage = gameoverObject.GetComponent<LevelManager>();
        //gameoverObject.SetActive(false);
        //enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.isAlive());
        if (!player.isAlive())
        {
            if (!dead) { 
                //gameoverObject.SetActive(true);
                Instantiate(gameoverObject);
                
                Debug.Log("about to respawn");
            }
            dead = true;
            //enabled = true;
            /*if (levelManage.respawning())
            {
                //Destroy(gameoverObject);
            }
            else
            {

                //return to main menu
            }*/
        }
    }
}
