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
        public long HealthyPopulation { get; private set; }
        public long SickPopulation { get; private set; }
        public double BirthRate { get; private set; }
        public double DeathRate { get; private set; }

        private Virus virus;
        private Random rand;

        public Location(string name, long healthyPop, long sickPop, double birth, double death, Virus vir)
        {
            this.Name = name;
            this.HealthyPopulation = healthyPop;
            this.SickPopulation = sickPop;
            this.BirthRate = birth;
            this.DeathRate = death;
            this.virus = vir;
            this.rand = new Random();
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
            double cleansings = (5 - virus.getTreatmentResistance()) * .01 * SickPopulation;
            return cleansings;
        }

        protected double getInfectedDeaths()
        {
            // Keeping this efficient: we want to roll the dice, but not do a silly amount of calculations
            // This splits the population into (roughly) 10 parts and rolls for each part

            double deaths = 0;
            for (int i = 0; i < 10; i++)
            {
                if (rand.Next(0, 1000) <= virus.getSicknessLevel())
                {
                    deaths += (SickPopulation / 10);
                }
            }
            return deaths;
        }
    }
}
