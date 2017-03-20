namespace ServiceAnt.Web.Utils
{
   public class MembershipHelper
   {
      /// <summary>
      /// Pobiera nazwę użytkownika z podanego stringa, omijając nazwę domeny.
      /// </summary>
      /// <param name="fullName">nazwa użytkownika zawierająca informację o domenie (domain\user)</param>
      /// <returns>nazwa użytkownika bez domeny</returns>
      public static string GetUserName(string fullName)
      {
         if (string.IsNullOrEmpty(fullName))
            return string.Empty;

         var splittedIdentity = fullName.Split('\\');
         return splittedIdentity.Length > 1 ? splittedIdentity[1] : splittedIdentity[0];
      }
   }
}