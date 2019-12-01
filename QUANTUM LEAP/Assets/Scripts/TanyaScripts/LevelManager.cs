using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public PlayerStats player;
    public bool respawn;
    private Vector3 spawnHere;

    public void StartGame()
	{
        SceneManager.LoadScene("Level1");

    }

    public void QuitGame()
	{
		SceneManager.LoadScene("Main Menu");
	}
}
