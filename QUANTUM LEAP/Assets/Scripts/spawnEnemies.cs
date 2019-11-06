using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{

    public GameObject enemy;
    float randX;
    Vector2 spawnLoc; //spawning location
    public float spawnRate = 2f; //how fast
    float nextSpawn = 0.0f;
    float timeLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit = Time.fixedTime;
        if (timeLimit < 5f) { 
            if(Time.time > nextSpawn){
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(20.0f ,50.4f);
                spawnLoc = new Vector2(randX, transform.position.y);
                Instantiate(enemy, spawnLoc, Quaternion.identity);
            }
        }
        if (timeLimit > 5.0f || timeLimit < 20.0f){
            if (Time.time > nextSpawn){
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(4.0f, 10.0f);
                spawnLoc = new Vector2(randX, transform.position.y);
                Instantiate(enemy, spawnLoc, Quaternion.identity);
            }
        }
    }
}
