using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ServiceAnt.DataAccess.Context;
using ServiceAnt.DataAccess.Interface.Repository;
using ServiceAnt.DataAccess.Model;

namespace ServiceAnt.DataAccess.Repository
{
   public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
      where TEntity : EntityBase
   {
      private readonly ServiceAntContext _context;
      private DbSet<TEntity> _dbSet;

      public RepositoryBase(ServiceAntContext context)
      {
         _context = context;
         _dbSet = _context.Set<TEntity>();
      }

      public TEntity Get(int id)
      {
         return Find(entity => entity.ID == id).FirstOrDefault();
      }

      // TODO wymagany cast czy nie?
      public IQueryable<TEntity> Query()
      {
         return _dbSet;
      }

      public void Insert(TEntity entity)
      {
         _dbSet.Add(entity);
      }

      public void Insert(IEnumerable<TEntity> entities)
      {
         _dbSet.AddRange(entities);
      }

      public void Update(TEntity entity)
      {
         if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);
         _context.Entry(entity).State = EntityState.Modified;
      }

      public void Update(TEntity entity, List<Expression<Func<TEntity, object>>> propertiesToUpdate)
      {
         if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);

         foreach (var propertyToModify in propertiesToUpdate)
         {
            _context.Entry(entity).Property(propertyToModify).IsModified = true;
         }
      }

      public void Delete(int id)
      {
         var entity = Get(id);
         Delete(entity);
      }

      public void Delete(Expression<Func<TEntity, bool>> filter)
      {
         foreach (var entity in Find(filter))
         {
            Delete(entity);
         }
      }

      public void Delete(TEntity entity)
      {
         if(entity == null)
            throw new ArgumentNullException("entity", "Given entity cannot be deleted or do not exist.");

         if (_context.Entry(entity).State == EntityState.Detached)
         {
            _dbSet.Attach(entity);
         }

         _dbSet.Remove(entity);
      }

      public int Count(Expression<Func<TEntity, bool>> filter = null)
      {
         if (filter == null)
            return _dbSet.Count();

         return Find(filter).Count();
      }

      public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
      {
         var query = _dbSet.Where(filter);
         return query;
      }

      public IEnumerable<TEntity> All
      {
         get { return _dbSet.ToArray(); }
      }
   }
}