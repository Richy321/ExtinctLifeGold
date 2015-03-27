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
    public const string imageGOName = "creatureImage";

    public Sprite[] creatureSprites;
	// Use this for initialization
	void Start() 
    {
        terrainGenerator.Generate();
        geneticAlgorithm.Initialise();
	}

    void Awake()
    {
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
            GameObject imageGO = card.transform.FindChild(imageGOName).gameObject;
            imageGO.GetComponent<Image>().sprite = creatureSprites[creature.spriteIndex];
            //Set Chromosome text
            GameObject chromosomeLabelGO = card.transform.FindChild(chromosomeLabelGOName).gameObject;

            int maxLength = System.Enum.GetValues(typeof(CreatureGene.GeneFlags)).Length - 2; //ignoring None and LastEntry
            string chromosome = System.Convert.ToString(creature.chromosome, 2);
            chromosome = chromosome.PadLeft(maxLength, '0');
            chromosomeLabelGO.GetComponent<Text>().text = chromosome;

            //Set Fitness Value
            GameObject FitnessValueGO = card.transform.FindChild(fitnessValueGOName).gameObject;
            FitnessValueGO.GetComponent<Text>().text = creature.fitnessValue.ToString();

            OnClickShowInfo geneInfoPanel = card.GetComponent<OnClickShowInfo>();
            geneInfoPanel.creature = creature;
        }
    }
}
