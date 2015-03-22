using UnityEngine;
using System.Collections;

public class HP_Medium : CreatureGene
{
    public HP_Medium()
    {
        modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Grass, CreatureGeneModifier.CreatureStatType.HP, 20.0f));
    }

    public override GeneFlags flag
    {
        get { return GeneFlags.HP_Medium; }
    }
}
