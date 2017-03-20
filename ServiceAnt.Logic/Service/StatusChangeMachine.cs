using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;

namespace ServiceAnt.Logic.Service
{
   public class StatusChangeMachine
   {
      private static Dictionary<ServiceControllerStatus, IEnumerable<ServiceControllerStatus>> _possiblePredecessor =
         new Dictionary<ServiceControllerStatus, IEnumerable<ServiceControllerStatus>>
         {
            {ServiceControllerStatus.Running, new List<ServiceControllerStatus>{ServiceControllerStatus.Paused, ServiceControllerStatus.Stopped}},
            {ServiceControllerStatus.Stopped, new List<ServiceControllerStatus>{ServiceControllerStatus.Paused, ServiceControllerStatus.Running}},
            {ServiceControllerStatus.Paused, new List<ServiceControllerStatus>{ServiceControllerStatus.Running}}
         };

      public static bool CanChange(ServiceController service, ServiceControllerStatus newStatus)
      {
         switch (newStatus)
         {
            case ServiceControllerStatus.Stopped:
               return service.CanStop && _possiblePredecessor[ServiceControllerStatus.Stopped].Contains(service.Status);
            case ServiceControllerStatus.Running:
               return _possiblePredecessor[ServiceControllerStatus.Running].Contains(service.Status);
            case ServiceControllerStatus.Paused:
               return _possiblePredecessor[ServiceControllerStatus.Paused].Contains(service.Status);
            default:
               return false;
         }
      }
   }
}