using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerEF.Services
{
    public class ServiceBase
    {
        private ServiceBase()
        {

        }

        private static ServiceBase current;
        public static ServiceBase Current
        {
            get
            {
                if (current == null)
                {
                    current = new ServiceBase();
                }

                return current;
            }
        }

        private static PlanetService planet;
        public static PlanetService Planet
        {
            get
            {
                if (planet == null)
                {
                    planet = new PlanetService();
                }

                return planet;
            }
        }
    }
}
