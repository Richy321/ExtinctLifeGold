using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour 
{
    public Dictionary<CreatureModifier.ModifierFlags, float> modifierToChance = new Dictionary<CreatureModifier.ModifierFlags, float>();

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
}
