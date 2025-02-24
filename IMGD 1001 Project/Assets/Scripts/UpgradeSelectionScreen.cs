using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Upgrade;

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
        
        List<Modifier> alreadyShownmodifierList = new List<Modifier>();

       

        
            
            foreach (UpgradeButton button in modifierButtons)
            {
                bool validModifier = false; 
                while (!validModifier)
                {


                    upgradeRarities rarity;

                    float random = Random.value;


                    if (random < 0.34)//picls a random rarity
                    {
                    rarity = upgradeRarities.Common;
                    }
                    else if (random < 0.61)
                    {
                    rarity = upgradeRarities.Uncommon;
                    }
                    else if (random < 0.81)
                    {
                    rarity = upgradeRarities.Rare;
                    }
                    else if (random < 0.94)
                    {
                        rarity = upgradeRarities.Epic;
                    }
                    else 
                    {
                        rarity = upgradeRarities.Legendary;
                    }
                    bool boolvalidRarity = false;
                    Modifier newModifier = modifierHandler.GetRandomModifier();

                    while (!boolvalidRarity)//finds a modifer of the random rarity
                    {
                        newModifier = modifierHandler.GetRandomModifier();
                        if (newModifier.Rarity == rarity)
                        {
                             boolvalidRarity = true;
                        }
                    }

                    validModifier = true;
                    
                    foreach (Modifier modifier in alreadyShownmodifierList)//makes sure our modifer is not already chosen
                    {
                   
                        if (newModifier == modifier)// makes sure our modifer is not on another button
                    {
                            validModifier = false;
                        }
                        if(newModifier.Rarity != rarity)//makes sure the modifer is of the right rarity
                        {
                            validModifier = false;
                        }


                    }

                    button.ChangeUpgrade(newModifier);
                 alreadyShownmodifierList.Add(newModifier);
                }
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
