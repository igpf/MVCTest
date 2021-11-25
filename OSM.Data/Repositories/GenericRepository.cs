using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;


namespace OSM.Data.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbContext _context;

        public DbContext Context
        {
            get
            {
                if (_context == null)
                {
                    throw new Exception("Context not initialized.");
                }
                return _context;
            }
            set { _context = value; }
        }

        public GenericRepository()
        {
            Database.SetInitializer(new NullDatabaseInitializer<OSMDatabaseContext>());
            _context = new OSMDatabaseContext();
        }

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        /// <summary>
        /// By default queries do not compare null when passed as parameters. This methods sets UseDatabaseNullSemantics = false, which
        /// makes searches with null param work correctly.
        /// </summary>
        public void EnableNullComparison()
        {
            _context.Configuration.UseDatabaseNullSemantics = false;
        }

        public T GetItem(Expression<Func<T, bool>> filter = null,
            IEnumerable<Expression<Func<T, object>>> includeProperties = null,
            bool tracking = false)
        {
            var stopWatch = Stopwatch.StartNew();
            IQueryable<T> query = Context.Set<T>();
            var includePropertiesList = includeProperties?.ToList();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includePropertiesList != null)
            {
                query = includePropertiesList.Aggregate(query, (current, item) => current.Include(item));
            }

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            var result = query.SingleOrDefault();

            //Logger.LogDebug($"Repository query completed in " +
            //                $"QueryExecutionTimeMs={stopWatch.ElapsedMilliseconds}, " +
            //                $"Entity={typeof(T).Name}, " +
            //                $"RepositoryMethod=GetItem, " +
            //                $"IncludedProperties=[{string.Join(",", includePropertiesList ?? new List<Expression<Func<T, object>>>())}]");

            return result;
        }

        public T Expand(T obj, string prop, bool tracking = false)
        {
            IQueryable<T> query = Context.Set<T>();

            string predicate = calculatePredicate(obj);

            query = query.Where(predicate, null);

            query = query.Include(prop);

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            return query.SingleOrDefault();
        }

        private string calculatePredicate(T obj)
        {
            string result = "";

            var oType = obj.GetType();
            var properties = oType.GetProperties();

            foreach (var px in properties)
            {
                var value = oType.GetProperty(px.Name).GetValue(obj);

                if (value != null && IsNumericType(value.GetType()))
                {

                    result += result != "" ? "AND " : "";
                    result += px.Name + " = " + value.ToString();

                }
            }

            return result;
        }

        private bool IsNumericType(Type tx)
        {
            switch (Type.GetTypeCode(tx))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public IQueryable<T> GetItems()
        {
            return Context.Set<T>();
        }

        public IEnumerable<T> GetItems(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<Expression<Func<T, object>>> includeProperties = null,
            bool tracking = false,
            int page = 0,
            int pageSize = 0)
        {
            var stopWatch = Stopwatch.StartNew();
            IQueryable<T> query = Context.Set<T>();
            var includePropertiesList = includeProperties?.ToList();

            if (!tracking)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includePropertiesList != null)
            {
                query = includePropertiesList.Aggregate(query, (current, item) => current.Include(item));
            }

            if (orderBy != null)
            {
                query = orderBy(query);

                if (pageSize > 0)
                {
                    var resultsToSkip = page * pageSize;
                    query = query
                        .Skip(() => resultsToSkip)
                        .Take(() => pageSize);
                }
            }

            var result = query.ToList();

            //Logger.LogDebug($"Repository query completed in " +
            //                $"QueryExecutionTimeMs={stopWatch.ElapsedMilliseconds}, " +
            //                $"Entity={typeof(T).Name}, " +
            //                $"RepositoryMethod=GetItems, " +
            //                $"IncludedProperties=[{string.Join(",", includePropertiesList ?? new List<Expression<Func<T, object>>>())}]");

            return result;
        }

        public int Count(Expression<Func<T, bool>> filter = null)
        {
            var stopWatch = Stopwatch.StartNew();
            IQueryable<T> query = Context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var count = query.Count();

            //Logger.LogDebug($"Repository query completed in QueryExecutionTimeMs={stopWatch.ElapsedMilliseconds}, Entity={typeof(T).Name}, RepositoryMethod=Count");

            return count;
        }

        public bool Any(Expression<Func<T, bool>> filter = null)
        {
            var stopWatch = Stopwatch.StartNew();
            IQueryable<T> query = Context.Set<T>();

            var result = filter != null
                ? query.Any(filter)
                : query.Any();

            //Logger.LogDebug($"Repository query completed in QueryExecutionTimeMs={stopWatch.ElapsedMilliseconds}, Entity={typeof(T).Name}, RepositoryMethod=Any");

            return result;
        }

        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public virtual void Attach(T entity)
        {
            Context.Set<T>().Attach(entity);
        }

        public virtual void Update(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateValues(T dbEntity, T newEntity)
        {
            Context.Entry(dbEntity).CurrentValues.SetValues(newEntity);
        }

        public virtual void Remove(int id)
        {
            T toDelete = Context.Set<T>().Find(id);
            Remove(toDelete);
        }

        public virtual void Remove(string id)
        {
            T toDelete = Context.Set<T>().Find(id);
            Remove(toDelete);
        }

        public virtual void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var e in entities)
            {
                this.Remove(e);
            }
        }
    }
}
