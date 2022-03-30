using _0330_Auth.Models.DBEntity;
using _0330_Auth.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace _0330_Auth.Repositories
{
    public class DBRepository : IDBRepository
    {
        private readonly AuthDBContext _dbContext;
        public DBRepository(AuthDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AuthDBContext DBContext { get { return _dbContext; } }

        public void Create<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
