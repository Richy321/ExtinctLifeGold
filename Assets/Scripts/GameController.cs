using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour 
{
    public CreatureGenerator creatureGenerator;
    public TerrainManager terrainManager;

    public Creature playerCreature;

	// Use this for initialization
	void Start () 
    {
        if(!creatureGenerator)
            creatureGenerator = gameObject.GetComponent<CreatureGenerator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}


    public void GeneratePlayerCreature()
    {
        if(!terrainManager)
            terrainManager = GameObject.Find(TerrainGenerator.terrainGridGOName).GetComponent<TerrainManager>();

        if (terrainManager)
        {
            List<Tile> selectedTiles = new List<Tile>();
            selectedTiles.Add(terrainManager.selectedTile);
            selectedTiles.AddRange(terrainManager.surroundingSelectedTiles);

            playerCreature = creatureGenerator.GenerateFromTiles(selectedTiles);

            int ap = playerCreature.actionPoints;
        }
    }
}
