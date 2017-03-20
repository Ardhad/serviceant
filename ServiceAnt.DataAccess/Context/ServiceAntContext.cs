using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ServiceAnt.DataAccess.Model;

namespace ServiceAnt.DataAccess.Context
{
   public class ServiceAntContext : DbContext
   {
      public ServiceAntContext() : base("ServiceAntDb")
      {}

      // DataSets
      public DbSet<FrequentlyUsedService> FrequentlyUsedService { get; set; }
      // ~DataSets

      protected override void OnModelCreating(DbModelBuilder builder)
      {
         builder.Conventions.Remove<PluralizingTableNameConvention>();
      }
   }
}