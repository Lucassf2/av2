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
            await db.Category.AddAsync(obj);
            await db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var obj = await db.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                db.Category.Remove(obj);
                return (await db.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var obj = await db.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                return new Category();
            }
            return obj;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Category.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            using var db = _contextFactory.CreateDbContext();
            var objFromDb = await db.Category.FirstOrDefaultAsync(x => x.Id == obj.Id);
            if (objFromDb is not null)
            {
                objFromDb.Name = obj.Name;
                db.Category.Update(objFromDb);
                await db.SaveChangesAsync();
                return objFromDb;
            }
            return obj;
        }
    }
}