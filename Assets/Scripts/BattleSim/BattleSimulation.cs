using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleSimulation : ScriptableObject 
{
    public static BattleStats Battle(Creature creatureA, Creature creatureB, Battleground battleground)
    {
        BattlegroundStats bgStats = new BattlegroundStats();//ScriptableObject.CreateInstance<BattlegroundStats>();
        bgStats.GenerateBattlegroundStats(battleground);

        creatureA.ApplyGenesPerBattleground(bgStats);
        creatureB.ApplyGenesPerBattleground(bgStats);

        return DoBattleSimulation(creatureA, creatureB);
    }

    static BattleStats DoBattleSimulation(Creature creatureA, Creature creatureB)
    {
        BattleStats stats = new BattleStats();

        CreatureBattleStats creatureStatsA = new CreatureBattleStats();
        CreatureBattleStats creatureStatsB = new CreatureBattleStats();
        stats.battleStatsPerCreature.Add(creatureA, creatureStatsA);
        stats.battleStatsPerCreature.Add(creatureB, creatureStatsB);

        while (creatureA.healthPoints > 0 && creatureB.healthPoints > 0)
        {
            creatureA.DoTurn(creatureB);
            creatureB.DoTurn(creatureA);

            stats.duration++;
        }

        stats.battleStatsPerCreature[creatureA].remainingHP = creatureA.healthPoints;
        stats.battleStatsPerCreature[creatureB].remainingHP = creatureB.healthPoints;
        stats.winner = creatureB.healthPoints > 0 ? creatureB : creatureA;

        return stats;
    }
}


