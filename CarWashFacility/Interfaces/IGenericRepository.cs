using System.Linq.Expressions;

namespace CarWashFacility.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public ICollection<T> GetAll();
        public T GetById(object id);
        public ICollection<T> GetByExpression(Expression<Func<T, bool>> expression);
        public object Add(T entity);
        public bool Update(T entity);
        
    }
}
