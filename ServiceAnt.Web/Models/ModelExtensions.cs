using System;
using System.ComponentModel;
using System.Reflection;

namespace ServiceAnt.Web.Models
{
   public static class ModelExtensions
   {
      public static string GetDescription(this Enum value)
      {
         FieldInfo fi = value.GetType().GetField(value.ToString());

         DescriptionAttribute[] attributes =
             (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

         if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
         else
            return value.ToString();
      }
   }
}