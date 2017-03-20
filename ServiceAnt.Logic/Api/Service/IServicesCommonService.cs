using System.ServiceProcess;
using ServiceAnt.DataAccess.Model;

namespace ServiceAnt.Logic.Api.Service
{
   // [GJ] TODO najgorsza nazwa na świecie
   public interface IServicesCommonService
   {
      ServiceStatus GetStatus();
      string GetDisplayName();
   }
}