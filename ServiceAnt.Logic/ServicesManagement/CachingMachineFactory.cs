using System.Collections.Generic;
using ServiceAnt.Logic.Api.Factory;

namespace ServiceAnt.Logic.ServicesManagement
{
   public class CachingMachineFactory : IMachineFactory
   {
      private readonly Dictionary<string, Machine> _hosts;

      public CachingMachineFactory()
      {
         _hosts = new Dictionary<string, Machine>();
      }

      public Machine Get(string hostName)
      {
         if (_hosts.ContainsKey(hostName))
            return _hosts[hostName];

         var newHost = new Machine(hostName);
         _hosts.Add(hostName, newHost);

         return newHost;
      }
   }
}