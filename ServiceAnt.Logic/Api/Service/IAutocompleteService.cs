using System.Collections.Generic;
using System.ServiceProcess;

namespace ServiceAnt.Logic.Api.Service
{
   public interface IAutocompleteService
   {
      IEnumerable<string> GetServiceNames(string hostName, string prefix);
   }
}