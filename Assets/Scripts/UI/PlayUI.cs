using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.startGameEvent += OnStartGame;
        GameManager.gameOverEvent += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.startGameEvent -= OnStartGame;
        GameManager.gameOverEvent -= OnGameOver;

    }

    private void OnStartGame()
    {
        GetComponent<Canvas>().enabled = true;
    }
    private void OnGameOver()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void PauseGame()
    {
        GameManager.instance.PauseGame();
    }
}
