using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
public class CreatureGenerator : MonoBehaviour 
{

    public Dictionary<CreatureGene.GeneFlags, CreatureGene> GeneMap = new Dictionary<CreatureGene.GeneFlags, CreatureGene>();

    // Use this for initialization
	void Start () 
    {
	    DirectoryInfo dir = new DirectoryInfo("Assets/Scripts/CreatureGenes");
        FileInfo[] info = dir.GetFiles("*.cs");

        foreach(FileInfo file in info)
        {
            CreatureGene gene = ScriptableObject.CreateInstance(Path.GetFileNameWithoutExtension(file.Name)) as CreatureGene;
            GeneMap.Add(gene.flag, gene);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public Creature GenerateFromTiles(List<Tile> tiles)
    {
        GameObject terrainGO = GameObject.Find(TerrainGenerator.terrainGridGOName);
        int creatureFlags =0;

        //TODO - possibly add up matching genes and over a certain amount activate a better gene of the same type
        if (terrainGO)
        {
            TerrainManager terrainManager = terrainGO.GetComponent<TerrainManager>();
            if (terrainManager)
            {
                creatureFlags = terrainManager.selectedTile.GenerateRandomizedBinaryRepresentation();

                foreach (Tile surroundingTile in terrainManager.surroundingSelectedTiles)
                {
                    creatureFlags |= surroundingTile.GenerateRandomizedBinaryRepresentation();
                }
            }
        }

        return GenerateFromChromosome(creatureFlags);
    }


    public Creature GenerateFromChromosome(int chromosome)
    {
        Creature creature = ScriptableObject.CreateInstance<Creature>();
        creature.chromosome = chromosome;
        foreach(CreatureGene.GeneFlags flag in Enum.GetValues(typeof(CreatureGene.GeneFlags)))
        {
            if ((chromosome & (int)flag) == 1)
            {
                GeneMap[flag].ApplyGene(creature);
            }
        }
        return creature;
    }
}
