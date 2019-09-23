using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web;
using Spring.Context.Support;
using Spring.Context;
using Rain.BaseEntities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Rain.BaseRepository
{
    public class RainDbContext : DbContext
    {
        public RainDbContext(string nameOrConnectionString)
               : base(nameOrConnectionString ?? "RainDbContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Database.CommandTimeout = 720;
        }

        public RainDbContext() : this((HttpContext.Current == null || HttpContext.Current.Session == null)
            ? null : "data source=DESKTOP-AEIOJO6;initial catalog=AdventureWorks2012;user id=sa;pwd=szwk_2019")
        {

        }

        public IList<IMapping> Mappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();

            if (Mappings != null)
            {
                foreach (var mapping in Mappings)
                {
                    mapping.RegistTo(modelBuilder.Configurations);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public static RainDbContext GetContext(string objectName = "dbContext")
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            return ctx.GetObject(objectName) as RainDbContext;
        }
    }
}
