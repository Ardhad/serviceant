using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using ServiceAnt.Logic.Api.Factory;
using ServiceAnt.Logic.Api.Service;

namespace ServiceAnt.Logic.Service
{
   public class AutocompleteService : IAutocompleteService
   {
      private readonly IMachineFactory _machineFactory;
      public AutocompleteService(IMachineFactory machineFactory)
      {
         _machineFactory = machineFactory;
      }

      public IEnumerable<string> GetServiceNames(string hostName, string prefix)
      {
         if(string.IsNullOrEmpty(hostName))
            return Enumerable.Empty<string>();

         var machine = _machineFactory.Get(hostName);
         if(machine.Exists() == false)
            return Enumerable.Empty<string>();

         return machine.GetServicesByPrefix(prefix).Select(x => x.ServiceName);
      }
   }
}