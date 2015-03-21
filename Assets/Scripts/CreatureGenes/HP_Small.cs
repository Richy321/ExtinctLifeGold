using UnityEngine;
using System.Collections;

public class HP_Small : CreatureGene
{
    public override GeneFlags flag
    {
        get { return GeneFlags.HP_Small; }
    }

    public override void ApplyGene(Creature creature)
    {
        creature.healthPoints += 10;
    }
}
