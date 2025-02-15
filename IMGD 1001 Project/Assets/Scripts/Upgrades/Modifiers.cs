using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
//Modifiers are upgrades that have passive effects and can be stacked
public abstract class Modifier : Upgrade
{
    public int stacks { get; protected set; } = 1; //The number of times the modifier has been applied to the player

    public int getStacks() { return stacks; }
    public void addStack() { stacks += 1; }

    public virtual void OnApply(Paddle player) { } //Use if the modifier has some kind of affect when it is applied
    public virtual void StatChange(Paddle player) { } //Use if the modifier changes a player's stats in some way
    public virtual void OnBallHit(Ball ball) { } //Use if the modifier has some kind of affect when the player hits the ball

}

[System.Serializable]
public class ModifierPanel
{
    public Modifier modifier;
    public string name;
    public int stacks;

    public ModifierPanel(Modifier modifier)
    {
        this.modifier = modifier;
        name = modifier.Name;
        stacks = modifier.getStacks();
    }
}

[System.Serializable]
public class SpeedBuff : Modifier
{

    [property:SerializeField] public override string Name{get{return "Speed Up!";}}
    public override string Description { get { return "Increase your paddle's move speed"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } //TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }
    public override void OnApply(Paddle player)
    {
        StatChange(player);
    }
    public override void StatChange(Paddle player)
    {
        player.statHandler.SetStat(player, "speed", player.statHandler.GetBaseStats(player).GetStat("speed") + 3 + (2 * stacks)); //Increase the player's speed by 3 + 2 * stacks
    }

}
