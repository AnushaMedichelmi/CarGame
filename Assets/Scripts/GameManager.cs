using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public GameObject isGameOverPanel;
    public int maxLives = 100;
    public Text maxLivesText;
    public GameObject isGameWonPanel;


    //Singleton

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }

            }
            return instance;
        }
    }

    public void AddingScore(int value)
    {
         score = score + value;
        scoreText.text = "SCORE:" + score;
        if(score >= 100)
        {
            Debug.Log("won");
            GameWon();
        }
    }


    public void MaxLives(int life)
    {
        maxLives=maxLives-life;
        maxLivesText.text = "MAXLIVES:" + maxLives;
        if (maxLives == 1)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        StartCoroutine("WaitToLoad");
    }

    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(1f);
        isGameOverPanel.SetActive(true); 
    }

    public void GameWon()
    {
        StartCoroutine("WaitForWin");
    }

    IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(1f);
        isGameWonPanel.SetActive(true);
    }
}
