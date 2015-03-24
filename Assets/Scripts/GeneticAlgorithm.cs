using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneticAlgorithm : MonoBehaviour
{
    public static int populationSize = 10;
    public float crossoverRate = 0.7f;
    public float mutationRate = 0.001f;
    public bool elitism = false;

    public int generation = 0;
    public int battles = 0;
    public bool requiresUIUpdate = false;
    public int totalFitness = 0;

    public List<Creature> population = new List<Creature>();
    public TerrainManager terrainManager;

    /// <summary>
    ///Roulette-wheel selection - normalised decending, random value (0-1), first to accum to that value
    ///Stocastic Universal sampling - multiple equally spaced out pointers
    ///Tournament selection - best individual of a randomly chosen subset
    ///Truncation selection - best % of of the population is kept.
    /// </summary>
    public enum SelectionAlgorithm
    {
        RouletteWheel,
        StocasticUniversalSampling,
        TournamentSelection,
        TruncationSelection
    }

    SelectionAlgorithm selectionAlgorithm = SelectionAlgorithm.RouletteWheel;


    // Use this for initialization
    void Start()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
    }

    void Awake()
    {
       // Initialise();
    }
    public void Initialise()
    {
        //find terrain manager
        if (!terrainManager)
        {
            GameObject terrainManagerGO = GameObject.Find(TerrainGenerator.terrainGridGOName);
            terrainManager = terrainManagerGO.GetComponent<TerrainManager>();
        }
        InitialisePopulation();
        CalculatePopulationFitness();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitialisePopulation()
    {
        population.Clear();
        AddRandomPopulation(populationSize);
        generation = 1;
        requiresUIUpdate = true;
    }

    private void AddRandomPopulation(int count)
    {
        for (int i = 0; i < count; i++)
        {
            population.Add(CreateRandomCreature());
        }
    }
    private int createRandomChromosome()
    {
        return Random.Range(0, (int)CreatureGene.GeneFlags.LastEntry);
    }

    private Creature CreateRandomCreature()
    {
        Creature creature = ScriptableObject.CreateInstance<Creature>();
            creature.chromosome = createRandomChromosome();
        
        return creature;
    }

    void ResetFitnessValue()
    {
        foreach (Creature creature in population)
            creature.ResetFitness();
    }

    void CalculatePopulationFitness()
    {
        ResetFitnessValue();
        battles = 0;

        foreach (Battleground battleground in terrainManager.battlegrounds)
        {
            foreach (Creature creatureA in population)
            {
                foreach (Creature creatureB in population)
                {
                    if (creatureA != creatureB)
                    {
                        BattleStats stats = BattleSimulation.Battle(creatureA, creatureB, battleground);
                        stats.winner.fitnessValue++;
                        battles++;
                        requiresUIUpdate = true;
                    }
                }
            }
        }

        //Sort highest fitness value to lowest
        population.Sort((creatureA, creatureB)=> creatureB.fitnessValue.CompareTo(creatureA.fitnessValue));

        totalFitness =  battles;
    }

    /// <summary>
    /// Retain the top 'keepPercentage' percent. Duplicate until full.
    /// </summary>
    /// <param name="keepPercentage"></param>
    void TruncationSelection(float keepPercentage)
    {
        List<Creature> newPopulation = new List<Creature>();
        int max = Mathf.CeilToInt(population.Count * keepPercentage);

        int currentPos = 0;
        while(newPopulation.Count < populationSize)
        {
            Creature clone = Object.Instantiate<Creature>(population[currentPos]);
            newPopulation.Add(clone);

            if(currentPos < max)
                currentPos++;
            else
                currentPos =0;
        }
        population = newPopulation;
    }

    void RouletteSelection(int totalFitness)
    {
        List<Creature> newPopulation = new List<Creature>();
        while (newPopulation.Count < populationSize)
        {
            Creature offspringA = RouletteSelectionSingle(totalFitness);
            Creature offspringB = RouletteSelectionSingle(totalFitness);

            Crossover(offspringA, offspringB);

            Mutation(offspringA);
            Mutation(offspringB);

            newPopulation.Add(offspringA);
            newPopulation.Add(offspringB);
        }
        population = newPopulation;
    }

    Creature RouletteSelectionSingle(int totalFitness)
    {
        //Generate random number between 0-1
        float randNumber = Random.value;

        float runningNormalisedFitness = 0;

        //TODO - switch to binary search instead of linear...
        for (int i = 0; i < populationSize; i++)
        {
            runningNormalisedFitness += (float)population[i].fitnessValue / totalFitness;

            if (runningNormalisedFitness > randNumber)
            {
                //return a clone
                return Object.Instantiate<Creature>(population[Mathf.Max(0, i)]);
            }
        }

        //we have a problem if we get here.
        throw new UnityException("RouletteSelectionSingle didn't find a value... normalisation must be wrong");
    }

    /// <summary>
    /// Crossover some individuals to hopefully create a fitter offspring
    /// </summary>
    /// <param name="creatureA"></param>
    /// <param name="creatureB"></param>
    void Crossover(Creature creatureA, Creature creatureB)
    {
        if (Random.value <= crossoverRate)
        {
            string chromosomeA = System.Convert.ToString(creatureA.chromosome, 2);
            string chromosomeB = System.Convert.ToString(creatureB.chromosome, 2);

            int maxLength = System.Enum.GetValues(typeof(CreatureGene.GeneFlags)).Length-2; //ignoring None and LastEntry

            chromosomeA = chromosomeA.PadLeft(maxLength, '0');
            chromosomeB = chromosomeB.PadLeft(maxLength, '0');

            //select random length along chromosome
            int splitPoint = Random.Range(0, maxLength);

            string leftA = chromosomeA.Substring(0, splitPoint);
            string rightA = chromosomeA.Substring(splitPoint);

            string leftB = chromosomeB.Substring(0, splitPoint);
            string rightB = chromosomeB.Substring(splitPoint);

            //swap after that point
            string resultA = leftA + rightB;
            string resultB = leftB + rightA;

            creatureA.chromosome = System.Convert.ToInt32(resultA, 2);
            creatureB.chromosome = System.Convert.ToInt32(resultB, 2);
        }
    }

    /// <summary>
    /// Randomly flip a bit of some of the population.
    /// </summary>
    /// <param name="creature"></param>
    void Mutation(Creature creature)
    {
        //string chromosome = System.Convert.ToString(creature.chromosome, 2);
        foreach (CreatureGene.GeneFlags flag in System.Enum.GetValues(typeof(CreatureGene.GeneFlags)))
        {
            if (Random.value <= mutationRate)
                creature.chromosome |= (int)flag;
        }
    }

    /// <summary>
    /// Assumes fitness has been calculated and population is ordered by fitness (highest to lowest)
    /// </summary>
    public void EvolvePopulation()
    {
        if (elitism)
        {
            //TODO - keep fittest individual
        }

        switch (selectionAlgorithm)
        {
            case SelectionAlgorithm.RouletteWheel:
                RouletteSelection(totalFitness);
                break;
            case SelectionAlgorithm.TruncationSelection:
                TruncationSelection(0.5f);
                break;
        }
        CalculatePopulationFitness();
        generation++;
        requiresUIUpdate = true;
    }

    public Creature GetCreatureByFitnessIndex(int fitnessIndex)
    {
        return population[fitnessIndex];
    }
}
