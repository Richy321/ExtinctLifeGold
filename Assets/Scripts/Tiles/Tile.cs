using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Tile : MonoBehaviour 
{
    public enum TileTypes
    {
        Ocean,
        Water,
        Sand,
        Grass,
        Plain,
        Mountain
    };

    public virtual TileTypes tileType 
    {
        get { return TileTypes.Grass; }
    }
    protected Dictionary<CreatureGene.GeneFlags, float> geneToChance = new Dictionary<CreatureGene.GeneFlags, float>();

    public int xCoord;
    public int yCoord;


    protected Color origColour;
    protected virtual void Start()
    {
        origColour = GetComponent<SpriteRenderer>().color;
    }

    protected virtual void Update()
    {

    }

    public virtual int GenerateRandomizedBinaryRepresentation()
    {
        int binaryRespresentation = 0;
        foreach (CreatureGene.GeneFlags item in geneToChance.Keys)
        {
            if(UnityEngine.Random.value <= geneToChance[item])
                binaryRespresentation |= (int)item;
        }
        return binaryRespresentation;
    }

    void OnMouseDown()
    {
        transform.parent.GetComponent<TerrainManager>().SetSelectedTile(this);
    }

    public void SetColour(Color colour)
    {
        GetComponent<SpriteRenderer>().color = colour;
    }

    public void ResetColour()
    {
        GetComponent<SpriteRenderer>().color = origColour;
    }
}
