using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenetskiAlgoritam
{
    public class City
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public City(double _latitude, double _longitude)
        {
            Latitude = _latitude;
            Longitude = _longitude;
        }

        public double CalculateDistanceBetweenCities(City _city)
        {
            double distance = Math.Sqrt(Math.Pow(Latitude - _city.Latitude, 2) + Math.Pow(Longitude - _city.Longitude, 2));
            return distance;
        }

        public static City CreateRandomCity()
        {
            City randomCity = new City(Utilities.RandomNumber.NextDouble(), Utilities.RandomNumber.NextDouble());
            return randomCity;
        }
    }
}
