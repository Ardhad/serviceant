using ServiceAnt.DataAccess.Model;

namespace ServiceAnt.Web.Models
{
   public class FrequentlyUsedServiceVm
   {
      public int Id { get; set; }
      public string MachineName { get; set; }
      public string Alias { get; set; }
      public string Name { get; set; }
      public string DisplayName { get; set; }
      public string UserName { get; set; }
      public ServiceStatus Status { get; set; }
   }
}