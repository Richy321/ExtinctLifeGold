using UnityEngine;
using System.Collections;

public class HP_Large : CreatureGene
{
    public HP_Large()
    {
        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Grass, CreatureGeneModifier.CreatureStatType.HP, largeHPBonus));
    }
    public override GeneFlags flag
    {
        get { return GeneFlags.HP_Large; }
    }
}
