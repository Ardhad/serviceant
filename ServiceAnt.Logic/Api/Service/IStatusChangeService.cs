using ServiceAnt.DataAccess.Model;
using ServiceAnt.Logic.Api.Dto;

namespace ServiceAnt.Logic.Api.Service
{
   public interface IStatusChangeService : IService
   {
      StatusChangeInfo Start(int id);
      StatusChangeInfo Stop(int id);
   }
}