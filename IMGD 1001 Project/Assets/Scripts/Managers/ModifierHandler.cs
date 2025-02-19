using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ModifierHandler : MonoBehaviour
{
    [SerializeField] private Dropdown modifierDropdown;

    private List<Modifier> modifierList = new List<Modifier>(); // THIS IS A LIST OF ALL MODIFIERS THAT CAN BE ADDED TO THE PLAYERS. ADD ANY NEW MODIFIERS IN THE AWAKE METHOD

    private void Awake()
    {
        //Add all modifiers to the list
        modifierList.Add(new SpeedBuff());
        modifierList.Add(new SizeBuff());
        modifierList.Add(new RedBall());

        //Clear the dropdown and add all modifiers from the list to it
        Debug.Log("Adding modifiers to "+modifierDropdown);
        modifierDropdown.ClearOptions();
        foreach (Modifier modifier in modifierList)
        {
            modifierDropdown.AddOption(modifier.Name);
        }
    }

    //Debug methods
    public void DebugAddModifier()
    {
        //Add the modifier that's selected in the dropdown to all players
        foreach (Paddle player in FindObjectsOfType<Paddle>())
        {
            AddModifier(player, FindModifierByName(modifierDropdown.GetSelectedOption()));
        }
        //Send an error message if the modifier doesn't exist
        if (FindModifierByName(modifierDropdown.GetSelectedOption()) == null)
        {
            Debug.LogError("Could not add modifier: " + modifierDropdown.GetSelectedOption());
        }
    }
    public void DebugClearAllModifiers()
    {
        //Remove all modifiers from all players
        foreach (Paddle player in FindObjectsOfType<Paddle>())
        {
            ClearModifiers(player);
        }
    }

    //Utility methods
    public Modifier FindModifierByName(string name)
    {
        //Find a modifier by its name
        foreach (Modifier modifier in modifierList)
        {
            if (modifier.Name == name)
            {
                return modifier;
            }
        }

        //if the modifier isn't found, return null and send an error message
        Debug.LogError("Modifier not found: " + name);
        return null;
    }


    public void AddModifier(Paddle player, Modifier newModifier)
    {

        Debug.Log("Adding modifier \""  + newModifier.Name + "\" to "+player.gameObject.name);
        //If the player's modifier list contains a modifier of the same type as the new modifier, add a stack to the existing modifier
        if (player.modifiers.Any(x => x.GetType() == newModifier.GetType()))
        {
            player.modifiers[player.modifiers.IndexOf(player.modifiers.Find(x => x.GetType() == newModifier.GetType()))].AddStack(1);
            player.activeModifiers[player.activeModifiers.IndexOf(player.activeModifiers.Find(x => x.modifier.GetType() == newModifier.GetType()))].stacks += 1;
        }
        else //Otherwise, add the modifier to the player and set its stacks to 1
        {
            //Instantiate a new modifier of the same type as the modifier in the list
            newModifier = (Modifier)System.Activator.CreateInstance(newModifier.GetType());

            newModifier.SetPlayer(player);
            newModifier.SetStacks(1);
            player.modifiers.Add(newModifier);
            player.activeModifiers.Add(new ModifierPanel(newModifier));
        }

    }
    public void RemoveModifier(Paddle player, Modifier modifier)
    {
        // If the player has the modifier, remove a stack
        if (player.modifiers.Contains(modifier))
        {
            player.modifiers[player.modifiers.IndexOf(modifier)].AddStack(-1);
            player.activeModifiers[player.activeModifiers.IndexOf(player.activeModifiers.Find(x => x.modifier == modifier))].stacks -= 1;

            // If the player has no more stacks of the modifier, remove the modifier from the player
            if (player.modifiers[player.modifiers.IndexOf(modifier)].GetStacks() == 0)
            {
                player.modifiers[player.modifiers.IndexOf(modifier)].OnRemove(); //Trigger the OnRemove method of the modifier
                player.modifiers.Remove(modifier);
                player.activeModifiers.Remove(player.activeModifiers.Find(x => x.modifier == modifier));
            }
        }
    }
    public void ClearModifiers(Paddle player)
    {
        foreach (Modifier modifier in player.modifiers)
        {            
            player.modifiers[player.modifiers.IndexOf(modifier)].ClearStacks(); //Clear the stacks of the modifier
            player.modifiers[player.modifiers.IndexOf(modifier)].OnRemove(); //Trigger the OnRemove method of the modifier
        }
        player.modifiers.Clear();
        player.activeModifiers.Clear();
    }
}
