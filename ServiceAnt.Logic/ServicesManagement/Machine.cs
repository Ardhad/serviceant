using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;

namespace ServiceAnt.Logic.ServicesManagement
{
   public class Machine
   {
      public const string LocalMachineName = "ten komputer";
      public string Name { get; set; }

      private bool? _existCached;

      public Machine(string hostName)
      {
         Name = hostName.Equals(LocalMachineName) ? "localhost" : hostName;
      }

      public bool IsRunning()
      {
         if (Exists() == false)
            return false;

         var ping = new Ping();
         var reply = ping.Send(Name, 500);
         return reply != null && reply.Status == IPStatus.Success;
      }

      public bool Exists()
      {
         if (_existCached.HasValue)
            return _existCached.Value;

         try
         {
            var uriHostNameType = Dns.GetHostEntry(Name);
            _existCached = true;
            return true;
         }
         catch (Exception)
         {
            _existCached = false;
            return false;
         }
      }

      public IEnumerable<ServiceController> GetServices()
      {
         return IsLocalMachine() ? ServiceController.GetServices() : ServiceController.GetServices(Name);
      }

      private bool IsLocalMachine()
      {
         return Name.Equals(LocalMachineName);
      }

      public void ToggleService(string serviceName)
      {
         var serviceController = new ServiceController(serviceName, Name);
         switch (serviceController.Status)
         {
            case ServiceControllerStatus.Running:
               serviceController.Stop();
               break;
            case ServiceControllerStatus.Stopped:
               serviceController.Start();
               break;
            case ServiceControllerStatus.Paused:
               serviceController.Start();
               break;
         }
      }

      public ServiceController GetService(string serviceName)
      {
         return GetServices().FirstOrDefault(serv => serv.ServiceName.Equals(serviceName));
      }
   }
}