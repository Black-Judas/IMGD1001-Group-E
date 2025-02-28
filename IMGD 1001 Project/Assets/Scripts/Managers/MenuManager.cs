using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayMusic("menuTheme");
    }

    public void CloseGame()
    {
        Debug.Log("Game closed");
        Application.Quit();
    }

}
