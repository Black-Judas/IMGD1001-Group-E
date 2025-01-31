using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollingCredits : MonoBehaviour
{

    public float scrollSpeed = 20f;

    private RectTransform RectTransform;

    private bool userIsInControl = false;


    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();

    }

    void Update()
    {
        //Credits scroll on their own until the user uses scroll wheel
        if (Input.mouseScrollDelta.y != 0)
        {
            userIsInControl = true;
        }

        if (!userIsInControl)
        {

            RectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        
        } else {

            RectTransform.anchoredPosition += new Vector2(0, -Input.mouseScrollDelta.y * scrollSpeed);
        
        }
    }
}
