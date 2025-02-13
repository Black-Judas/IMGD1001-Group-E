using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Upgrade
{
    [SerializeField] public string upgradeName { get; private set ;} = "Name"; //The name of the upgrade to refer to in UI and other possible cases
    [SerializeField] public string upgradeDescription { get; private set; } = "Description"; //The description explaining what the upgrade actually does
    [SerializeField] public Image upgradeImage { get; private set; } = null; //The picture to display for the upgrade (NOT YET IMPLEMENTED)

    public enum upgradeRarities
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [SerializeField] public upgradeRarities upgradeRarity { get; private set; } = upgradeRarities.Common; //Default rarity is common unless specified

    public string GetName() {  return upgradeName; }
    public string GetDescription() { return upgradeDescription; }
    public Image GetImage() { return upgradeImage; }
    public upgradeRarities GetRarity() { return upgradeRarity; }

}
