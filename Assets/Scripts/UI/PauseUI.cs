using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void ResumeGame()
    {
        GameManager.instance.PauseGame();
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
}
