using System.Collections.Generic;

namespace ServiceAnt.Logic.Api.Dto
{
   public class ValidationResult
   {
      public bool Success { get; set; }
      public IEnumerable<string> Messages { get; set; }

      public static ValidationResult GetSuccess()
      {
         return new ValidationResult
         {
            Success = true,
            Messages = new List<string>()
         };
      }

      public static ValidationResult GetFailure(params string[] messages)
      {
         return new ValidationResult
         {
            Success = false,
            Messages = new List<string>(messages)
         };
      }
   }
}