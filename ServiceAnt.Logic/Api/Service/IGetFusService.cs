using System.Collections.Generic;
using ServiceAnt.DataAccess.Model;
using ServiceAnt.Logic.Api.Dto;

namespace ServiceAnt.Logic.Api.Service
{
   public interface IGetFusService : IService
   {
      IEnumerable<FrequentlyUsedServiceDto> GetAll(string userName);
   }
}