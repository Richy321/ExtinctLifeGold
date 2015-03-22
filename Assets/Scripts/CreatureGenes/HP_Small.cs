using UnityEngine;
using System.Collections;

public class HP_Small : CreatureGene
{
    public HP_Small()
    {
       modifiers.Add(new CreatureGeneModifier(Tile.TileTypes.Grass, CreatureGeneModifier.CreatureStatType.HP, 10.0f));
    }

    public override GeneFlags flag
    {
        get { return GeneFlags.HP_Small; }
    }

}
