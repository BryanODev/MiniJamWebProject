using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject HighScoreScreen;
    public TMP_Text HighScoreText;

    private void Awake()
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

    public void ToggleHighScoreScreen() 
    {
        if (HighScoreScreen != null)
        {
            HighScoreScreen.SetActive(!HighScoreScreen.activeSelf);
            HighScoreText.text = GameMode.Instance.Score.ToString();
        }
    }
}
