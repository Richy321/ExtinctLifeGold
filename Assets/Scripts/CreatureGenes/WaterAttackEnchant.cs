using UnityEngine;
using System.Collections;

public class WaterAttackEnchant : CreatureGene
{
    public WaterAttackEnchant()
    {
        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Ocean, CreatureGeneModifier.CreatureStatType.AttackDamage, largeAttackDamageBonus));
        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Water, CreatureGeneModifier.CreatureStatType.AttackDamage, mediumAttackDamageBonus));

        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Mountain, CreatureGeneModifier.CreatureStatType.AttackDamage, -largeAttackDamageBonus));
        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Grass, CreatureGeneModifier.CreatureStatType.AttackDamage, -mediumAttackDamageBonus));
    }

    public override GeneFlags flag
    {
        get { return GeneFlags.WaterAttackEnchant; }
    }
}
