using UnityEngine;
using UnityEngine.UI;

public class RoundScoreBubble : MonoBehaviour
{
    private Image imageComponent;
    [SerializeField] private Sprite[] sprites; // 0 = off, 1 = on
    [SerializeField] private Color activatedColor;

    const int OFF = 0;
    const int ON = 1;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        SetOff();
    }

    public void Toggle()
    {
        if (imageComponent.sprite == sprites[OFF])
        {
            SetOn();
        }
        else
        {
            SetOff();
        }
    }
    public void SetOn()
    {
        imageComponent.sprite = sprites[ON];
        imageComponent.color = activatedColor;
    }
    public void SetOff()
    {
        imageComponent.sprite = sprites[OFF];
        imageComponent.color = Color.white;

        //set the transparency to 0.5f
        Color tempColor = imageComponent.color;
        tempColor.a = 0.5f;
        imageComponent.color = tempColor;
    }
}
