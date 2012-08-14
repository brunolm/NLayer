using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerEF.Domain;
using NLayerEF.Infrastructure;

namespace NLayerEF.Services
{
    public class PlanetService
    {
        internal PlanetService()
        {

        }

        public bool Add(Planet planet)
        {
            EntityFactory.Current.Planets.Add(planet);

            return EntityFactory.Current.SaveChanges() > 0;
        }

        public IQueryable<Planet> GetList()
        {
            return EntityFactory.Current.Planets;
        }

        public void RemoveAll()
        {
            var planets = EntityFactory.Current.Planets.ToList();

            foreach (var planet in planets)
            {
                EntityFactory.Current.Planets.Remove(planet);
            }

            EntityFactory.Current.SaveChanges();
        }
    }
}
