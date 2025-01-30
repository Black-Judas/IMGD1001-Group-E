using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene: MonoBehaviour
{

    public void GoToGameScene()
    {
        SceneManager.LoadScene("Pong");
    }

    public void GoToTutorialScene()
    {
        SceneManager.LoadScene("Sample Game");
    }


    public void GoToCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoToVersionNotesScene()
    {
        SceneManager.LoadScene("Version Notes");
    }


    public void GoToMainMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
