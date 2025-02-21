using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSelectionScreen : MonoBehaviour
{
    [SerializeField] private UpgradeButton[] modifierButtons;
    [SerializeField] private Button rerollButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_Text playerText;

    private GameManager gameManager;
    private ModifierHandler modifierHandler;

    public Upgrade SelectedUpgrade { get; private set; }

    public Paddle targetedPlayer;

    //Unity Methods
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        modifierHandler = FindObjectOfType<ModifierHandler>();
        confirmButton.onClick.AddListener(ConfirmSelection);
        rerollButton.onClick.AddListener(RerollUpgrades);

        modifierButtons = GetComponentsInChildren<UpgradeButton>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }


    //Selection Logic
    public void SelectUpgrade(Upgrade upgrade)
    {
        SelectedUpgrade = upgrade;
        Debug.Log("Selected upgrade: " + SelectedUpgrade.Name);
    }

    public void ConfirmSelection()
    {
        if (SelectedUpgrade != null)
        {
            //If the upgrade is a modifier, add it to the player as one
            if (SelectedUpgrade is Modifier)
            {
                modifierHandler.AddModifier(targetedPlayer, (Modifier)SelectedUpgrade);
            } //TODO: ADD ACTIVE ABILITY LOGIC HERE
        }

        this.gameObject.SetActive(false);
    }


    //Generic Methods
    public void RerollUpgrades()
    {
        foreach (UpgradeButton button in modifierButtons)
        {
            button.ChangeUpgrade(modifierHandler.GetRandomModifier());
        }

        SelectedUpgrade = null;
    }

    public void StartPicking(Paddle player)
    {
        Debug.Log("Starting upgrade selection for " + player.gameObject.name);
        this.gameObject.SetActive(true);
        playerText.text = player.gameObject.name;
        targetedPlayer = player;
        RerollUpgrades();
    }
}
