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
        AddWaterMovementSpeed = 4,
        AddMeleeDamage = 8,
        AddRangedDamage = 16,
        AddHealAbilty = 32,
        LastEntry = AddHealAbilty
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
            AP
        }

        public Tile.TileTypes tileType = Tile.TileTypes.Grass;
        public CreatureStatType statType = CreatureStatType.HP;
        public float modifierValue = 1.0f;
    }

    public List<CreatureGeneModifier> modifiers = new List<CreatureGeneModifier>();

}
