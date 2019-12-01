using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCam;
    float shakeAmount = 0;
    private GameObject playerObject;
    private PlayerStats player;

    // Transform of the GameObject you want to shake
    private Transform transform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.7f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    {
        if (base.transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        initialPosition = base.transform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
           TriggerShake();
        }

        if (shakeDuration > 0)
        {
            Debug.Log("Trying to shake");
            base.transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            base.transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 2.0f;
    }

    // Start is called before the first frame update
    /*private void Awake()
    {
        //playerObject = GameObject.FindGameObjectWithTag("Player");
        //player = playerObject.GetComponent<PlayerStats>();
        if(mainCam == null)
        {
            mainCam = Camera.main;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake(0.1f, 0.5f);
        }
    }
    public void Shake(float amt, float length)
    {
        shakeAmount = amt;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("EndShake", length);
    }

    // Update is called once per frame
    public void BeginShake()
    {
        if(shakeAmount > 0)
        {
            Vector3 camPos = mainCam.transform.position;
            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;

        }
    }

    public void EndShake()
    {
        CancelInvoke("BeginShake");
        mainCam.transform.localPosition = Vector3.zero;
    }*/
}
