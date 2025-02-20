using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    public virtual void OnEnemyBallHit(Ball ball) { } // USe if the modifer has effect when the other player hits the ball

    public virtual void OnReset(Ball ball) { } // Use if on start of point you want smthn to happen

    public virtual void OnUpdate(Ball ball) { } //Use on update


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
public class RedBallBlueBall : Modifier
{
    // Properties
    [property: SerializeField] public override string Name { get { return "Red Ball Blue Ball"; } }
    public override string Description { get { return "Turns the ball red when you hit it, and the ball blue when your oppenthits it"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Devolper; } }

    public override void OnBallHit(Ball ball)
    {
        ball.GetComponent<SpriteRenderer>().color = Color.red;
    }

    public override void OnEnemyBallHit(Ball ball)
    {
        ball.GetComponent<SpriteRenderer>().color = Color.blue;
    }
}






public class SpeedBall : Modifier
{
    // Properties
    [property: SerializeField] public override string Name { get { return "SpeedBall"; } }
    public override string Description { get { return "Speeds ball up until your oppent hits it"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }



    public override void OnBallHit(Ball ball)
    {
        
        Vector2 velo = ball.GetVelocity();
        int force = (this.stacks * 4) + 8;

        if (velo[0] > 0)
        {
            ball.AddForce(new Vector2(force, 0));
        }
        else
        {
            ball.AddForce(new Vector2(-force, 0));

        }
    }

    public override void OnEnemyBallHit(Ball ball)
    {
        if (ball.hasBeenHit)
        {
            Vector2 velo = ball.GetVelocity();
            int force = -((this.stacks * 4) + 8);

            if (velo[0] > 0)
            {
                ball.AddForce(new Vector2(force, 0));
            }
            else
            {
                ball.AddForce(new Vector2(-force, 0));

            }
        }
    }


}


public class Gamble : Modifier
{
    // Properties
    [property: SerializeField] public override string Name { get { return "Gamble"; } }
    public override string Description { get { return "Small chance for ball to get a crazy boost on hit"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }


     bool gamble = false;
    public override void OnBallHit(Ball ball)
    {

        if (Random.value <= (0.1 + this.stacks * 0.5))
        {
             gamble = true;
            Vector2 velo = ball.GetVelocity();
            int force = (this.stacks * 5) + 50;

            if (velo[0] > 0)
            {
                ball.AddForce(new Vector2(force, 0));
            }
            else
            {
                ball.AddForce(new Vector2(-force, 0));

            }
        }

    }


    public override void OnEnemyBallHit(Ball ball)
    {
        if (ball.hasBeenHit)
        {
            if (gamble == true)
            {
                gamble = false;
                Vector2 velo = ball.GetVelocity();
                int force = -((this.stacks * 10) + 50);

                if (velo[0] > 0)
                {
                    ball.AddForce(new Vector2(force, 0));
                }
                else
                {
                    ball.AddForce(new Vector2(-force, 0));

                }
            }
        }
    }


    public override void OnReset(Ball ball)
    {
        gamble = false;
    }


}


public class Invisiball : Modifier
{
    // Properties
    [property: SerializeField] public override string Name { get { return "Invisiball"; } }
    public override string Description { get { return "Turn the ball a random amount of transulence for a small distane "; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }



    public override void OnBallHit(Ball ball)
    {
        Color ballColor = ball.GetComponent<SpriteRenderer>().color;
        Color newBallColor = ballColor;
        //newBallColor.a = Random.value - ((0.2f * this.stacks));
        newBallColor.a = 0;
        ball.GetComponent<SpriteRenderer>().color = newBallColor;

    }

    public override void OnUpdate(Ball ball)
    {
        Color ballColor = ball.GetComponent<SpriteRenderer>().color;
        if (ballColor.a < 1)
        {
            ballColor.a = ballColor.a + (0.00002f - (this.stacks * 0.000005f));
            ball.GetComponent<SpriteRenderer>().color = ballColor;

        }
    }

    public override void OnReset(Ball ball)
    {
        Color ballColor = ball.GetComponent<SpriteRenderer>().color;
        ballColor.a = 1;
        ball.GetComponent<SpriteRenderer>().color = ballColor;

    }


    public override void OnEnemyBallHit(Ball ball)
    {
        Color ballColor = ball.GetComponent<SpriteRenderer>().color;
        ballColor.a = 1;
        ball.GetComponent<SpriteRenderer>().color = ballColor;

    }



}


public class FastPitch : Modifier
{
    // Properties
    [property: SerializeField] public override string Name { get { return "FastPitch"; } }
    public override string Description { get { return "The ball moves Lickity Split until it reaches the middle "; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }

    bool sloweddown = true;

    int playerNumber = 0;

    public override void OnBallHit(Ball ball)
    {
        Vector2 velo = ball.GetVelocity();
        int force = (this.stacks * 10) + 50;
        sloweddown = false;

        if (ball.transform.position.x < 0)
        {
            playerNumber = 1;
            ball.AddForce(new Vector2(force, 0));

        } else
        {
            playerNumber = 2;
            ball.AddForce(new Vector2(-force, 0));
        }
         
        
 

    }

    public override void OnUpdate(Ball ball)
    {
        int force = (this.stacks * 10) + 50;
        Vector2 ballpos = ball.GetPosition();
       

        if (playerNumber == 2)
        {
            if (ballpos[0] < 0)
            {
                if (sloweddown == false)
                {
                  ball.AddForce(new Vector2(force, 0));
                    sloweddown = true;
                   
                }
            }
        } else
        {
            if (ballpos[0] > 0)
            {
                if (sloweddown == false)
                {
                    ball.AddForce(new Vector2(-force, 0));
                    sloweddown = true;
                   
                }
            }
        }
     }


    public override void OnReset(Ball ball)
    {
        sloweddown = true;
       
    }



}


public class smallball : Modifier
{
    // Properties
    [property: SerializeField] public override string Name { get { return "smallball"; } }
    public override string Description { get { return "Shrinks the balls size, but dont worry its just as deadly"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }




    public override void OnBallHit(Ball ball)
    {
        Vector3 ballScale = ball.transform.localScale;

        ballScale = ballScale * (0.75f - this.stacks * 0.05f); 
        ball.transform.localScale = ballScale;





        Vector2 velo = ball.GetVelocity();
        int force =  2;

        if (velo[0] > 0)
        {
            ball.AddForce(new Vector2(-force, 0));
        }
        else
        {
            ball.AddForce(new Vector2(force, 0));

        }
    }

    

    public override void OnEnemyBallHit(Ball ball)
    {
        if (ball.hasBeenHit)
        {
            Vector3 ballScale = ball.transform.localScale;

            ballScale = ballScale / (0.75f - this.stacks * 0.05f);
            ball.transform.localScale = ballScale;
        }

    }

    public override void OnReset(Ball ball)
    {
   
        
            ball.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
 

    }


}




public class bigball : Modifier
{
    // Properties
    [property: SerializeField] public override string Name { get { return "bigball"; } }
    public override string Description { get { return "makes the ball large when your enemy hits hit"; } }
    public override UnityEngine.UI.Image Image { get { return null; } } // TODO: ADD IMAGE
    public override upgradeRarities Rarity { get { return upgradeRarities.Common; } }





    public override void OnBallHit(Ball ball)
    {
        Vector3 ballScale = ball.transform.localScale;
        if (ball.hasBeenHit)
        {
            ballScale = ballScale / (1.5f + this.stacks * 0.15f);
            ball.transform.localScale = ballScale;
        }
        





    }

    public override void OnEnemyBallHit(Ball ball)
    {


        Vector3 ballScale = ball.transform.localScale;
        ballScale = (ballScale * (1.5f+ this.stacks * 0.15f));

        ball.transform.localScale = ballScale;

    }

    public override void OnReset(Ball ball)
    {


        ball.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);


    }


}