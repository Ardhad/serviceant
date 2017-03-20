namespace ServiceAnt.Logic.Api.Service
{
   public interface IIdentityService : IService
   {
      string UserName { get; }
   }
}