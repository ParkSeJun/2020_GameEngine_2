using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{


    public void OnClickedRestart()
    {
        string curScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("Restart", curScene);
        SceneManager.LoadScene("Restart");
    }

    public void OnClickedMain()
    {
        SceneManager.LoadScene("Intro");
    }

    public void OnClickedExit()
    {
        Application.Quit();
    }
}
