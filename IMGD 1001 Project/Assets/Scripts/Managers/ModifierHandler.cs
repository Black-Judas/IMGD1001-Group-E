using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierHandler : MonoBehaviour
{
    public void addModifier(Paddle player, Modifier modifier)
    {

        Debug.Log("Adding modifier: " + modifier.Name);
        if (player.modifiers.Contains(modifier)) //If the player already has the modifier, add a stack
        {
            player.modifiers[player.modifiers.IndexOf(modifier)].addStack();
            player.activeModifiers[player.activeModifiers.IndexOf(player.activeModifiers.Find(x => x.modifier == modifier))].stacks += 1;
        }
        else //Otherwise, add the modifier to the player
        {
            player.modifiers.Add(modifier);
            player.activeModifiers.Add(new ModifierPanel(modifier));
        }

        player.modifiers[player.modifiers.IndexOf(modifier)].OnApply(player); //Trigger the OnApply method of the modifier

    }
}
