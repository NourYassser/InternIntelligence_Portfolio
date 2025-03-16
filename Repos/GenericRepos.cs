using InternIntelligence_Portfolio.Models.DataBase;

namespace InternIntelligence_Portfolio.Repos
{
    public interface IGenericRepo<T>
    {
        public IQueryable<T> GetAll();
        public T GetById(int id);
        public void Add(T t);
        public void Delete(T t);
        public void Update(T t);
        void Save();
    }

    public class GenericRepos<T> : IGenericRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepos(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().ToList().AsQueryable();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Add(T t)
        {
            _context.Set<T>().Add(t);
            Save();
        }

        public void Delete(T t)
        {
            _context.Set<T>().Remove(t);
            Save();
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T t)
        {
            _context.Set<T>().Update(t);
            Save();
        }
    }
}
