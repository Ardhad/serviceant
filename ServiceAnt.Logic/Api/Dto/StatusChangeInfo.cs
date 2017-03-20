namespace ServiceAnt.Logic.Api.Dto
{
   public class StatusChangeInfo
   {
      public StatusChange StatusChange { get; set; }
      public string Info { get; set; }

      public static StatusChangeInfo Success()
      {
         return new StatusChangeInfo
         {
            StatusChange = StatusChange.Success,
            Info = string.Empty
         };
      }

      public static StatusChangeInfo Failure(string message)
      {
         return new StatusChangeInfo
         {
            StatusChange = StatusChange.Failure,
            Info = message
         };
      }

      public static StatusChangeInfo NotExist(string message)
      {
         return new StatusChangeInfo
         {
            StatusChange = StatusChange.NotExist,
            Info = message
         };
      }
   }
}