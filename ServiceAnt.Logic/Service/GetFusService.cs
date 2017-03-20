using System.Collections.Generic;
using System.Linq;
using ServiceAnt.DataAccess.Interface.Repository;
using ServiceAnt.DataAccess.Model;
using ServiceAnt.Logic.Api.Dto;
using ServiceAnt.Logic.Api.Factory;
using ServiceAnt.Logic.Api.Service;

namespace ServiceAnt.Logic.Service
{
   public class GetFusService : IGetFusService
   {
      private readonly IFrequentlyUsedServiceRepository _repository;
      private readonly IMachineFactory _machineFactory;

      public GetFusService(IFrequentlyUsedServiceRepository repository, IMachineFactory machineFactory)
      {
         _repository = repository;
         _machineFactory = machineFactory;
      }

      public IEnumerable<FrequentlyUsedServiceDto> GetAll(string userName)
      {
         return from service in _repository.AllFromUser(userName) 
            let servicesCommonService = new ServicesCommonService(service.MachineName, service.Name, _machineFactory) 
            select new FrequentlyUsedServiceDto
            {
               Id = service.ID,
               UserName = service.UserName,
               MachineName = service.MachineName,
               Alias = service.Alias,
               Name = service.Name,
               Status = servicesCommonService.GetStatus(),
               DisplayName = servicesCommonService.GetDisplayName()
            };
      }
   }
}