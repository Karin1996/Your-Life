using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public Dictionary<string, int> stats; 

    public PlayerStats()
    {
        stats = new Dictionary<string, int>()
        {
            { "knowledge", UnityEngine.Random.Range(1, 6) },
            { "friendship",  UnityEngine.Random.Range(1, 6) },
            { "ambition",  UnityEngine.Random.Range(1, 6) },
            { "independence",  UnityEngine.Random.Range(1, 6) }
        };
    }

    public void updateStat(string stat, int value)
    {
        int updatedValue = stats[stat] + value;

        if (updatedValue > 10)
        {
            updatedValue = 10;
        }

        if (updatedValue < 0)
        {
            updatedValue = 0;
        }

        stats[stat] = updatedValue;
    }

    public int getStat(string stat)
    {
        return stats[stat];
    }
}
