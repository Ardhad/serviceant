using ServiceAnt.Logic.Api.Dto;

namespace ServiceAnt.Logic.Api.Service
{
   public interface INewFusValidationService
   {
      ValidationResult Validate(NewFusDto newFus);
   }
}