using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleSimulation : ScriptableObject 
{
    public static BattleStats Battle(Creature creatureA, Creature creatureB, Battleground battleground)
    {
        //not not alter existing creatures, so make clones
        //Creature creatureAClone = Object.Instantiate(creatureA) as Creature;
        //Creature creatureBClone = Object.Instantiate(creatureB) as Creature;

        BattlegroundStats bgStats = new BattlegroundStats();
        bgStats.GenerateBattlegroundStats(battleground);

        creatureA.ApplyGenesPerBattleground(bgStats);
        creatureA.ApplyGenesPerBattleground(bgStats);

        return DoBattleSimulation(creatureA, creatureB);
    }

    static BattleStats DoBattleSimulation(Creature creatureA, Creature creatureB)
    {
        BattleStats stats = new BattleStats();
        /*
        //Random swap to choose starting creature
        if (Random.value > 0.5f)
        {
            Creature temp = creatureA;
            creatureA = creatureB;
            creatureB = temp;
        }*/

        stats.battleStatsPerCreature.Add(creatureA, new BattleStats.CreatureBattleStats());
        stats.battleStatsPerCreature.Add(creatureB, new BattleStats.CreatureBattleStats());

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
        }

        stats.battleStatsPerCreature[creatureA].remainingHP = creatureA.healthPoints;
        stats.battleStatsPerCreature[creatureB].remainingHP = creatureB.healthPoints;
        stats.winner = creatureB.healthPoints > 0 ? creatureB : creatureA;

        return stats;
    }
}
