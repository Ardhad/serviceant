using System;
using System.Data.Entity;
using ServiceAnt.DataAccess.Context;
using ServiceAnt.DataAccess.Interface;

namespace ServiceAnt.DataAccess
{
   public class UnitOfWork : IUnitOfWork
   {
      private ServiceAntContext _context;

      public UnitOfWork(ServiceAntContext context)
      {
         _context = context;
      }

      public void Commit()
      {
         _context.SaveChanges();
      }

      public void Rollback()
      {
         foreach (var entry in _context.ChangeTracker.Entries())
         {
            switch (entry.State)
            {
               case EntityState.Added:
                  entry.State = EntityState.Detached;
                  break;
               case EntityState.Deleted:
                  entry.Reload();
                  break;
               case EntityState.Modified:
                  entry.State = EntityState.Unchanged;
                  break;
            }
         }
      }

   }
}