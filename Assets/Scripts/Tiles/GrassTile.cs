using UnityEngine;
using System.Collections;

public class GrassTile : Tile
{
	// Use this for initialization
    protected override void Start() 
    {
        base.Start();
        this.geneToChance.Add(CreatureGene.GeneFlags.HP_Small, 1.0f);
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
