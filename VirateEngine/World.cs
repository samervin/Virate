using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirateEngine
{
    public class World
    {
        public int Day { get; private set; }
        public List<Country> countries;

        private Virus virus;

        public World(Virus vir)
        {
            Day = 1;
            virus = vir;
            countries = new List<Country>();
            //countries.Add(new Country(name, population, sick population, birth rate, death rate, gdp, virus))
            List<City> usCities = new List<City>();
            usCities.Add(new City("New York", 8175000, 100, 0.0000356, 0.0000219, vir));
            usCities.Add(new City("Los Angeles", 3792000, 100, 0.0000356, 0.0000219, vir));

            countries.Add(new Country("United States", 312000000, 100, 0.0000356, 0.0000219, 0, usCities, virus));
        }

        public void advanceDay()
        {
            Day++;
            foreach (Country c in countries)
            {
                c.updatePopulation();
            }
        }

    }
}
