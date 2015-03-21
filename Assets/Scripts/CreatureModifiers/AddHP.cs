using UnityEngine;
using System.Collections;

public class AddHP : CreatureModifier
{
    public override void ApplyModifier(Creature creature)
    {
        creature.healthPoints += 10;
    }
}
