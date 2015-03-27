using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattlegroundStats //: ScriptableObject 
{
    public Dictionary<Tile.TileTypes, float> modifierPerTileType = new Dictionary<Tile.TileTypes, float>();
    public BattlegroundStats()
    {
        ResetModifiers();
    }

    private void ResetModifiers()
    {
        modifierPerTileType.Clear();
        foreach (Tile.TileTypes tileType in System.Enum.GetValues(typeof(Tile.TileTypes)))
        {
            modifierPerTileType.Add(tileType, 1.0f);
        }
    }

    public void GenerateBattlegroundStats(Battleground battleground)
    {
        if (battleground.tiles.Count > 0)
        {
            ResetModifiers();
            Dictionary<Tile.TileTypes, int> tileCounts = new Dictionary<Tile.TileTypes, int>();

            foreach (Tile.TileTypes tileType in System.Enum.GetValues(typeof(Tile.TileTypes)))
            {
                tileCounts.Add(tileType, 0);
            }

            foreach (Tile tile in battleground.tiles)
            {
                tileCounts[tile.tileType]++;
            }

            foreach (Tile.TileTypes tileType in System.Enum.GetValues(typeof(Tile.TileTypes)))
            {
                modifierPerTileType[tileType] = (float)tileCounts[tileType] / battleground.tiles.Count;
            }
        }
    }
}
