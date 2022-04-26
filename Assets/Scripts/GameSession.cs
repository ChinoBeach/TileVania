using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int intPlayerLives = 3;
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
    
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
