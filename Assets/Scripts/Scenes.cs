using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void LoadSceneGame()
    {
        SceneManager.LoadScene("GAME");
    }

    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("MENU");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
