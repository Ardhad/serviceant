using System.ServiceProcess;
using ServiceAnt.DataAccess.Model;
using ServiceAnt.Logic.Api.Factory;
using ServiceAnt.Logic.Api.Service;
using ServiceAnt.Logic.ServicesManagement;

namespace ServiceAnt.Logic.Service
{
   public class ServicesCommonService : IServicesCommonService
   {
      private readonly Machine _host;
      private readonly string _serviceName;
      private readonly IMachineFactory _machineFactory;

      public ServicesCommonService(string hostName, string serviceName, IMachineFactory machineFactory)
      {
         _serviceName = serviceName;
         _machineFactory = machineFactory;
         _host = _machineFactory.Get(hostName);
      }
  
      public ServiceStatus GetStatus()
      {
         if (_host.Exists() == false)
            return ServiceStatus.HostNotExist;
         if (_host.IsRunning() == false) 
            return ServiceStatus.HostDown;

         var service = _host.GetService(_serviceName);
         if (service == null)
         {
            return ServiceStatus.NotExist;
         }

         return MapStatus(service.Status);
      }
      
      public string GetDisplayName()
      {
         var service = Get();
         return service == null ? string.Empty : service.DisplayName;
      }

      public ServiceController Get()
      {
         if (_host.Exists() == false || _host.IsRunning() == false)
         {
            return null;
         }

         return _host.GetService(_serviceName);
      }

      public ServiceStatus MapStatus(ServiceControllerStatus status)
      {
         switch (status)
         {
            case ServiceControllerStatus.ContinuePending:
               return ServiceStatus.ContinuePending;
            case ServiceControllerStatus.PausePending:
               return ServiceStatus.PausePending;
            case ServiceControllerStatus.Paused:
               return ServiceStatus.Paused;
            case ServiceControllerStatus.Running:
               return ServiceStatus.Running;
            case ServiceControllerStatus.StartPending:
               return ServiceStatus.StartPending;
            case ServiceControllerStatus.StopPending:
               return ServiceStatus.StopPending;
            case ServiceControllerStatus.Stopped:
               return ServiceStatus.Stopped;
            default:
               return ServiceStatus.NotExist;
         }
      }
   }
}