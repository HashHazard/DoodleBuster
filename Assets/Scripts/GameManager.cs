using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    GameScreen,
    EndGame,
    AdScreen,
    ResumeMenu
}

// Manage the game state
public class GameManager : MonoBehaviour
{
    public GameState state;

    public static event Action<GameState> OnGameStateChange;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.GameScreen:
                break;
            case GameState.EndGame:
                break;
            case GameState.AdScreen:
                break;
            case GameState.ResumeMenu:
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }
}

