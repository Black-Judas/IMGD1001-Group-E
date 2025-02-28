using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailCard : MonoBehaviour
{
    private UpgradeSelectionScreen upgradeSelectionScreen;

    private Upgrade SelectedUpgrade;
    private RarityColors rarityColorReference = new RarityColors(); //Reference to the colors associated with each rarity (can be changed in Upgrade.cs)

    //COMPONENTS
    private TMP_Text nameText;
    private TMP_Text rarityText;
    private Image icon;
    private TMP_Text descriptionText;

    private void Awake()
    {

        nameText = transform.Find("Name and Rarity").Find("Name").GetComponent<TMP_Text>();
        rarityText = transform.Find("Name and Rarity").Find("Rarity").GetComponent<TMP_Text>();
        icon = transform.Find("Icon").GetComponent<Image>();
        descriptionText = transform.Find("Description").GetComponent<TMP_Text>();

        upgradeSelectionScreen = FindObjectOfType<UpgradeSelectionScreen>();


    }

    // Update is called once per frame
    void Update()
    {
        SelectedUpgrade = upgradeSelectionScreen.SelectedUpgrade;

        if (SelectedUpgrade != null)
        {
            EnableComponents();
            nameText.text = SelectedUpgrade.Name;
            rarityText.text = SelectedUpgrade.Rarity.ToString();
            rarityText.color = rarityColorReference.GetColor(SelectedUpgrade.Rarity);

            if (SelectedUpgrade.Icon != null)
            {
                icon.sprite = SelectedUpgrade.Icon;
            }
            else
            {
                icon.sprite = null;
                icon.enabled = false;
            }

            descriptionText.text = SelectedUpgrade.Description;

        }
        else
        {
            DisableComponents();
        }
    }

    void DisableComponents()
    {
        this.GetComponent<Image>().color = Color.gray;
        nameText.enabled = false;
        rarityText.enabled = false;
        icon.enabled = false;
        descriptionText.enabled = false;
    }

    private void EnableComponents()
    {
        this.GetComponent<Image>().color = Color.white;

        nameText.enabled = true;
        rarityText.enabled = true;
        icon.enabled = true;
        descriptionText.enabled = true;
    }
}
