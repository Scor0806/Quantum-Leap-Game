using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    int damage = 50;

    private GameObject cameraShake;
    private CineCameraShake cam;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Laser Created");
        cameraShake = GameObject.FindGameObjectWithTag("Camera");
        cam = cameraShake.GetComponent<CineCameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Shake());
        StartCoroutine(destroy());
    }
    private void OnTriggerStay2D(Collider2D obj)
    {
        float timerMax = 1f;
        float timer = 0;
        if (obj.CompareTag("Enemy"))
        {
            

            timer += Time.deltaTime;

            if (timer < timerMax)
            {
                Debug.Log("Enemy inside");
                timer = 0;
                obj.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1.56f);
        Destroy(gameObject);
    }

    IEnumerator Shake()
    {
        cam.activated = true;
        yield return new WaitForSeconds(1.56f);
        cam.activated = false;
    }
}
