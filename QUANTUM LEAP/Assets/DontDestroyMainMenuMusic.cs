using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMainMenuMusic : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
