using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Modifiers are upgrades that have passive effects and can be stacked
[System.Serializable]
public abstract class Modifier : Upgrade
{
    //Stack handling
    public int stacks { get; protected set; } = 1; // The number of times the modifier has been applied to the player
    public int GetStacks() { return this.stacks; }
    public void SetStacks(int i) { this.stacks = i; OnApply(); }
    public void AddStack(int i = 1) { this.stacks += i; OnApply(); }
    public void ClearStacks() { this.stacks = 0; OnApply(); }

    //Methods
    public virtual void OnApply() { } // Use if the modifier has some kind of affect when it is applied
    public virtual void OnRemove() { } // Use if the modifier has some kind of affect when it is removed
    public virtual void StatChange() { } // Use if the modifier changes a player's stats in some way
    public virtual void OnBallHit(Ball ball) { } // Use if the modifier has some kind of affect when the player hits the ball

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
        this.name = modifier.Name;
        this.stacks = modifier.stacks;
    }
}

[System.Serializable]
public class SpeedBuff : Modifier
{
    // Properties
    [property:SerializeField] public override string Name{get{return "Speed Up!";}}
    public override string Description { get { return "Increase your paddle's move speed"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }


    public override void OnApply()
    {
        StatChange();
    }
    public override void OnRemove()
    {
        StatChange();
    }
    public override void StatChange()
    {
        if (this.stacks != 0)
        {
            this.player.statHandler.SetStat(this.player, "speed", this.player.statHandler.baseSpeed + 3 + (2 * this.stacks)); // Set the player's speed to 3 + 2 * stacks
        }
        else
        {
            this.player.statHandler.SetStat(this.player, "speed", this.player.statHandler.baseSpeed); // Set the player's speed to the base speed
        }
    }
}

[System.Serializable]
public class SizeBuff : Modifier
{
    // Properties
    [property: SerializeField] public override string Name { get { return "Grow"; } }
    public override string Description { get { return "Increase the length of your paddle to cover more ground"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }


    public override void OnApply()
    {
        StatChange();
    }
    public override void OnRemove()
    {
        StatChange();
    }
    public override void StatChange()
    {
        if (this.stacks != 0)
        {
            this.player.statHandler.SetStat(this.player, "size", this.player.statHandler.baseSize * 1.2f + (0.1f * this.stacks)); // Set the player's speed to 3 + 2 * stacks
        }
        else
        {
            this.player.statHandler.SetStat(this.player, "size", this.player.statHandler.baseSize); // Set the player's speed to the base speed
        }
    }
}

[System.Serializable]
public class RedBall : Modifier
{
    // Properties
    [property: SerializeField] public override string Name { get { return "Red Ball"; } }
    public override string Description { get { return "Turns the ball red when you hit it"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }

    public override void OnBallHit(Ball ball)
    {
        ball.GetComponent<SpriteRenderer>().color = Color.red;
    }
}