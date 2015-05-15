using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirateEngine
{
    public class Country : Locations.Location
    {
        public new long HealthyPopulation {
            get
            {
                long citySum = cities.Sum(c => c.HealthyPopulation);
                return citySum + HealthyRuralPopulation;
            }
        }
        public new long SickPopulation {
            get
            {
                long citySum = cities.Sum(c => c.SickPopulation);
                return citySum + SickRuralPopulation;
            }
        }

        public long HealthyRuralPopulation { get; set; }
        public long SickRuralPopulation { get; set; }
        public int GDP { get; set; }

        private Virus virus;
        private List<City> cities;

        public Country(string name, long healthyRuralPopulation, long sickRuralPopulation, double birth, double death, int gdp, List<City> citylist, Virus vir)
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
