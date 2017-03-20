using ServiceAnt.DataAccess.Interface;
using ServiceAnt.DataAccess.Interface.Repository;
using ServiceAnt.DataAccess.Model;
using ServiceAnt.Logic.Api.Dto;
using ServiceAnt.Logic.Api.Service;

namespace ServiceAnt.Logic.Service
{
   public class NewFusService : INewFusService
   {
      private readonly IFrequentlyUsedServiceRepository _repository;
      private readonly IIdentityService _identityService;
      private readonly IUnitOfWork _unitOfWork;

      public NewFusService(IFrequentlyUsedServiceRepository repository, IIdentityService identityService, IUnitOfWork unitOfWork)
      {
         _repository = repository;
         _identityService = identityService;
         _unitOfWork = unitOfWork;
      }

      public void Create(NewFusDto newFus)
      {
         var newEntity = new FrequentlyUsedService
         {
            UserName = _identityService.UserName,
            MachineName = newFus.MachineName,
            Alias = newFus.ServiceAlias,
            Name = newFus.ServiceName
         };

         _repository.Insert(newEntity);
         _unitOfWork.Commit();
      }
   }
}