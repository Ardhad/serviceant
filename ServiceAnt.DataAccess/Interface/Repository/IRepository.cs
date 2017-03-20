using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ServiceAnt.DataAccess.Model;

namespace ServiceAnt.DataAccess.Interface.Repository
{
   public interface IRepository<TEntity>
      where TEntity : EntityBase
   {
      TEntity Get(int id);
      IQueryable<TEntity> Query();
      void Insert(TEntity entity);
      void Insert(IEnumerable<TEntity> entities);
      void Update(TEntity entity);
      void Update(TEntity entity, List<Expression<Func<TEntity, object>>> propertiesToUpdate);
      void Delete(int id);
      void Delete(Expression<Func<TEntity, bool>> filter);
      void Delete(TEntity entity);
      int Count(Expression<Func<TEntity, bool>> filter = null);
      IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);
      IEnumerable<TEntity> All { get; }   
   }
}