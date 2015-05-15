using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirateEngine
{
    public class City : Locations.Location
    {
        public City(string name, long healthyPop, long sickPop, double birth, double death, Virus vir)
            : base(name, healthyPop, sickPop, birth, death, vir)
        {
        }
    }
}
