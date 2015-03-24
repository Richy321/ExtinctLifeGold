using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleSimulation : ScriptableObject 
{
    public static BattleStats Battle(Creature creatureA, Creature creatureB, Battleground battleground)
    {
        BattlegroundStats bgStats = ScriptableObject.CreateInstance<BattlegroundStats>();
        bgStats.GenerateBattlegroundStats(battleground);

        creatureA.ApplyGenesPerBattleground(bgStats);
        creatureA.ApplyGenesPerBattleground(bgStats);

        return DoBattleSimulation(creatureA, creatureB);
    }

    static BattleStats DoBattleSimulation(Creature creatureA, Creature creatureB)
    {
        BattleStats stats =  ScriptableObject.CreateInstance<BattleStats>();
        
        
        /*
        //Random swap to choose starting creature
        if (Random.value > 0.5f)
        {
            Creature temp = creatureA;
            creatureA = creatureB;
            creatureB = temp;
        }*/

        CreatureBattleStats creatureStatsA = ScriptableObject.CreateInstance<CreatureBattleStats>();
        CreatureBattleStats creatureStatsB = ScriptableObject.CreateInstance<CreatureBattleStats>();
        stats.battleStatsPerCreature.Add(creatureA, creatureStatsA);
        stats.battleStatsPerCreature.Add(creatureB, creatureStatsB);

        while (creatureA.healthPoints > 0 && creatureB.healthPoints > 0)
        {
            //do turn
            if (creatureA.healthPoints > 0)
            {
                creatureA.BasicAttack(creatureB);
            }

            if (creatureB.healthPoints > 0)
            {
                creatureB.BasicAttack(creatureA);
            }

            stats.duration++;
        }

        stats.battleStatsPerCreature[creatureA].remainingHP = creatureA.healthPoints;
        stats.battleStatsPerCreature[creatureB].remainingHP = creatureB.healthPoints;
        stats.winner = creatureB.healthPoints > 0 ? creatureB : creatureA;

        return stats;
    }
}
