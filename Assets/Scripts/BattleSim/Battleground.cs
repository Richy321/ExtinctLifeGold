using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battleground : ScriptableObject
{
    public List<Tile> tiles = new List<Tile>();
    public Vector2 coord;
    public Battleground(int xcoord, int ycoord)
    {
        coord = new Vector2(xcoord, ycoord);
    }
}
