using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Upgrade Selection Screen
    public CanvasGroup upgradesCanvasGroup;

    public RectTransform upgradeButtonsRectTransform;
    public Vector2 upgradeButtonsLocation = new Vector2(-145f,0f);

    public CanvasGroup detailCardGroup;
    public RectTransform detailCardRectTransform;
    public Vector2 detailCardPosition = new Vector2(233f, -36f);

    public RectTransform topText;
    public Vector2 topTextPosition;

    public Image backgroundOverlay;


    public void upgradeSelectionScreenIn()
    {
        upgradesCanvasGroup.interactable = true;
        upgradesCanvasGroup.blocksRaycasts = true;

        FadeUpgradeScreenIn();
        ButtonsSlideIn();
        TopTextSlideIn();
        DetailCardAppear();
    }

    public void upgradeSelectionScreenOut()
    {
        upgradesCanvasGroup.interactable = false;
        upgradesCanvasGroup.blocksRaycasts = false;

        ButtonsSlideOut();
        FadeUpgradeScreenOut();
    }
    
    public void ButtonsSlideIn(float slideDuration = 0.5f)
    {

        upgradeButtonsRectTransform.transform.localPosition = new Vector3(-800f, 0f, 0f); //Buttons start to the left off-screen

        upgradeButtonsRectTransform.DOAnchorPos(upgradeButtonsLocation, slideDuration, false).SetEase(Ease.OutCubic); //Ease the buttons into position over the duration provided

    }
    public void ButtonsSlideOut(float slideDuration = 0.5f)
    {

        upgradeButtonsRectTransform.transform.localPosition = upgradeButtonsLocation;

        upgradeButtonsRectTransform.DOAnchorPos(new Vector2(-800f, 0f), slideDuration, false).SetEase(Ease.InCubic);

    }
    
    public void DetailCardAppear(float duration = 0.2f)
    {
        //Slide in
        detailCardRectTransform.transform.localPosition = new Vector2(detailCardPosition.x, -400f);
        detailCardRectTransform.DOAnchorPos(detailCardPosition, duration, false);

        //Pop in
        //detailCardRectTransform.localScale = Vector3.zero;
        //detailCardRectTransform.DOScale(1, duration).SetEase(Ease.OutSine);

        //Flicker in
        //detailCardGroup.alpha = 0.2f;
        //detailCardGroup.DOFade(1, duration).SetEase(Ease.OutBounce);

        //Flip over
        //detailCardRectTransform.rotation = Quaternion.Euler(0,180,0);
        //detailCardRectTransform.DORotate(new Vector3(0, 0, 0), duration);
    }

    public void TopTextSlideIn(float duration = 0.2f)
    {
        topText.transform.localPosition = new Vector2(0, 200);
        topText.DOAnchorPos(topTextPosition, duration, false);
    }

    public void FadeUpgradeScreenIn(float fadeDuration = 0.5f)
    {
        upgradesCanvasGroup.alpha = 0f;
        upgradesCanvasGroup.DOFade(1, fadeDuration);
    }
    public void FadeUpgradeScreenOut(float fadeDuration = 0.2f)
    {
        upgradesCanvasGroup.alpha = 1f;
        upgradesCanvasGroup.DOFade(0, fadeDuration);
    }
}
