using ServiceAnt.Logic.ServicesManagement;

namespace ServiceAnt.Logic.Api.Factory
{
   public interface IMachineFactory
   {
      Machine Get(string hostName);
   }
}