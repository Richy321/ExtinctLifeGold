using UnityEngine;
using System.Collections;

public class GrassTile : Tile
{
	// Use this for initialization
    protected override void Start() 
    {
        base.Start();
        this.modifierToChance.Add(CreatureModifier.ModifierFlags.AddHP, 0.5f);
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
