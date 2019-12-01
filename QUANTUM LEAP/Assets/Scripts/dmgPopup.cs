using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dmgPopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    private const float DISAPPEAR_TIMER_MAX = 0.7f;
    

    private static int sortingOrder;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            //normal hit

            textMesh.fontSize = 8;
            textColor = new Color(224, 193, 0);
        }
        else
        {
            //critical hit
            textMesh.fontSize = 12;
            textColor = new Color(255, 0, 0);
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
        moveVector = new Vector3(0.3f, 1f) * 20f;
    }

    public static dmgPopup Create(Vector3 position, int damageAmount, bool isCriticalHit)
    {
        //Debug.Log("Displaying text here: " + position);
        Transform damagePopupTransform = Instantiate(GameAssets.i.DamagePopup, position - new Vector3(-10,0,0), Quaternion.identity);
        dmgPopup damagePopup = damagePopupTransform.GetComponent<dmgPopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);

        return damagePopup;
    }

    private void Update()
    {
        //Debug.Log(transform.position);
        transform.position += moveVector * Time.deltaTime/8;
        //moveVector -= moveVector * 4f * Time.deltaTime;
        if(disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            //first half of pop up life w increase scale
            //float increaseScaleAmount = 1f;
            //transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            //second half we decrease
            //float decreaseScaleAmount = 1f;
            //transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            //start disappearing
            float disappearSpeed = 5f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
 