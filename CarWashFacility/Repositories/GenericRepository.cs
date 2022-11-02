using CarWashFacility.Data;
using CarWashFacility.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarWashFacility.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _applicationDbContext { get; set; }
        private DbSet<T> table { get; set; }
        
        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            table = _applicationDbContext.Set<T>();
        }
        public object Add(T entity)
        {
            table.Add(entity);
            Save();
            return entity.GetType().GetProperty("Id").GetValue(entity,null);
        }

        public ICollection<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public ICollection<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            var result = table.Where(expression).ToList();
            return result;
        }

        public bool Update(T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Detached;
            table.Update(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            var result = Save();
            return result;
        }
        protected bool Save()
        {
            return _applicationDbContext.SaveChanges() > 0;
        }
    }
}
