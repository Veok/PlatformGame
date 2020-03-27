using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;

    public static GameManager instance;
    public Canvas InGameCanvas;
    public Text CoinsText;
    private int _coins;
    public Image Key;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu();
    }

    void Awake()
    {
        instance = this;
        Key.color = Color.gray;
    }

    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        InGameCanvas.enabled = (newGameState == GameState.GS_GAME);
    }

    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }

    public void FoundKey()
    {
        Key.color = Color.white;
    }
    public void GameOver()
    {
        SetGameState(GameState.GS_GAME_OVER);
    }

    public void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
    }

    public void LevelCompleted()
    {
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    public void AddCoins()
    {
        _coins++;
        CoinsText.text = _coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (instance.currentGameState == GameState.GS_PAUSEMENU)
        {
            if (Input.GetKey(KeyCode.S))
            {
                instance.InGame();
            }
        }
    }
}

public enum GameState
{
    GS_PAUSEMENU,
    GS_GAME,
    GS_LEVELCOMPLETED,
    GS_GAME_OVER
}