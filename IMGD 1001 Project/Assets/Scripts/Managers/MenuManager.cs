using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayMusic("menuTheme");
    }

}
