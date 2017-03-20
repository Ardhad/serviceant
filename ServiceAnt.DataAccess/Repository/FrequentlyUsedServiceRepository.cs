using System.Collections.Generic;
using System.Linq;
using ServiceAnt.DataAccess.Context;
using ServiceAnt.DataAccess.Interface.Repository;
using ServiceAnt.DataAccess.Model;

namespace ServiceAnt.DataAccess.Repository
{
   public class FrequentlyUsedServiceRepository : RepositoryBase<FrequentlyUsedService>, IFrequentlyUsedServiceRepository
   {
      public FrequentlyUsedServiceRepository(ServiceAntContext context) : base(context)
      {
      }

      public IEnumerable<FrequentlyUsedService> AllFromUser(string userName)
      {
         return Query().Where(x => x.UserName.Equals(userName));
      }
   }
}