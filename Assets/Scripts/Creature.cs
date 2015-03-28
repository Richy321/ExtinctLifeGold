using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Creature : ScriptableObject 
{
    public static Dictionary<CreatureGene.GeneFlags, CreatureGene> GeneMap = new Dictionary<CreatureGene.GeneFlags, CreatureGene>();
    public static System.Random rand;
    static Creature()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Scripts/CreatureGenes");
        FileInfo[] info = dir.GetFiles("*.cs");

        foreach (FileInfo file in info)
        {
            CreatureGene gene = ScriptableObject.CreateInstance(Path.GetFileNameWithoutExtension(file.Name)) as CreatureGene;
            GeneMap.Add(gene.flag, gene);
        }
        rand = new System.Random();
    }

    private const int baseHealthPoints = 100;
    public int healthPoints = baseHealthPoints;
    public int preBattleHealthPoints = baseHealthPoints;

    private const int baseActionPoints = 100;
    public int actionPoints = baseActionPoints;

    public const int baseAttackDamage = 20;
    public int attackDamage = baseAttackDamage;

    public const int baseAttackCost = 50;
    public int attackCost = baseAttackCost;

    public const int baseArmorRating = 100;
    public int armorRating = baseArmorRating;

    public int chromosome = 0;
    public int fitnessValue = 0;

    public Dictionary<CreatureGene.CreatureAbility.AbilityType, List<CreatureGene.CreatureAbility>> abilities = new Dictionary<CreatureGene.CreatureAbility.AbilityType,List<CreatureGene.CreatureAbility>>();

    public int spriteIndex;

    public const float abilityChance = 0.5f;
    public const float tryHealPercent = 0.25f; 
    
    public void ResetToBase()
    {
        healthPoints = baseHealthPoints;
        actionPoints = baseActionPoints;
        attackDamage = baseAttackDamage;
        attackCost = baseAttackCost;
        armorRating = baseArmorRating;
        
        abilities.Clear();
        foreach (CreatureGene.CreatureAbility.AbilityType abilityType in System.Enum.GetValues(typeof(CreatureGene.CreatureAbility.AbilityType)))
        {
            abilities.Add(abilityType, new List<CreatureGene.CreatureAbility>());
        }
    }
    public void ResetFitness()
    {
        fitnessValue = 0;
    }

    public void ApplyGenesPerBattleground(BattlegroundStats bgStats)
    {
        ResetToBase();
        int hpModAccum = 0;
        int apModAccum = 0;
        int attCostModAccum = 0;
        int attDmgModAccum = 0;
        int armorRatingModAccum = 0;

        foreach (CreatureGene.GeneFlags geneFlag in System.Enum.GetValues(typeof(CreatureGene.GeneFlags)))
        {
            if (geneFlag != CreatureGene.GeneFlags.LastEntry)
            {
                if ((chromosome & (int)geneFlag) == (int)geneFlag)
                {
                    foreach (CreatureGene.CreatureGeneModifier mod in GeneMap[geneFlag].modifiers)
                    {
                        if (mod.statType == CreatureGene.CreatureGeneModifier.CreatureStatType.HP)
                        {
                            hpModAccum += Mathf.CeilToInt(baseHealthPoints * bgStats.modifierPerTileType[mod.tileType]);
                        }
                        else if (mod.statType == CreatureGene.CreatureGeneModifier.CreatureStatType.AP)
                        {
                            apModAccum += Mathf.CeilToInt(baseActionPoints * bgStats.modifierPerTileType[mod.tileType]);
                        }
                        else if (mod.statType == CreatureGene.CreatureGeneModifier.CreatureStatType.AttackCost)
                        {
                            attCostModAccum += Mathf.CeilToInt(baseAttackCost * bgStats.modifierPerTileType[mod.tileType]);
                        }
                        else if (mod.statType == CreatureGene.CreatureGeneModifier.CreatureStatType.AttackDamage)
                        {
                            attDmgModAccum += Mathf.CeilToInt(baseAttackDamage * bgStats.modifierPerTileType[mod.tileType]);
                        }
                        else if (mod.statType == CreatureGene.CreatureGeneModifier.CreatureStatType.Armor)
                        {
                            armorRatingModAccum += Mathf.CeilToInt(baseArmorRating * bgStats.modifierPerTileType[mod.tileType]);
                        }
                    }

                    foreach (CreatureGene.CreatureAbility ability in GeneMap[geneFlag].abilities)
                    {
                        abilities[ability.abilityType].Add(ability);
                    }
                }
            }
        }
        healthPoints += hpModAccum;
        actionPoints += apModAccum;
        attackCost += attCostModAccum;
        attackDamage += attDmgModAccum;
        armorRating += armorRatingModAccum;
        preBattleHealthPoints = healthPoints;
    }

    public void TryAbility(CreatureGene.CreatureAbility.AbilityType abilityType, Creature enemy)
    {
        foreach (CreatureGene.CreatureAbility ability in abilities[abilityType])
	    {
            if (ability.chargesRemaining > 0)
            {
                if (rand.NextDouble() > abilityChance)
                {
                    ability.Use(this, enemy);
                    ability.chargesRemaining--;
                    break;
                }
            }
	    }
    }

    public void BasicAttack(Creature target)
    {
        if(target != this)
            target.TakeDamage(attackDamage);
    }

    public void TakeDamage(int attackDamage)
    {
        healthPoints -= attackDamage;
    }

    public void DoTurn(Creature enemy)
    {
        if (healthPoints > 0)
        {
            if (healthPoints / preBattleHealthPoints <= tryHealPercent)
                TryAbility(CreatureGene.CreatureAbility.AbilityType.Heal, enemy);

            if (enemy.healthPoints > 0)
            {
                CreatureGene.CreatureAbility.AbilityType rndAbilityType = (CreatureGene.CreatureAbility.AbilityType)rand.Next(1, 4);
                TryAbility(rndAbilityType, enemy);

                BasicAttack(enemy);
            }
        }
    }


    public static CreatureGene GetGeneFromFlag(CreatureGene.GeneFlags flag)
    {
        return GeneMap[flag];
    }
}
