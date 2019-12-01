using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //  INITIALIZATION
    public static int scoreValue = 0;
    [SerializeField] Text score;

    // Start is called before the first frame update
    void Start(){
        score.text = "Score: 0";
    }

    // Update is called once per frame
    void Update(){
        score.text = "Score: " + scoreValue;
    }
}
