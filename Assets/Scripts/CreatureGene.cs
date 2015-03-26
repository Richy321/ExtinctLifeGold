using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
        None = 0,
        HP_Small = 1,
        HP_Medium = 2,
        RunningChargeAttack = 4,
        AddMeleeDamage = 8,
        AddRangedDamage = 16,
        HealAbilty = 32,
        LastEntry = HealAbilty
    };

    public class CreatureGeneModifier
    {
        public CreatureGeneModifier(Tile.TileTypes tileType, CreatureStatType statType, float modValue)
        {
            this.tileType = tileType;
            this.statType = statType;
            this.modifierValue = modValue;
        }

        public enum CreatureStatType
        {
            HP, 
            AP,
            AttackDamage,
            AttackCost,
            Armor
        }

        public Tile.TileTypes tileType = Tile.TileTypes.Grass;
        public CreatureStatType statType = CreatureStatType.HP;
        public float modifierValue = 1.0f;
    }

    public abstract class CreatureAbility
    {
        public enum AbilityType
        {
            Heal =0,
            Attack =1,
            Buff =2,
            Debuff =3
        }

        public AbilityType abilityType = AbilityType.Attack;
        public int chargesRemaining = 1;

        public abstract void Use(Creature self, Creature enemy);
    }

    public List<CreatureGeneModifier> modifiers = new List<CreatureGeneModifier>();

    public List<CreatureAbility> abilities = new List<CreatureAbility>();
}
