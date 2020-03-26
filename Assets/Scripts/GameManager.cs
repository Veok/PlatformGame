using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;

    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu();
    }

    void Awake()
    {
        instance = this;
    }

    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
    }

    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
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