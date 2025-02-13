using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[System.Serializable]
public class ModifierList
{
    public Modifier modifier;
    public string name;
    public int stacks;

    public ModifierList(Modifier newModifier, string newName, int newStacks) 
    { 
        modifier = newModifier;
        name = newName;
        stacks = newStacks;
    }
}
