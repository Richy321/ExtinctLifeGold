using UnityEngine;
using System.Collections;

public class LandHP : CreatureGene
{
    public LandHP()
    {
        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Ocean, CreatureGeneModifier.CreatureStatType.HP, -largeHPBonus));
        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Water, CreatureGeneModifier.CreatureStatType.HP, -mediumHPBonus));

        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Mountain, CreatureGeneModifier.CreatureStatType.HP, largeHPBonus));
        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Grass, CreatureGeneModifier.CreatureStatType.HP, mediumHPBonus));
    }

    public override GeneFlags flag
    {
        get { return GeneFlags.LandHP; }
    }
}

