using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int intPlayerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] int intScore = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        int intNumGameSessions = FindObjectsOfType<GameSession>().Length;

        if(intNumGameSessions > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
       livesText.text = intPlayerLives.ToString();
       scoreText.text = intScore.ToString();
    }
    public void AddToScore(int intPointsToAdd)
    {
        intScore += intPointsToAdd;
        scoreText.text = intScore.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (intPlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void TakeLife()
    {
        intPlayerLives--;
        int intCurrentScenceIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(intCurrentScenceIndex);
        livesText.text = intPlayerLives.ToString();

    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);

    }

    void Update()
    {
        
    }
}
