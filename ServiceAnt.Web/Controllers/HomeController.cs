using System.Linq;
using System.Web.Mvc;
using ServiceAnt.Logic.Api.Dto;
using ServiceAnt.Logic.Api.Service;
using ServiceAnt.Web.Models;

namespace ServiceAnt.Web.Controllers
{
   public class HomeController : Controller
   {
      private const string StatusChangeInfoAlias = "StatusChangeFailure";
      private readonly IGetFusService _getFusService;
      private readonly IIdentityService _identityService;
      private readonly IStatusChangeService _statusChangeService;

      public HomeController(IGetFusService getFusService, IIdentityService identityService, IStatusChangeService statusChangeService)
      {
         _getFusService = getFusService;
         _identityService = identityService;
         _statusChangeService = statusChangeService;
      }

      public ActionResult Index()
      {
         ViewBag.Title = string.Format("Moja lista serwisów. Nazywam się {0}.", _identityService.UserName);
         ViewBag.StatusChangeInfo = TempData.ContainsKey(StatusChangeInfoAlias)
            ? TempData[StatusChangeInfoAlias]
            : string.Empty;

         var allServices = _getFusService.GetAll(_identityService.UserName);

         return View(allServices.Select(x => new FrequentlyUsedServiceVm
         {
            Id = x.Id,
            UserName = x.UserName,
            MachineName = x.MachineName,
            Name = x.Name,
            DisplayName = x.DisplayName,
            Alias = x.Alias,
            Status = x.Status
         }));
      }
      public ActionResult GridPartial()
      {
         ViewBag.StatusChangeInfo = TempData.ContainsKey(StatusChangeInfoAlias)
            ? TempData[StatusChangeInfoAlias]
            : string.Empty;

         var allServices = _getFusService.GetAll(_identityService.UserName);

         return PartialView("GridPartial", allServices.Select(x => new FrequentlyUsedServiceVm
         {
            Id = x.Id,
            UserName = x.UserName,
            MachineName = x.MachineName,
            Name = x.Name,
            DisplayName = x.DisplayName,
            Alias = x.Alias,
            Status = x.Status
         }));
      }

      public ActionResult Start(int id)
      {
         var statusChangeInfo = _statusChangeService.Start(id);
         if (statusChangeInfo.StatusChange != StatusChange.Success)
         {
            TempData[StatusChangeInfoAlias] = statusChangeInfo.Info;
         }

         return RedirectToAction("Index");
      }

      public ActionResult Stop(int id)
      {
         var statusChangeInfo = _statusChangeService.Stop(id);
         if (statusChangeInfo.StatusChange != StatusChange.Success)
         {
            TempData[StatusChangeInfoAlias] = statusChangeInfo.Info;
         }

         return RedirectToAction("Index");
      }
   }
}
