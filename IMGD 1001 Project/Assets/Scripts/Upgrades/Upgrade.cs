using UnityEngine;
using UnityEngine.UI;

//Modifiers and abilities all ultimately inherit from this class
public abstract class Upgrade 
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

    protected Paddle player; // The player that the modifier is applied to
    public void SetPlayer(Paddle player) { this.player = player; } // Set the player that the modifier is applied to
}
