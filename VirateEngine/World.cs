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
            countries.Add(new Country("United States", 312000000, 100, 0.0000356, 0.0000219, 0, virus));
            countries.Add(new Country("Canada", 35200000, 100, 0.0000301, 0.0000192, 0, virus));
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
