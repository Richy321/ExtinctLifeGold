using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battleground : ScriptableObject
{
    public List<Tile> tiles;
    public Vector2 coord;

    public Battleground()
    {
        tiles = new List<Tile>();
    }
    public void Initialise(int xcoord, int ycoord)
    {
        tiles = new List<Tile>();
        coord = new Vector2(xcoord, ycoord);
    }
}
