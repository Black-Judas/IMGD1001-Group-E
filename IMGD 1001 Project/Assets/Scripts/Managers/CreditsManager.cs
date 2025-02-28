using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayMusic("creditsTheme");
    }

}
