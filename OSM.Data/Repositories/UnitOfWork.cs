using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using OSM.Data.Repositories;


namespace OSM.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        //private readonly IAuthenticationProvider _authenticationProvider;

        //public UnitOfWork(IAuthenticationProvider authenticationProvider)
        //{
        //    Logger.LogInfo($"{nameof(UnitOfWork)} created");
        //    _authenticationProvider = authenticationProvider;
        //    RefreshContext();
        //}

        //public UnitOfWork(DbContext context, IAuthenticationProvider authenticationProvider)
        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //_authenticationProvider = authenticationProvider;
        }

        public DbContext Context => _context;

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        /// <summary>
        /// By default queries do not compare null when passed as parameters. This methods sets UseDatabaseNullSemantics = false, which
        /// makes searches with null param work correctly.
        /// </summary>
        public void EnableNullComparison()
        {
            _context.Configuration.UseDatabaseNullSemantics = false;
        }

        public IRepository<T> RepositoryFor<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public object RepositoryFor(Type targetType)
        {
            Type genericType = typeof(GenericRepository<>).MakeGenericType(targetType);

            return Activator.CreateInstance(genericType, _context);
        }

        private void PrepareForSaveChanges()
        {


        }

        private void WorkWithDbEntityValidationException(DbEntityValidationException e)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            PrepareForSaveChanges();
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                WorkWithDbEntityValidationException(e);
                throw;
            }
        }

        public void SaveChanges() //We should return the same type as  _context.SaveChanges();
        {
            PrepareForSaveChanges();
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                WorkWithDbEntityValidationException(e);
                throw;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        // Just to satisfy Interface definition
        public void RefreshContext()
        {
            _context?.Dispose();
            Database.SetInitializer(new NullDatabaseInitializer<OSMDatabaseContext>());
            _context = new OSMDatabaseContext();
        }
        
    }
}