using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] ScoreViewer scoreViewer;
    [SerializeField] AudioClip gameOverClip;
    private void OnEnable()
    {
        GameManager.gameOverEvent += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.gameOverEvent -= OnGameOver;
    }


    //Plays Gameover sfx and displays the top scores.
    void OnGameOver()
    {
        AudioManager.instance.TurnOffMusic();
        AudioManager.instance.PlayOneShot(gameOverClip);
        GetComponent<Canvas>().enabled = true;
        TopScoreManager.instance.AddScore();
        DisplayTopScores();
    }


    public void DisplayTopScores()
    {
        scoreViewer.LoadScores();
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }
}
