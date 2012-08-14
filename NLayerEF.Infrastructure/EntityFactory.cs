using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace NLayerEF.Infrastructure
{
    public class EntityFactory : IDbContextFactory<UniverseEntities>
    {
        /// <summary>
        /// This interface implementation helps something on designer time
        /// </summary>
        /// <returns></returns>
        public UniverseEntities Create()
        {
            return EntityFactory.Current;
        }

        /// <summary>
        /// This assembly is using System.Web, this example was made to use on the web
        /// The fallback to MemoryCache is for unit testing purposes
        /// </summary>
        public static UniverseEntities Current
        {
            get
            {
                UniverseEntities db;
                string storageKey = String.Format("{0}[{1}]", typeof(UniverseEntities).FullName, Thread.CurrentThread.ManagedThreadId);

                if (HttpContext.Current != null)
                {
                    db = HttpContext.Current.Session[storageKey] as UniverseEntities;

                    if (db == null)
                    {
                        HttpContext.Current.Session[storageKey] = db = new UniverseEntities();
                    }
                }
                else
                {
                    db = MemoryCache.Default.Get(storageKey) as UniverseEntities;

                    if (db == null)
                    {
                        db = new UniverseEntities();

                        MemoryCache.Default.Add(storageKey, db, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(15) });
                    }
                }

                return db;
            }
        }
    }
}
