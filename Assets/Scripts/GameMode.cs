using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public float Score;
    public static GameMode Instance;

    public delegate void OnStartGameDelegate();
    public OnStartGameDelegate onStartGameDelegate;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartGame() 
    {
        //We start game
        //We enable the player to start the game!
        //We can use a delegate and add listeners to the delegate to start the game.

        if (onStartGameDelegate != null) 
        {
            onStartGameDelegate();
        }
    }

    public void AddScore(float scoreToAdd) 
    {
        Debug.Log("+" + scoreToAdd);
        Score += scoreToAdd;
    }

    public void SetScore(float newScore) 
    {
        Score = newScore;
    }

    public void OnPlayerLost() 
    {
        Debug.Log("Game Over!");
        Debug.Log("We lost!");
    }
}