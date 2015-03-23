using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour 
{
    public Tile selectedTile;
    List<Tile> tiles = new List<Tile>();
    public Vector2 gridSize;
    public List<Tile> surroundingSelectedTiles = new List<Tile>();

    public List<Battleground> battlegrounds = new List<Battleground>();
    public Vector2 battlegroundSize = new Vector2(20, 20);

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

    public void CalculateBattlegrounds()
    {
        for (int xCoord = 0; xCoord < gridSize.x / battlegroundSize.x; xCoord++)
        {
            for (int yCoord = 0; yCoord < gridSize.y / battlegroundSize.y; yCoord++)
            {
                Battleground bg = ScriptableObject.CreateInstance<Battleground>();
                bg.coord = new Vector2(xCoord, yCoord);
                for (int x1 = 0; x1 < battlegroundSize.x; x1++)
                {
                    for (int y1 = 0; y1 < battlegroundSize.y; y1++)
                    {
                        bg.tiles.Add(tiles[getGridCoord((int)battlegroundSize.x * xCoord + x1, (int)battlegroundSize.y * yCoord + y1)]);
                    }
                }
                battlegrounds.Add(bg);
            }
        }
    }
}
