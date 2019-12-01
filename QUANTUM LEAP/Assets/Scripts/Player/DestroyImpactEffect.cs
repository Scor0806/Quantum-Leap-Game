using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyImpactEffect : MonoBehaviour { 
    // INITIALIZATION
    private float destroyTimer = 0f;

    // Update is called once per frame
    void Update(){
        destroyTimer += Time.deltaTime;
        if(destroyTimer > 1) {
            Destroy(gameObject);
        }
    }
}
