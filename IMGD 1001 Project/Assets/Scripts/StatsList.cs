using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public string _name { get; }
    public float _value { get; set; }

    public Stat(string name, float value)
    {
        _name = name;
        _value = value;
    }

}

[System.Serializable]
public class StatsList
{
    [field:SerializeField] public List<Stat> stats { get; private set; } = new List<Stat>();
    public void AddStat(string name, float value) //Add a stat to the list
    {
        stats.Add(new Stat(name, value));
    }
    public void RemoveStat(string name) //Remove a stat from the list
    {
        stats.Remove(stats.Find(x => x._name == name));
    }
    public void SetStat(string name, float value) //Set a stat to a specific value if it exists, otherwise add it
    {
        Stat stat = stats.Find(x => x._name == name);
        if (stat != null)
        {
            stat._value = value;
        }
        else
        {
            AddStat(name, value);
        }
    }
    public float GetStat(string name) //Get the value of a stat
    {
        return stats.Find(x => x._name == name)._value;
    }

    public override string ToString() //Return a string of all the stats
    {
        string result = "";
        foreach (Stat stat in stats)
        {
            result += stat._name + ": " + stat._value + "\n";
        }
        return result;
    }
}
