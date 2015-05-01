using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirateEngine
{
    public class Virus
    {
        public string Name { get; private set; }
        public string DNA { get; private set; }

        private Random rand;

        public Virus(string name)
        {
            this.rand = new Random();
            this.Name = name;
            this.DNA = generateDNA(20);
        }

        public bool canEvolve(int currentDay)
        {
            if (currentDay % this.getEvolutionRate() == 0)
                return true;

            return false;
        }

        public void updateDNAAtIndex(int index)
        {
            updateDNAAtIndex(index, generateDNABase());
        }

        public void updateDNAAtIndex(int index, char newBase)
        {
            StringBuilder temp = new StringBuilder(DNA);
            temp[index] = newBase;
            DNA = temp.ToString();
        }

        public void updateDNAWithBase(char newBase)
        {
            int index = rand.Next(0, DNA.Length);
            updateDNAAtIndex(index, newBase);
        }

        private string generateDNA(int length)
        {
            string dna = "";
            for (int i = 0; i < length; i++)
            {
                dna += generateDNABase();
            }
            return dna;
        }

        private char generateDNABase()
        {
            switch (rand.Next(0, 4))
            {
                case 0: return 'A';
                case 1: return 'T';
                case 2: return 'G';
                case 3: return 'C';
                default: return '?';
            }
        }

        // Sickness level -- higher is better
        public double getSicknessLevel()
        {
            return DNA.Count(f => f == 'A');
        }

        // Infectivity level -- higher is better
        public double getInfectivityLevel()
        {
            return DNA.Count(f => f == 'T');
        }

        // Evolution rate -- higher G means lower number of days to evolution
        public int getEvolutionRate()
        {
            return DNA.Length - DNA.Count(f => f == 'G');
        }

        // Treatment resistance -- higher is better
        public double getTreatmentResistance()
        {
            return DNA.Count(f => f == 'C');
        }

    }
}
