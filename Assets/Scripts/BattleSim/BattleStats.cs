﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleStats
{
    public int duration = 0;

    public Creature winner;
    public Dictionary<Creature, CreatureBattleStats> battleStatsPerCreature = new Dictionary<Creature, CreatureBattleStats>();
}

public class CreatureBattleStats
{
    public float remainingHP;
}
