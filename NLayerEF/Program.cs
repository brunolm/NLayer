using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLayerEF.Domain;
using NLayerEF.Services;

namespace NLayerEF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Clear table for the test
            ServiceBase.Planet.RemoveAll();

            // How many concurrent accesses?
            int testSize = 10;
            Thread[] threads = new Thread[testSize];

            for (int i = 0; i < testSize; ++i)
            {
                threads[i] = new Thread(() => { Test(); });
                threads[i].Start();
            }
            for (int i = 0; i < testSize; ++i)
            {
                threads[i].Join();
            }


            Console.WriteLine("List of planets with 'a':");

            // returning IQueryable from the service allow further filtering before going to db
            // good? bad? It could break a business rule
            var planets = ServiceBase.Planet.GetList()
                .Where(p => p.Name.Contains("a"))
                .GroupBy(p => p.Name)
                .ToList();

            foreach (var planet in planets)
            {
                Console.WriteLine("{0}", planet.Key);
            }

            Console.ReadKey();
        }

        static void Test()
        {
            // the idea here is to have a single instance of a service
            ServiceBase.Planet.Add(new Planet { Name = "Earth" });
            ServiceBase.Planet.Add(new Planet { Name = "Mars" });
            ServiceBase.Planet.Add(new Planet { Name = "Uranius" });
            ServiceBase.Planet.Add(new Planet { Name = "Saturn" });
            ServiceBase.Planet.Add(new Planet { Name = "Pluto" });
            ServiceBase.Planet.Add(new Planet { Name = "ZX 710a" });
            ServiceBase.Planet.Add(new Planet { Name = "WWO 117" });

            // I also saw an approach where all services methods were extension methods
            // For example, if a user will add a post on facebook
            //      user.PostToFacebook(stringPost);
            //      public static void PostToFacebook(this User user, string message)

            // I have yet to test these methods and decide with one I like most
        }
    }
}
