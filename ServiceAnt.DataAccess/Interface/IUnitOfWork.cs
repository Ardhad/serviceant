using System;

namespace ServiceAnt.DataAccess.Interface
{
   public interface IUnitOfWork
   {
      void Commit();
      void Rollback();
   }
}