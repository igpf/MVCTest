using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// By default queries do not compare null when passed as parameters. This methods sets UseDatabaseNullSemantics = false, which
        /// makes searches with null param work correctly.
        /// </summary>
        void EnableNullComparison();

        IRepository<T> RepositoryFor<T>() where T : class;

        object RepositoryFor(Type targetType);

        void SaveChanges();

        Task<int> SaveChangesAsync();

        void RefreshContext();
        bool HasChanges();

        DbContext Context { get; }
    }
}
