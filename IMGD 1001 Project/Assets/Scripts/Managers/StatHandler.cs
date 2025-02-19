using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatHandler : MonoBehaviour
{
    public Dictionary<Paddle, StatsList> playerStats = new Dictionary<Paddle, StatsList>(); //Dictionary of player stats

    //Base stats
    public readonly float baseSpeed = 10f;
    public readonly float baseSize = 0.8f;

    private void Awake()
    {
        if (FindAnyObjectByType<StatHandler>() != this) //If there's already a StatHandler in the scene, destroy this one
        {
            Destroy(this);
        }
    }

    private void AddPlayer(Paddle player) //Add a new player to the dictionary with base stats
    {
        playerStats.Add(player, new StatsList()); 

        //Add the base stats to the player
        playerStats[player].AddStat("speed", baseSpeed);
        playerStats[player].AddStat("size", baseSize);
    }

    public StatsList GetStats(Paddle player)
    {
        if (playerStats.ContainsKey(player)) //Return the stats of the player if they're in the dictionary
        {
            return playerStats[player];
        }
        else //Otherwise, add them to the dictionary and return their stats
        {
            AddPlayer(player);
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

    private void Start()
    {
        StartCoroutine(UpdateStats());
    }

    IEnumerator UpdateStats() //Update the stats of all players every 0.2 seconds
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            foreach (Paddle player in FindObjectsOfType<Paddle>())
            {
                player.UpdateStats();
            }
        }
    }
}
