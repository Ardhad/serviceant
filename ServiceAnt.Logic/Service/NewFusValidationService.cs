using System.ServiceProcess;
using ServiceAnt.Logic.Api.Dto;
using ServiceAnt.Logic.Api.Factory;
using ServiceAnt.Logic.Api.Service;
using ServiceAnt.Logic.ServicesManagement;

namespace ServiceAnt.Logic.Service
{
   public class NewFusValidationService : INewFusValidationService
   {
      private readonly IMachineFactory _machineFactory;

      public NewFusValidationService(IMachineFactory machineFactory)
      {
         _machineFactory = machineFactory;
      }

      public ValidationResult Validate(NewFusDto newFus)
      {
         if (CheckRequiredFields(newFus) == false)
         {
            return ValidationResult.GetFailure("Nie wszystkie wymagane pola zostały uzupełnione.");
         }

         var machine = _machineFactory.Get(newFus.MachineName);

         if(machine.Exists() == false)
            return ValidationResult.GetFailure(string.Format("Host o nazwie {0} nie istnieje lub nie masz do niego dostępu.", newFus.MachineName));

         if (machine.IsRunning() == false)
            return ValidationResult.GetFailure(string.Format("Host o nazwie {0} nie jest uruchomiony.", newFus.MachineName));

         var service = machine.GetService(newFus.ServiceName);
         if (service == null)
            return ValidationResult.GetFailure(string.Format("Serwis o nazwie {0} nie istnieje.", newFus.ServiceName));

         return ValidationResult.GetSuccess();
      }

      private bool CheckRequiredFields(NewFusDto newFus)
      {
         if (string.IsNullOrEmpty(newFus.MachineName) || string.IsNullOrEmpty(newFus.ServiceAlias) ||
             string.IsNullOrEmpty(newFus.ServiceName))
            return false;

         return true;
      }
   }
}