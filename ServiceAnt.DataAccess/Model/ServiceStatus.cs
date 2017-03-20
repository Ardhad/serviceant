namespace ServiceAnt.DataAccess.Model
{
   public enum ServiceStatus
   {
      StartPending = 0,
      Running = 1,
      StopPending = 2,
      Stopped = 3,
      PausePending = 4,
      Paused = 5,
      ContinuePending = 6,
      NotExist = 7
   }
}