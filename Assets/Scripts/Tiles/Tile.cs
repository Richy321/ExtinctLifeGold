using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour 
{
    public Dictionary<CreatureModifier.ModifierFlags, float> modifierToChance = new Dictionary<CreatureModifier.ModifierFlags, float>();

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

    int GenerateRandomizedBinaryRepresentation()
    {
        int binaryRespresentation = 0;
        foreach (CreatureModifier.ModifierFlags item in modifierToChance.Keys)
        {
            if(Random.value <= modifierToChance[item])
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
