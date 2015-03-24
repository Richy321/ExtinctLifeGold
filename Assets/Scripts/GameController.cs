using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    public TerrainManager terrainManager;
    public TerrainGenerator terrainGenerator;

    public Creature playerCreature;
    public GeneticAlgorithm geneticAlgorithm;

    public GameObject fitnessPanel;
    public Text generationText;
    public Text battleText;

    public const string creatureCardGOPrefix = "CreatureCard";
    public const string chromosomeLabelGOName = "txtChromosome";
    public const string fitnessLabelGOName = "txtFitnessLabel";
    public const string fitnessValueGOName = "txtFitnessValue";

	// Use this for initialization
	void Start () 
    {
        terrainGenerator.Generate();
        geneticAlgorithm.Initialise();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (geneticAlgorithm.requiresUIUpdate)
        {
            UpdateUI();
            geneticAlgorithm.requiresUIUpdate = false;
        }
	}

    public void UpdateUI()
    {
        generationText.text = "Generation: " + geneticAlgorithm.generation.ToString();
        battleText.text = "Battles: " + geneticAlgorithm.battles.ToString();

        for (int i = 0; i < 5; i++)
        {
            UpdateCreatureCard(i);
        }
    }


    void UpdateCreatureCard(int index)
    {
        GameObject card = GameObject.Find(creatureCardGOPrefix + index.ToString());
        if (card)
        {
            Creature creature = geneticAlgorithm.GetCreatureByFitnessIndex(index);

            //Set Creature Image

            //Set Chromosome text
            GameObject chromosomeLabelGO = card.transform.FindChild(chromosomeLabelGOName).gameObject;
            chromosomeLabelGO.GetComponent<Text>().text = System.Convert.ToString(creature.chromosome, 2);

            //Set Fitness Value
            GameObject FitnessValueGO = card.transform.FindChild(fitnessValueGOName).gameObject;
            FitnessValueGO.GetComponent<Text>().text = creature.fitnessValue.ToString();
        }
    }
}
