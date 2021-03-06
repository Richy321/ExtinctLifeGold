﻿using UnityEngine;
using System.Collections;

public class SandTile : Tile 
{
    public override TileTypes tileType
    {
        get { return TileTypes.Sand; }
    }

	// Use this for initialization
	protected override void Start () 
    {
        base.Start();
        this.geneToChance.Add(CreatureGene.GeneFlags.HP_Medium, 1.0f);
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
