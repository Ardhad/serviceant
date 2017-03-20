namespace ServiceAnt.DataAccess.Model
{
   /// <remarks>Klasa bazowa dla wszystkich entitów. Nie może być interfejsem, żeby możliwe było generyczne pobieranie DbSetów z contextu poprzez
   /// metodę DbContext.Set<>().</remarks>
   public abstract class EntityBase
   {
      public int ID { get; set; }
   }
}