using UnityEngine;
using System.Collections;

public class PlainTile : Tile 
{

    public override TileTypes tileType
    {
        get { return TileTypes.Plain; }
    }

	// Use this for initialization
	protected override void Start () 
    {
        base.Start();	
	}
	
	// Update is called once per frame
	protected override void Update() 
    {
        base.Update();
	}
}
