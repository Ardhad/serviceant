using System.ServiceProcess;
using ServiceAnt.DataAccess.Interface.Repository;
using ServiceAnt.DataAccess.Model;
using ServiceAnt.Logic.Api.Dto;
using ServiceAnt.Logic.Api.Factory;
using ServiceAnt.Logic.Api.Service;
using ServiceAnt.Logic.ServicesManagement;

namespace ServiceAnt.Logic.Service
{
   public class StatusChangeService : IStatusChangeService
   {
      private readonly IFrequentlyUsedServiceRepository _repository;
      private readonly IMachineFactory _machineFactory;

      public StatusChangeService(IFrequentlyUsedServiceRepository repository, IMachineFactory machineFactory)
      {
         _repository = repository;
         _machineFactory = machineFactory;
      }

      public StatusChangeInfo Start(int id)
      {
         var fus = _repository.Get(id);
         if (fus == null)
         {
            return StatusChangeInfo.NotExist(string.Format("W bazie nie istnieje serwis o id {0}.", id));
         }

         StatusChangeInfo statusChangeInfo;
         var service = TryGetService(fus.MachineName, fus.Name, out statusChangeInfo);;
         if (service == null)
         {
            return statusChangeInfo;
         }

         if (StatusChangeMachine.CanChange(service, ServiceControllerStatus.Running) == false)
         {
            return StatusChangeInfo.Failure(string.Format("Nie można uruchomić serwisu w statusie {0}.", service.Status));
         }
         service.Start();

         return StatusChangeInfo.Success();
      }

      private ServiceController TryGetService(string machineName, string serviceName, out StatusChangeInfo statusChangeInfo)
      {
         var machine = _machineFactory.Get(machineName);
         if (machine.Exists() == false || machine.IsRunning() == false)
         {
            {
               statusChangeInfo = StatusChangeInfo.NotExist(
                  string.Format("Maszyna o nazwie {0} nie istnieje lub jest wyłączona.", machineName));
               return null;
            }
         }

         var service = machine.GetService(serviceName);
         if (service == null)
         {
            statusChangeInfo = StatusChangeInfo.NotExist(
               string.Format("Serwis o nazwie {0} nie istnieje.", serviceName));
            return null;  
         }
         
         statusChangeInfo = StatusChangeInfo.Success();

         return service;
      }

      public StatusChangeInfo Stop(int id)
      {
         var fus = _repository.Get(id);
         if (fus == null)
         {
            return StatusChangeInfo.NotExist(string.Format("W bazie nie istnieje serwis o id {0}.", id));
         }

         StatusChangeInfo statusChangeInfo;

         var service = TryGetService(fus.MachineName, fus.Name, out statusChangeInfo); ;
         if (service == null)
         {
            return statusChangeInfo;
         }

         if (StatusChangeMachine.CanChange(service, ServiceControllerStatus.Stopped) == false)
         {
            return StatusChangeInfo.Failure(string.Format("Nie można zatrzymać serwisu w statusie {0}.", service.Status));
         }
         service.Stop();

         return StatusChangeInfo.Success();
      }
   }
}