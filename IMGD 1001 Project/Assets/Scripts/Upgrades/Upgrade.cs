using UnityEngine;
using UnityEngine.UI;

//Modifiers and abilities all ultimately inherit from this class
public abstract class Upgrade
{
    public abstract string Name { get; } //The name of the upgrade to refer to in UI and other possible cases
    public abstract string Description { get; } //The description explaining what the upgrade actually does
    public abstract Sprite Icon { get; } //The picture to display for the upgrade
    public enum upgradeRarities
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Developer // new rarity for dev stuff, not to be added to real game
    }
    public abstract upgradeRarities Rarity { get; } //The rarity level of the upgrade

    protected Paddle player; // The player that the modifier is applied to
    public void SetPlayer(Paddle player) { this.player = player; } // Set the player that the modifier is applied to
}

public class RarityColors
{
    public Color Common = Color.white;
    public Color Uncommon = Color.green;
    public Color Rare = Color.red;
    public Color Epic = Color.magenta;
    public Color Legendary = Color.yellow;

    //Retrieve the color associated with the given rarity
    public Color GetColor(Upgrade.upgradeRarities upgradeRarity)
    {
        switch (upgradeRarity)
        {
            case Upgrade.upgradeRarities.Common:
                return Common;
            case Upgrade.upgradeRarities.Uncommon:
                return Uncommon;
            case Upgrade.upgradeRarities.Rare:
                return Rare;
            case Upgrade.upgradeRarities.Epic:
                return Epic;
            case Upgrade.upgradeRarities.Legendary:
                return Legendary;
            default:
                return Color.white;
        }
    }
}
