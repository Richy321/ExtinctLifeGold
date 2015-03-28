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

    public virtual string description
    {
        get { return flag.ToString(); }
    }
    protected static float smallAttackDamageBonus = 2.0f;
    protected static float mediumAttackDamageBonus = 10.0f;
    protected static float largeAttackDamageBonus = 25.0f;

    protected static float smallHPBonus = 2.0f;
    protected static float mediumHPBonus = 10.0f;
    protected static float largeHPBonus = 25.0f;

    [Flags]
    public enum GeneFlags
    {
        HP_Small = 1,
        HP_Medium = 2,
        HP_Large= 4,
        WaterAttackEnchant = 8,
        LandAttackEnchant = 16,
        HealAbilty = 32,
        LandHP = 64,
        WaterHP = 128,
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
