using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Modifier : Upgrade
{
    public virtual float StatChange(Paddle player, float statToChange, int stacks) { return 0; } //Use if the modifier changes a player's stats in some way
    public virtual void OnBallHit(Ball ball) { } //Use if the modifier has some kind of affect when the player hits the ball

}

public class SpeedBuff : Modifier
{
    public override string Name{get{return "Speed Up!";}}
    public override string Description { get { return "Increase your paddle's move speed"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } //TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }


    public override float StatChange(Paddle player, float speed, int stacks)
    {
        return speed + 3 + (2*stacks);
    }

}

