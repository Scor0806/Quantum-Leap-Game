﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip ShotFired1, CharacterDamage, EnemyDeath1;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        ShotFired1 = Resources.Load<AudioClip>("ShotFired1");
        CharacterDamage = Resources.Load<AudioClip>("CharacterDamage");
        EnemyDeath1 = Resources.Load<AudioClip>("EnemyDeath1");

        audioSrc = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "ShotFired1":
                audioSrc.PlayOneShot(ShotFired1);
                break;
            case "CharacterDamage":
                audioSrc.PlayOneShot(CharacterDamage);
                break;
            case "EnemyDeath1":
                audioSrc.PlayOneShot(EnemyDeath1);
                break;
        }
    }
}
