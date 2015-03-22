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
        int xCoord = 0;
        int yCoord = 0;
        Battleground bg = new Battleground(xCoord, yCoord);

        for(int x =0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (y % gridSize.y == 0)
                {
                    yCoord++;
                    battlegrounds.Add(bg);
                    bg = new Battleground(xCoord, yCoord);
                }
                bg.tiles.Add(tiles[getGridCoord(x, y)]);
            }

            if (x % gridSize.x == 0)
            {
                xCoord++;
                battlegrounds.Add(bg);
                bg = new Battleground(xCoord, yCoord);
            }
            yCoord = 0;
        }
    }
}
