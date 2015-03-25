using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStats //: ScriptableObject 
{
    public int duration = 0;

    public Creature winner;
    public Dictionary<Creature, CreatureBattleStats> battleStatsPerCreature = new Dictionary<Creature, CreatureBattleStats>();
}

public class CreatureBattleStats //: ScriptableObject
{
    public float remainingHP;
}
