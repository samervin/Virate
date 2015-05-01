using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirateEngine
{
    public class Country
    {
        public string Name { get; private set; }
        public int HealthyPopulation { get; set; }
        public int SickPopulation { get; set; }
        public double BirthRate { get; set; }
        public double DeathRate { get; set; }
        public int GDP { get; set; }

        private Virus virus;

        public Country(string name, int healthyPopulation, int sickPopulation, double birthRate, double deathRate, int gdp, Virus vir)
        {
            this.Name = name;
            this.HealthyPopulation = healthyPopulation;
            this.SickPopulation = sickPopulation;
            this.BirthRate = birthRate;
            this.DeathRate = deathRate;
            this.GDP = gdp;
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

            //Console.WriteLine("\nadding " + naturalBirthsAndDeaths + " natural births and deaths");
            //Console.WriteLine("adding " + newInfections + " new infections");
            //Console.WriteLine("adding " + newCleansings + " new cleansings");
            //Console.WriteLine("killing " + infectedDeaths + " infected people");

            HealthyPopulation += (int)(naturalBirthsAndDeaths - newInfections + newCleansings);
            SickPopulation += (int)(newInfections - newCleansings - infectedDeaths);

            if (HealthyPopulation < 0) HealthyPopulation = 0;
            if (SickPopulation < 0) SickPopulation = 0;
        }

        private double getNaturalBirthsAndDeaths()
        {
            double births = HealthyPopulation * BirthRate;
            double deaths = HealthyPopulation * DeathRate;
            return (births - deaths);
        }

        private double getNewInfections()
        {
            double infections = SickPopulation * .01 * virus.getInfectivityLevel();
            return infections;
        }

        private double getNewCleansings()
        {
            double cleansings = SickPopulation * .01 / virus.getSicknessLevel(); //TODO: this calculation is weird
            return cleansings;
        }

        private double getInfectedDeaths()
        {
            double deaths = SickPopulation * .000001 * virus.getSicknessLevel();
            return deaths;
        }
    }
}
