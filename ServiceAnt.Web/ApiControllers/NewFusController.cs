using System.Collections.Generic;
using System.Web.Http;
using ServiceAnt.Logic.Api.Dto;
using ServiceAnt.Logic.Api.Service;
using ServiceAnt.Web.Models;
using ServiceAnt.Web.Utils;

namespace ServiceAnt.Web.ApiControllers
{
    public class NewFusController : ApiController
    {
       private readonly INewFusService _service;
       private readonly INewFusValidationService _validationService;


       public NewFusController(INewFusService service, INewFusValidationService validationService)
       {
          _service = service;
          _validationService = validationService;
       }

       // POST api/newfus
       public RequestResult Post([FromBody]NewFusVm newFus)
       {
          var newFusDto = new NewFusDto
          {
             MachineName = newFus.MachineName,
             ServiceAlias = newFus.ServiceAlias,
             ServiceName = newFus.ServiceName
          };

          var validationResult = _validationService.Validate(newFusDto);
          if (validationResult.Success)
          {
             _service.Create(newFusDto);
             return RequestResult.GetSuccess();
          }
          
          return RequestResult.GetFailure(string.Join(" ", validationResult.Messages));
       }
    }
}
