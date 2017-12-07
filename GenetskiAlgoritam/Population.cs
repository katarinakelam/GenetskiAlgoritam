using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenetskiAlgoritam
{
    public class Population
    {
        public List<Path> Paths { get; set; }

        public double HighestFitness { get; set; }

        public Population(List<Path> _paths)
        {
            Paths = _paths;
            HighestFitness = this.CalculateHighestFitness();
        }

        public static Population CreatePopulation(Path path, int numberOfPaths)
        {
            List<Path> newPaths = new List<Path>();

            for (int i = 0; i < numberOfPaths; i++)
            {
                newPaths.Add(path.SelectEntities());
            }

            return new Population(newPaths);
        }

        public double CalculateHighestFitness()
        {
            double hFitness = this.Paths.Max(p => p.Fitness);
            return hFitness;
        }

        public Path SelectionOfEntity()
        {
            while (true)
            {
                int pathNumber = Utilities.RandomNumber.Next(0, Utilities.PopulationSize);

                if (Utilities.RandomNumber.NextDouble() < this.Paths[pathNumber].Fitness / this.HighestFitness)
                {
                    return new Path(this.Paths[pathNumber].Cities);
                }
            }
        }

        public Population CreateNextGeneration(int numberOfPaths)
        {
            List<Path> Paths = new List<Path>();

            for (int i = 0; i < numberOfPaths; i++)
            {
                Path path = this.SelectionOfEntity().DoCrossover(this.SelectionOfEntity());

                foreach (City city in path.Cities)
                {
                    path = path.DoMutation();
                }

                Paths.Add(path);
            }

            return new Population(Paths);
        }

        public Population GetEliteEntities(int numberOfEliteEntities)
        {
            List<Path> elitePath = new List<Path>();
            Population population = new Population(Paths);

            for (int i = 0; i < numberOfEliteEntities; i++)
            {
                elitePath.Add(population.FindEliteEntity());
                population = new Population(population.Paths.Except(elitePath).ToList());
            }

            return new Population(elitePath);
        }

        public Path FindEliteEntity()
        {
            foreach (Path path in this.Paths)
            {
                if (path.Fitness == this.HighestFitness)
                {
                    return path;
                }
            }
            return null;
        }

        public Population EvolvePopulation()
        {
            Population elite = this.GetEliteEntities(Utilities.EliteEntityNumber);
            Population newGeneration = this.CreateNextGeneration(Utilities.PopulationSize - Utilities.EliteEntityNumber);
            return new Population(elite.Paths.Concat(newGeneration.Paths).ToList());
        }

    }
}
