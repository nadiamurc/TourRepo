using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Tour.DomainModel;

namespace DAL
{
    public class ApplicationDataBaseContext : DbContext
    {
        public ApplicationDataBaseContext()
        :base("name=DatabaseEntities")
        {
        }

        public virtual DbSet<LocationType> LocationTypes { get; set; }
    }
}
