using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatHandler : MonoBehaviour
{
    public Dictionary<Paddle, StatsList> playerStats = new Dictionary<Paddle, StatsList>(); //Dictionary of player stats
    public Dictionary<Paddle, StatsList> baseStats = new Dictionary<Paddle, StatsList>(); //Dictionary of base stats

    //Base stats
    public float baseSpeed = 10f;

    private void Awake()
    {
        if (FindAnyObjectByType<StatHandler>() != this) //If there's already a StatHandler in the scene, destroy this one
        {
            Destroy(this);
        }
    }

    private void AddPlayer(Paddle player)
    {
        playerStats.Add(player, new StatsList()); //Add the player to the dictionary

        //Add the base stats to the player
        playerStats[player].AddStat("speed", baseSpeed);
        playerStats[player].AddStat("size", player.transform.localScale.y);

        //Add the base stats to the base stats dictionary
        baseStats.Add(player, playerStats[player]);
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
    public StatsList GetBaseStats(Paddle player)
    {
        if (baseStats.ContainsKey(player)) //Return the base stats of the player if they're in the dictionary
        {
            return baseStats[player];
        }
        else //Otherwise, add them to the dictionary and return their base stats
        {
            AddPlayer(player);
            return baseStats[player];
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

    //Update the stats of all players every 0.2 seconds
    IEnumerator UpdateStats()
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
