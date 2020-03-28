using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;

    public static GameManager instance;
    public Canvas InGameCanvas;
    public Text CoinsText;
    public Text EnemyKilledText;
    private int _coins;
    private int _enemiesKilled;
    public Image Key;
    public Image[] HeartsArray;
    private int Hearts = 3;
    public float Timer;
    public Text TimerText;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu();
    }

    void Awake()
    {
        instance = this;
        CoinsText.fontSize = 90;
        EnemyKilledText.fontSize = 90;
        Key.color = Color.gray;
        HeartsArray[HeartsArray.Length - 1].color = Color.gray;
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

    public void EnemyKilledCounter()
    {
        _enemiesKilled++;
        EnemyKilledText.text = _enemiesKilled.ToString();
    }

    public void AddHeart()
    {
        HeartsArray[Hearts++].color = Color.white;
    }

    public void RemoveHeart()
    {
        HeartsArray[Hearts - 1].color = Color.gray;
        Hearts--;
    }

    // Update is called once per frame
    void Update()
    {
        if (instance.currentGameState == GameState.GS_PAUSEMENU)
        {
            if (Input.GetKey(KeyCode.S))
            {
                instance.InGame();
                CoinsText.text = "0";
                EnemyKilledText.text = "0";
            }
        }

        if (instance.currentGameState == GameState.GS_GAME)
        {
            Timer += Time.deltaTime;
            var time = TimeSpan.FromSeconds(Timer);
            var text = time.ToString(@"mm\:ss\:ff");
            TimerText.text = text;
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