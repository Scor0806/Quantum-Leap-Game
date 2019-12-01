using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionObject : MonoBehaviour
{
    public bool talks; // if this is true then object can talk to player.

    public string message;

    public void Talk(){
        Debug.Log(message);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
