using System.Collections.Generic;
using ServiceAnt.DataAccess.Model;

namespace ServiceAnt.DataAccess.Interface.Repository
{
   public interface IFrequentlyUsedServiceRepository : IRepository<FrequentlyUsedService>
   {
      IEnumerable<FrequentlyUsedService> AllFromUser(string userName);
   }
}