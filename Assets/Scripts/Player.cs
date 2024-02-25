using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{   
    [Header("SCORE PANEL")]
    public Text scoreText;
    public Text CoinText;
    public Text HeartText;

    [Header("RESTART PANEL")]
    public GameObject GameOverUI;
    public Text Point;

    private int Score;
    private bool ScoreInc;
    public static int Coin;
    public static int Heart;
    public static bool HeartLock;
    public static bool CoinLock;

    private void Start()
    {
        ScoreInc = true;
        Heart = 3;
        HeartText.text = Heart.ToString();

        StartCoroutine(IncreaseScore());
    }

    private void Update()
    {
        if (Heart == 0)
            StartCoroutine(GameOver());

        if (HeartLock)
        {
            Heart--;
            HeartText.text = Player.Heart.ToString();
            HeartLock = false;
        }
        if (CoinLock)
        {
            Coin++;
            CoinText.text = Player.Coin.ToString();
            CoinLock = false;
        }

    }
    IEnumerator GameOver()
    {
        ScoreInc = false;

        Point.text = Score.ToString();

        GameOverUI.SetActive(true);

        if(Score>Menu.HighScore)
        {
            Menu.HighScore = Score;
            PlayerPrefs.SetInt("HighScore", Score);
        }

        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator IncreaseScore()
    {
        yield return new WaitForSeconds(3);

        while (ScoreInc)
        {
            yield return new WaitForSeconds(1);

            Score += 1;
            scoreText.text = Score.ToString();
            
        }
    }

}
