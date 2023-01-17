using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public float Score;
    public static GameMode Instance;
    public Water water;

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

    private void Start()
    {
        StartGame();
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

        UIManager.Instance.ToggleHighScoreScreen();

        if (water) 
        {
            water.StopWaterRise();
        }
    }

    public void RestartGame() 
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart() 
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
