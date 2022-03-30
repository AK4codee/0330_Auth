using _0330_Auth.Models.DBEntity;
using System.Linq;

namespace _0330_Auth.Repositories.Interface
{
    public interface IDBRepository
    {
        public AuthDBContext DBContext { get; }
        public void Create<T>(T entity)  where T : class;
        public void Update<T>(T entity) where T : class;
        public void Delete<T>(T entity) where T : class;
        public IQueryable<T> GetAll<T>() where T : class;
        public void Save();
    }
}
