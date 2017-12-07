using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenetskiAlgoritam
{
    public class Path
    {
        public List<City> Cities { get; set; }

        public double PathDistance { get; set; }

        public double Fitness { get; set; }

        public Path(List<City> _cities)
        {
            Cities = _cities;
            PathDistance = this.CalculatePathDistance();
            Fitness = this.CalculateEntityFitness();
        }

        public static Path CreatePath(int numberOfCities)
        {
            List<City> cities = new List<City>();

            for (int i = 0; i < numberOfCities; i++)
            {
                cities.Add(City.CreateRandomCity());
            }

            return new Path(cities);
        }

        public Path SelectEntities()
        {
            List<City> cities = new List<City>(this.Cities);
            int numberOfCities = cities.Count;

            while (true)
            {
                if (numberOfCities == 1)
                    break;

                int shufflePartner = Utilities.RandomNumber.Next(numberOfCities);
                numberOfCities--;

                City temporateCity = cities[shufflePartner];
                cities[shufflePartner] = cities[numberOfCities];
                cities[numberOfCities] = temporateCity;
            }
            return new Path(cities);
        }

        public Path DoCrossover(Path _path)
        {
            if (Utilities.RandomNumber.NextDouble() < Utilities.CrossoverRate)
            {
                int delimiterA = Utilities.RandomNumber.Next(0, _path.Cities.Count);
                int delimiterB = Utilities.RandomNumber.Next(delimiterA, _path.Cities.Count);

                List<City> subListParentA = this.Cities.GetRange(delimiterA, delimiterB - delimiterA + 1);
                List<City> subListParentB = _path.Cities.Except(subListParentA).ToList();

                List<City> child = subListParentB.Take(delimiterA).Concat(subListParentA).Concat(subListParentB.Skip(delimiterA)).ToList();

                return new Path(child);
            }
            else return _path;
        }

        public Path DoMutation()
        {
            List<City> cities = new List<City>(this.Cities);

            if (Utilities.RandomNumber.NextDouble() < Utilities.MutationRate)
            {
                int shufflePartnerA = Utilities.RandomNumber.Next(0, this.Cities.Count);
                int shufflePartnerB = Utilities.RandomNumber.Next(0, this.Cities.Count);

                City tempCity = cities[shufflePartnerA];
                cities[shufflePartnerA] = cities[shufflePartnerB];
                cities[shufflePartnerB] = tempCity;
            }

            return new Path(cities);
        }

        public double CalculatePathDistance()
        {
            double trajectory = 0;
            for (int i = 0; i < this.Cities.Count; i++)
            {
                trajectory += this.Cities[i].CalculateDistanceBetweenCities(this.Cities[(i + 1) % this.Cities.Count]);
            }

            return trajectory;
        }

        public double CalculateEntityFitness()
        {
            double fitness;
            fitness = 1 / this.PathDistance;

            return fitness;
        }
    }
}
