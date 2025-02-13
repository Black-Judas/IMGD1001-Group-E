using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public abstract class Upgrade //Modifiers and abilities all ultimately inherit from this class
{
    public abstract string Name { get; } //The name of the upgrade to refer to in UI and other possible cases
    public abstract string Description { get; } //The description explaining what the upgrade actually does
    public abstract Image Image { get; } //The picture to display for the upgrade (NOT YET IMPLEMENTED)
    public enum upgradeRarities
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public abstract upgradeRarities Rarity { get; } //The rarity level of the upgrade
}
