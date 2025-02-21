using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private Upgrade heldUpgrade;
    private TMP_Text nameText;
    private TMP_Text descriptionText;
    private UpgradeSelectionScreen upgradeSelectionScreen;
    public Button button { get; private set; }

    private void Awake()
    {
        nameText = transform.Find("Name").GetComponent<TMP_Text>();
        descriptionText = transform.Find("Description").GetComponent<TMP_Text>();
        button = GetComponent<Button>();
        upgradeSelectionScreen = FindObjectOfType<UpgradeSelectionScreen>();

        //Set the selected upgrade to the upgrade that this button holds when clicked
        button.onClick.AddListener(() => upgradeSelectionScreen.SelectUpgrade(heldUpgrade));

    }

    private void UpdateText()
    {
        nameText.text = heldUpgrade.Name;
        descriptionText.text = heldUpgrade.Description;
    }

    public void ChangeUpgrade(Upgrade newUpgrade)
    {
        heldUpgrade = newUpgrade;
        UpdateText();
    }

    public Upgrade CheckUpgrade()
    {
        return heldUpgrade;
    }

}
