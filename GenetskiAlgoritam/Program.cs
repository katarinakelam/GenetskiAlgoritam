using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenetskiAlgoritam
{
    class Program
    {
        static void Main(string[] args)
        {
            int populationSize, cityNumber, eliteEntityNumber;
            double mutationRate, crossoverRate;

            Console.WriteLine("Enter population size:");
            Int32.TryParse(Console.ReadLine(), out populationSize);

            Console.WriteLine("Enter a number of cities to visit:");
            Int32.TryParse(Console.ReadLine(), out cityNumber);

            Console.WriteLine("Enter a number of elite entities to keep safe in each generation while evolving:");
            Int32.TryParse(Console.ReadLine(), out eliteEntityNumber);

            Console.WriteLine("Enter crossover rate:");
            Double.TryParse(Console.ReadLine(), out crossoverRate);

            Console.WriteLine("Enter mutation rate:");
            Double.TryParse(Console.ReadLine(), out mutationRate);

            Utilities.PopulationSize = populationSize;
            Utilities.CityNumber = cityNumber;
            Utilities.EliteEntityNumber = eliteEntityNumber;
            Utilities.CrossoverRate = crossoverRate;
            Utilities.MutationRate = mutationRate;
            Utilities.RandomNumber = new Random();
            SolveTSP();
        }

        public static void SolveTSP()
        {
            Path destination = Path.CreatePath(Utilities.CityNumber);
            Population population = Population.CreatePopulation(destination, Utilities.PopulationSize);

            int numberOfGeneration = 0;
            bool improved = true;

            while (numberOfGeneration < 400)
            {
                if (improved)
                {
                    ShowPopulation(population, numberOfGeneration);
                }

                improved = false;
                double oldFitness = population.HighestFitness;

                population = population.EvolvePopulation();
                if (population.HighestFitness > oldFitness)
                {
                    improved = true;
                }

                numberOfGeneration++;
            }

            Console.WriteLine("Genetic algorithm for Traveller Salesman Problem finished.");
            Console.ReadKey();
        }

        public static void ShowPopulation(Population population, int numberOfGeneration)
        {
            Path path = population.FindEliteEntity();
            Console.WriteLine("Generation {0}\n Highest fitness : \t {1}\n Shortest distance : {2} kilometres\n", numberOfGeneration, path.Fitness, path.PathDistance);
        }
    }
}
