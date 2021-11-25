using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OSM.Data.Repositories

{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// By default queries do not compare null when passed as parameters. This methods sets UseDatabaseNullSemantics = false, which
        /// makes searches with null param work correctly.
        /// </summary>
        void EnableNullComparison();

        IQueryable<T> GetItems();

        T GetItem(Expression<Func<T, bool>> filter = null,
            IEnumerable<Expression<Func<T, object>>> includeProperties = null,
            bool tracking = false);

        T Expand(T obj, string prop, bool tracking = false);

        IEnumerable<T> GetItems(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<Expression<Func<T, object>>> includeProperties = null,
            bool tracking = false,
            int page = 0,
            int pageSize = 0);

        int Count(Expression<Func<T, bool>> filter = null);

        bool Any(Expression<Func<T, bool>> filter = null);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Attach(T entity);

        void Update(T entity);

        void UpdateValues(T dbEntity, T newEntity);

        void Remove(int id);

        void Remove(string id);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}