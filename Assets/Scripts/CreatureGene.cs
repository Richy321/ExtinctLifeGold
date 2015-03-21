using UnityEngine;
using System.Collections;
using System;

public abstract class CreatureGene : ScriptableObject 
{
    public virtual GeneFlags flag
    {
        get { return GeneFlags.HP_Small; }
    }

    [Flags]
    public enum GeneFlags
    {
        HP_Small = 0,
        HP_Medium = 1,
        AddGroundMovementSpeed = 2,
        AddWaterMovementSpeed = 4,
        AddMeleeDamage = 8,
        AddRangedDamage = 16,
        AddHealAbilty = 32
    };

    public abstract void ApplyGene(Creature creature);
}
