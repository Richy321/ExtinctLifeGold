using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour 
{
    public Tile selectedTile;
    List<Tile> tiles = new List<Tile>();
    public Vector2 gridSize;
    List<Tile> surroundingSelectedTiles = new List<Tile>();

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void SetSelectedTile(Tile tile)
    {
        if (selectedTile)
            selectedTile.ResetColour();

        selectedTile = tile;
        selectedTile.SetColour(Color.red);

        foreach (Tile surroundingTile in surroundingSelectedTiles)
            surroundingTile.ResetColour();

        surroundingSelectedTiles.Clear();

        //set surrounding tiles

        //N
        if (selectedTile.yCoord > 0)
            surroundingSelectedTiles.Add(tiles[getGridCoord(selectedTile.xCoord, selectedTile.yCoord -1)]);
        //E
        if (selectedTile.xCoord < gridSize.x - 1)
            surroundingSelectedTiles.Add(tiles[getGridCoord(selectedTile.xCoord +1, selectedTile.yCoord)]);
        //W
        if (selectedTile.xCoord > 0)
            surroundingSelectedTiles.Add(tiles[getGridCoord(selectedTile.xCoord -1, selectedTile.yCoord)]);
        //S
        if (selectedTile.yCoord < gridSize.y - 1)
            surroundingSelectedTiles.Add(tiles[getGridCoord(selectedTile.xCoord, selectedTile.yCoord + 1)]);

        //NE
        if (selectedTile.xCoord < gridSize.x - 1 && selectedTile.yCoord >0)
            surroundingSelectedTiles.Add(tiles[getGridCoord(selectedTile.xCoord + 1, selectedTile.yCoord -1)]);

        //NW 
        if (selectedTile.xCoord >0 && selectedTile.yCoord > 0)
            surroundingSelectedTiles.Add(tiles[getGridCoord(selectedTile.xCoord - 1, selectedTile.yCoord-1)]);

        //SE
        if (selectedTile.yCoord < gridSize.y - 1 && selectedTile.xCoord < gridSize.x - 1)
            surroundingSelectedTiles.Add(tiles[getGridCoord(selectedTile.xCoord +1, selectedTile.yCoord + 1)]);

        //SW
        if (selectedTile.yCoord < gridSize.y - 1 && selectedTile.xCoord > 0)
            surroundingSelectedTiles.Add(tiles[getGridCoord(selectedTile.xCoord  -1, selectedTile.yCoord + 1)]);

        foreach (Tile surroundingTile in surroundingSelectedTiles)
        {
            surroundingTile.SetColour(Color.magenta);
        }
    }

    public void AddTile(Tile tile)
    {
        tiles.Add(tile);
    }

    int getGridCoord(int x, int y)
    {
        return (int)(x * gridSize.y + y);
    }

}
