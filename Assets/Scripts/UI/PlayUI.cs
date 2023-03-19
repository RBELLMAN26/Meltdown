using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayUI : MonoBehaviour
{
    [SerializeField] InputActionAsset playerInputAsset;
    InputActionMap playerInputMap;
    InputAction pauseInput;

    public GameObject pauseMenu;

    private void Awake()
    {
        playerInputMap = playerInputAsset.FindActionMap("Player");
        pauseInput = playerInputMap.FindAction("Pause");
    }
    private void OnEnable()
    {
        GameManager.startGameEvent += OnStartGame;
        GameManager.gameOverEvent += OnGameOver;
        
        pauseInput.performed += context => PauseGame();
        pauseInput.Enable();
    }

    private void OnDisable()
    {
        GameManager.startGameEvent -= OnStartGame;
        GameManager.gameOverEvent -= OnGameOver;
        pauseInput.performed -= context => PauseGame();
        pauseInput.Disable();

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
        if(GetComponent<Canvas>().enabled)
        {
            GameManager.instance.PauseGame();
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }
}
