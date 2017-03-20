using System.Collections.Generic;
using System.Data.Entity;
using ServiceAnt.DataAccess.Context;
using ServiceAnt.DataAccess.Model;

namespace ServiceAnt.DataAccess.Initialize
{
   public class ServiceAntDbInitializer : DropCreateDatabaseIfModelChanges<ServiceAntContext>
   {
      protected override void Seed(ServiceAntContext context)
      {
         var services = new List<FrequentlyUsedService>()
         {
            new FrequentlyUsedService
            {
               MachineName = "localhost",
               Name = "W32Time",
               Alias = "Windows Time",
               UserName = "Gwidon"
            },
            new FrequentlyUsedService
            {
               MachineName = "localhost",
               Name = "aspnet_state",
               Alias = "ASP State Service",
               UserName = "Gwidon"
            },
            new FrequentlyUsedService
            {
               MachineName = "innykomp",
               Name = "Pit Service",
               Alias = "PIT",
               UserName = "Gwidon"
            }
         };

         services.ForEach(x => context.FrequentlyUsedService.Add(x));
         context.SaveChanges();
      }

   }
}