using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Creature : ScriptableObject 
{
    public static Dictionary<CreatureGene.GeneFlags, CreatureGene> GeneMap = new Dictionary<CreatureGene.GeneFlags, CreatureGene>();

    static Creature()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Scripts/CreatureGenes");
        FileInfo[] info = dir.GetFiles("*.cs");

        foreach (FileInfo file in info)
        {
            CreatureGene gene = ScriptableObject.CreateInstance(Path.GetFileNameWithoutExtension(file.Name)) as CreatureGene;
            GeneMap.Add(gene.flag, gene);
        }
    }

    private const int baseHealthPoints = 100;
    public int healthPoints = 100;

    private const int baseActionPoints = 100;
    public int actionPoints = 100;

    public const int baseAttackDamage = 20;
    public int attackDamage = 20;

    public const int baseAttackCost = 50;
    public int attackCost = 50;

    public int chromosome = 0;
    public int fitnessValue = 0;

    public void ResetToBase()
    {
        healthPoints = baseHealthPoints;
        actionPoints = baseActionPoints;
        attackDamage = baseAttackDamage;
        attackCost = baseAttackCost;

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

        foreach (CreatureGene.GeneFlags geneFlag in System.Enum.GetValues(typeof(CreatureGene.GeneFlags)))
        {
            if ((chromosome & (int)geneFlag) == 1)
            {
                foreach (CreatureGene.CreatureGeneModifier mod in GeneMap[geneFlag].modifiers)
                {
                    if(mod.statType == CreatureGene.CreatureGeneModifier.CreatureStatType.HP)
                    {
                        hpModAccum += Mathf.CeilToInt(baseHealthPoints * bgStats.modifierPerTileType[mod.tileType]); 
                    }

                    else if (mod.statType == CreatureGene.CreatureGeneModifier.CreatureStatType.AP)
                    {
                        apModAccum += Mathf.CeilToInt(baseActionPoints * bgStats.modifierPerTileType[mod.tileType]);
                    } 
                }
            }
        }
        healthPoints += hpModAccum;
        actionPoints += apModAccum;
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
}
