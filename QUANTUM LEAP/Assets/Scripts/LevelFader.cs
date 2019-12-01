using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class LevelFader : MonoBehaviour
{
    public Animator anim;
    public LayerMask playerLayer;
    private string LevelToLoad;

    public Transform endPoint;
    public float radius;
    public string nextLevel;
    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(endPoint.position, radius, playerLayer))
        {
            FadeToLevel(nextLevel);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FadeToLevel(nextLevel);
        }
    }

    public void FadeToLevel(string levelName)
    {
        LevelToLoad = levelName;
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        StartCoroutine(WaitOnFade());
    }

    IEnumerator WaitOnFade()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene(LevelToLoad);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endPoint.position, radius);

    }
}
