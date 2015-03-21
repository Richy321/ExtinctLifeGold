using UnityEngine;
using System.Collections;
using System;

public abstract class CreatureModifier : MonoBehaviour 
{
    [FlagsAttribute]
    public enum ModifierFlags
    {
        AddHP = 0,
        AddAP = 1,
        AddGroundMovementSpeed = 2,
        AddWaterMovementSpeed = 4,
        AddMeleeDamage = 8,
        AddRangedDamage = 16,
        AddHealAbilty = 32
    };

    public abstract void ApplyModifier(Creature creature);
}
