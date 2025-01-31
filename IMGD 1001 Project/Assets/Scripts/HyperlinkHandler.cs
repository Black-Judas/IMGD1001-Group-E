using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(TMP_Text))]
public class HyperlinkHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log("Click detected");
        TMP_Text tmp_Text = GetComponent<TMP_Text>();

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(tmp_Text, eventData.position, null);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = tmp_Text.textInfo.linkInfo[linkIndex];

            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}
