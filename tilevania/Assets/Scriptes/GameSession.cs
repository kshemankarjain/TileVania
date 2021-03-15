using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text ScoreText;
    [SerializeField] Text LivesText;

    private void Awake()
    {
        int NumGameSessions = FindObjectsOfType<GameSession>().Length;
        if (NumGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }
    public void ProcessPlayerDeath()
    {
        if(PlayerLives>1)
        {
            TakeALife();
        }else
        {
            ResetGameSession();
        }
    }

    private void TakeALife()
    {
        PlayerLives--;
        var CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
        LivesText.text = PlayerLives.ToString();

    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    private void Start()
    {
        LivesText.text = PlayerLives.ToString();
        ScoreText.text = score.ToString();
    }
    public void AddToScore(int PointstoAdd)
    {
        score += PointstoAdd;
        ScoreText.text = score.ToString();

    }
}
