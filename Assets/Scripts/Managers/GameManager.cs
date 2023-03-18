using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float timeInGame;

    public delegate void OnStartGame();
    public static OnStartGame startGameEvent;

    public delegate void OnPauseGame();
    public static OnPauseGame pauseGameEvent;

    public delegate void OnGameOver();
    public static OnGameOver gameOverEvent;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timeInGame += Time.deltaTime;
        UpdateTimers();
    }

    void UpdateTimers()
    {
        foreach(TimerUpdate timer in FindObjectsOfType<TimerUpdate>())
        {
            timer.SetTime(timeInGame);
        }
    }

    public void StartGame()
    {
        timeInGame = 0;
        GameManager.startGameEvent?.Invoke();
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverEvent?.Invoke();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
