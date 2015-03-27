using UnityEngine;
using System.Collections;

public class HP_Medium : CreatureGene
{
    public HP_Medium()
    {
        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Grass, CreatureGeneModifier.CreatureStatType.HP, mediumHPBonus));
    }

    public override GeneFlags flag
    {
        get { return GeneFlags.HP_Medium; }
    }
}
