using Microsoft.EntityFrameworkCore;
using HamburgueriaBlazor.Data;
using HamburgueriaBlazor.Repository.IRepository;

namespace HamburgueriaBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public CategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Category> CreateAsync(Category obj)
        {
            using var db = _contextFactory.CreateDbContext();
            db.Category.Add(obj);
            await db.SaveChangesAsync();
            return obj;
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            using var db = _contextFactory.CreateDbContext();
            var existing = await db.Category.FirstOrDefaultAsync(c => c.Id == obj.Id);
            if (existing == null)
            {
                return obj;
            }

            existing.Name = obj.Name;
            db.Category.Update(existing);
            await db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var entity = await db.Category.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            db.Category.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Category> GetAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Category.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Category.AsNoTracking().ToListAsync();
        }
    }
}
