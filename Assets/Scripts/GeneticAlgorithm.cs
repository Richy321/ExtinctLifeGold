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
            Creature creature = ScriptableObject.CreateInstance<Creature>();
            creature.chromosome = Random.Range(0, (int)CreatureGene.GeneFlags.LastEntry);
            population.Add(creature);
        }
    }

    void CalculatePopulationFitness()
    {
        //foreach battleground tile
            //foreach creature
                //foreach creature
                    //BattleSimulation.Battle()
                    //accumulate wins
            

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
        List<Creature> newPopulation = new List<Creature>();

        if (elitism)
        {
            //keep fittest individual
        }
    }
}
