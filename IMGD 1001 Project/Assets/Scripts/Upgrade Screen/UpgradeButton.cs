using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{

    private UpgradeSelectionScreen upgradeSelectionScreen;

    private Upgrade heldUpgrade; //Keeps track of which upgrade the button holds and displays
    private RarityColors rarityColorReference = new RarityColors(); //Reference to the colors associated with each rarity (can be changed in Upgrade.cs)

    //BUTTON TEXT
    private TMP_Text nameText;
    //private TMP_Text descriptionText;
    private TMP_Text rarityText;
    private Color textColor;

    //BUTTON COMPONENTS
    public Button button { get; private set; }
    private Image buttonImage;


    private void Awake()
    {

        nameText = transform.Find("Name").GetComponent<TMP_Text>();
        //descriptionText = transform.Find("Description").GetComponent<TMP_Text>();
        rarityText = transform.Find("Rarity").GetComponent<TMP_Text>();
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        upgradeSelectionScreen = FindObjectOfType<UpgradeSelectionScreen>();

        //Set the selected upgrade to the upgrade that this button holds when clicked
        button.onClick.AddListener(() => upgradeSelectionScreen.SelectUpgrade(heldUpgrade));

    }

    private void Update()
    {
        UpdateColor();
    }

    private void UpdateText()
    {
        nameText.text = heldUpgrade.Name;
        //descriptionText.text = heldUpgrade.Description;
        rarityText.text = heldUpgrade.Rarity.ToString();
    }

    public void ChangeUpgrade(Upgrade newUpgrade)
    {
        heldUpgrade = newUpgrade;
        UpdateText();
    }

    public Upgrade CheckUpgrade(){ return heldUpgrade; }

    private void UpdateColor()
    {
        Color buttonColor = button.image.canvasRenderer.GetColor();

        Upgrade.upgradeRarities rarityOfUpgrade = heldUpgrade.Rarity;

        textColor = rarityColorReference.GetColor(rarityOfUpgrade) * buttonColor;

        nameText.color = textColor;
        //descriptionText.color = textColor;
        rarityText.color = textColor;

    }

}
