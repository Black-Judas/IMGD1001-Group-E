using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatHandler : MonoBehaviour
{
    public Dictionary<Paddle, StatsList> playerStats = new Dictionary<Paddle, StatsList>(); //Dictionary of player stats

    private void Awake()
    {
        if (FindAnyObjectByType<StatHandler>() != this) //If there's already a StatHandler in the scene, destroy this one
        {
            Destroy(this);
        }
    }




    //Base stats
    public float baseSpeed = 10f;

    public StatsList GetStats(Paddle player)
    {
        if (playerStats.ContainsKey(player)) //Return the stats of the player if they're in the dictionary
        {
            return playerStats[player];
        }
        else //Otherwise, add them to the dictionary, give them their initial stats, and return them
        {
            playerStats.Add(player, new StatsList());
            playerStats[player].AddStat("speed", baseSpeed);
            playerStats[player].AddStat("size", player.transform.localScale.y);
            return playerStats[player];
        }
    }

    public void SetStat(Paddle player, string stat, float value)
    {
        if (playerStats.ContainsKey(player)) //Set the stat if the player is in the dictionary
        {
            playerStats[player].SetStat(stat, value);
        }
    }
}
