using UnityEngine;
using System.Collections;

public class HP_Medium : CreatureGene
{
    public override GeneFlags flag
    {
        get { return GeneFlags.HP_Medium; }
    }

    public override void ApplyGene(Creature creature)
    {
        creature.healthPoints += 25;
    }
}
