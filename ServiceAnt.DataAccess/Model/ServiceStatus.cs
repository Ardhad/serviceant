using System.ComponentModel;

namespace ServiceAnt.DataAccess.Model
{
   public enum ServiceStatus
   {
      [Description("Uruchamianie")]
      StartPending = 0,

      [Description("Uruchomiony")]
      Running = 1,

      [Description("Wyłączanie")]
      StopPending = 2,

      [Description("Wyłączony")]
      Stopped = 3,

      [Description("Zatrzymywanie")]
      PausePending = 4,

      [Description("Zatrzymany")]
      Paused = 5,

      [Description("Uruchamianie")]
      ContinuePending = 6,

      [Description("Nie istnieje")]
      NotExist = 7,

      [Description("Nieznany host")]
      HostNotExist = 8,

      [Description("Wyłączony host")]
      HostDown = 9
   }
}