using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    public GameObject currentInterObj = null;
    public interactionObject currentInterObjScript = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // see if object has a message
        if(currentInterObjScript.talks){
            currentInterObjScript.Talk();
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("interObject")){
            currentInterObj = other.gameObject;
            currentInterObjScript = currentInterObj.GetComponent<interactionObject>();

        }
    }
}
