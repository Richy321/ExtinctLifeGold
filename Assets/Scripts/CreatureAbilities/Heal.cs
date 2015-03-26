using UnityEngine;
using System.Collections;

public class Heal : CreatureGene.CreatureAbility
{
    const float healPercent = 0.3f;
    public Heal()
    {
        abilityType = AbilityType.Heal;
    }
    public override void Use(Creature self, Creature enemy)
    {
        self.healthPoints += Mathf.CeilToInt(self.preBattleHealthPoints * healPercent);
    }
}
