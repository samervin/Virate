using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirateEngine.Locations
{
    public abstract class Location
    {
        public string Name { get; set; }
        public int HealthyPopulation { get; private set; }
        public int SickPopulation { get; private set; }
        public double BirthRate { get; private set; }
        public double DeathRate { get; private set; }

        private Virus virus;

        public Location(string name, int healthyPop, int sickPop, double birth, double death, Virus vir)
        {
            this.Name = name;
            this.HealthyPopulation = healthyPop;
            this.SickPopulation = sickPop;
            this.BirthRate = birth;
            this.DeathRate = death;
            this.virus = vir;
        }

        public void updatePopulation()
        {
            double naturalBirthsAndDeaths = getNaturalBirthsAndDeaths();
            double newInfections = getNewInfections();
            double newCleansings = getNewCleansings();
            double infectedDeaths = getInfectedDeaths();
            if (newInfections > HealthyPopulation) newInfections = HealthyPopulation;
            if (newCleansings > SickPopulation) newCleansings = SickPopulation;

            HealthyPopulation += (int)(naturalBirthsAndDeaths - newInfections + newCleansings);
            SickPopulation += (int)(newInfections - newCleansings - infectedDeaths);

            if (HealthyPopulation < 0) HealthyPopulation = 0;
            if (SickPopulation < 0) SickPopulation = 0;
        }

        protected double getNaturalBirthsAndDeaths()
        {
            double births = HealthyPopulation * BirthRate;
            double deaths = HealthyPopulation * DeathRate;
            return (births - deaths);
        }

        protected double getNewInfections()
        {
            double infections = SickPopulation * .01 * virus.getInfectivityLevel();
            return infections;
        }

        protected double getNewCleansings()
        {
            double cleansings = SickPopulation * .01 / virus.getSicknessLevel(); //TODO: this calculation is weird
            return cleansings;
        }

        protected double getInfectedDeaths()
        {
            double deaths = SickPopulation * .000001 * virus.getSicknessLevel();
            return deaths;
        }
    }
}
