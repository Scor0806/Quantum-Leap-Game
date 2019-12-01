using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class laserStrike : MonoBehaviour
{
    int damage = 50;
    public laserStrikeIcon slider;
    public float delay;
    public bool cooling = false;
    public GameObject targeter;
    public Transform throwPoint;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = delay;
        slider.value = 0;
    }
    public void ThrowTargeter()
    {
        if (!cooling) {
            Instantiate(targeter, throwPoint.position, throwPoint.rotation);
            StartCoolDown();
        }
    }

    public void StartCoolDown()
    {
        StartCoroutine(OnUpdate());
    }

    IEnumerator OnUpdate()
    {
        cooling = true;
        slider.maxValue = delay;
        slider.value = 0;
        for(int i = 0; i < delay; i++)
        {
            slider.value = delay - i;
            yield return new WaitForSeconds(1f);
        }
        slider.value = 0;
        cooling = false;
    }
}
