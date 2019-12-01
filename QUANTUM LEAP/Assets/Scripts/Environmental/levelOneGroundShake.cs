using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelOneGroundShake : MonoBehaviour
{
    // Start is called before the first frame update
    bool landed = false;
    private GameObject cameraShake;
    private CineCameraShake cam;
    public Vector2 landZone;
    public Vector2 area;
    void Start()
    {
        cameraShake = GameObject.FindGameObjectWithTag("Camera");
        cam = cameraShake.GetComponent<CineCameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapBox(transform.position, area, 0f, LayerMask.GetMask("Player")))
        {
            StartCoroutine(Shake());
            landed = true;
        }
    }

    IEnumerator Shake()
    {
        cam.activated = true;
        yield return new WaitForSeconds(1f);
        cam.activated = false;
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, area);
    }
}
