using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirateEngine
{
    public class Country : Locations.Location
    {
        public new int HealthyPopulation {
            get
            {
                int citySum = cities.Sum(c => c.HealthyPopulation);
                return citySum + HealthyRuralPopulation;
            }
        }
        public new int SickPopulation {
            get
            {
                int citySum = cities.Sum(c => c.SickPopulation);
                return citySum + SickRuralPopulation;
            }
        }

        public int HealthyRuralPopulation { get; set; }
        public int SickRuralPopulation { get; set; }
        public int GDP { get; set; }

        private Virus virus;
        private List<City> cities;

        public Country(string name, int healthyRuralPopulation, int sickRuralPopulation, double birth, double death, int gdp, List<City> citylist, Virus vir)
            : base(name, healthyRuralPopulation, sickRuralPopulation, birth, death, vir)
        {
            this.HealthyRuralPopulation = healthyRuralPopulation;
            this.SickRuralPopulation = sickRuralPopulation;
            this.GDP = gdp;
            this.cities = citylist;
            this.virus = vir;
        }

        public new void updatePopulation()
        {
            base.updatePopulation();
            foreach(City c in cities)
            {
                c.updatePopulation();
            }
        }
    }
}
