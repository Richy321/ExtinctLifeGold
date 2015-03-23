using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneticAlgorithm : MonoBehaviour
{
    public static int populationSize = 64;
    public float crossoverRate = 0.7f;
    public float mutationRate = 0.001f;
    public bool elitism = false;

    List<Creature> population = new List<Creature>();
    TerrainManager terrainManager;

    // Use this for initialization
    void Start()
    {
        InitialisePopulation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitialisePopulation()
    {
        population.Clear();
        AddRandomPopulation(populationSize);
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
            creature.fitnessValue = 0;
    }

    void CalculatePopulationFitness()
    {
        ResetFitnessValue();

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
                    }
                }
            }
        }
    }

    /// <summary>
    /// Discard bad designs
    /// </summary>
    void Selection()
    {
        //Roulette-wheel selection - normalised decending, random value (0-1), first to accum to that value
        //Stocastic Universal sampling - multiple equally spaced out pointers
        //tournament selection - best individual of a randomly chosen subset
        //Truncation selection - best % of of the population is kept.
        TruncationSelection(0.5f);
    }

    void TruncationSelection(float keepPercentage)
    {
        List<Creature> newPopulation = new List<Creature>();
        population.Sort((creatureA, creatureB) => creatureA.fitnessValue.CompareTo(creatureB.fitnessValue));

        int max = Mathf.CeilToInt(population.Count * keepPercentage);

        for (int i = 0; i < populationSize; i++)
        {
            if(i < max)
                newPopulation.Add(population[i]);
            else
                newPopulation.Add(CreateRandomCreature());
        }
        population = newPopulation;
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

            //select random length along chromosome
            int splitPoint = Random.Range(0, System.Enum.GetValues(typeof(CreatureGene.GeneFlags)).Length);

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
        string chromosome = System.Convert.ToString(creature.chromosome, 2);
        foreach (CreatureGene.GeneFlags flag in System.Enum.GetValues(typeof(CreatureGene.GeneFlags)))
        {
            if (Random.value <= mutationRate)
                creature.chromosome |= (int)flag;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void EvolvePopulation()
    {

        //find terrrain manager
        GameObject terrainManagerGO = GameObject.Find(TerrainGenerator.terrainGridGOName);
        terrainManager = terrainManagerGO.GetComponent<TerrainManager>();

        List<Creature> newPopulation = new List<Creature>();

        if (elitism)
        {
            //keep fittest individual
        }
    }
}
