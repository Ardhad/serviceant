namespace ServiceAnt.Web.Utils
{
   public class RequestResult
   {
      public bool Success { get; set; }
      public string Message { get; set; } 
   
      public static RequestResult GetSuccess()
      {
         return new RequestResult
         {
            Success = true
         };
      }

      public static RequestResult GetFailure(string message)
      {
         return new RequestResult
         {
            Success = false,
            Message = message
         };
      }
   }

   
}