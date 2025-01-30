using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene: MonoBehaviour
{
    // Start is called before the first frame update
    public void gotTogameScene()
    {
        SceneManager.LoadScene("Pong");
    }

    public void gotToTutorialScene()
    {
        SceneManager.LoadScene("Sample Game");
    }


    public void gotToCreditsScenel()
    {
        SceneManager.LoadScene("Credits");
    }

    public void gotToVersionNotesScreen()
    {
        SceneManager.LoadScene("Version Notes");
    }


    public void gotToMainMenuScreen()
    {
        SceneManager.LoadScene("Main Menu");
    }


}
