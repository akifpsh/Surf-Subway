using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text HighScore_Text;
    public static int HighScore;
    public static string HighScoreText;

    private void Awake()
    {
        HighScore_Text.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

    }
    private void Start()
    {
        HighScore_Text.text=PlayerPrefs.GetInt("HighScore",HighScore).ToString();
    }

}
