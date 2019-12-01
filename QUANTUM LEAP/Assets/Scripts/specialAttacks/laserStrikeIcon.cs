using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode]
public class laserStrikeIcon : MonoBehaviour
{

    public Image filled;

    public float maxValue = 100;
    public float value = 0;

    // Update is called once per frame
    void Update()
    {
        value = Mathf.Clamp(value, 0, maxValue);
        float amount = value / maxValue;

        filled.fillAmount = amount;
    }
}
