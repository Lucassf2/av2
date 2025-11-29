using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using HamburgueriaBlazor.Data;
using HamburgueriaBlazor.Repository.IRepository;

namespace HamburgueriaBlazor.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductRepository(
            IDbContextFactory<ApplicationDbContext> contextFactory,
            IWebHostEnvironment webHostEnvironment)
        {
            _contextFactory = contextFactory;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Product> CreateAsync(Product obj)
        {
            using var db = _contextFactory.CreateDbContext();
            db.Product.Add(obj);
            await db.SaveChangesAsync();
            return obj;
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            using var db = _contextFactory.CreateDbContext();
            var existing = await db.Product.FirstOrDefaultAsync(p => p.Id == obj.Id);
            if (existing == null)
            {
                return obj;
            }

            existing.Name = obj.Name;
            existing.Price = obj.Price;
            existing.Description = obj.Description;
            existing.SpecialTag = obj.SpecialTag;
            existing.CategoryId = obj.CategoryId;
            existing.ImageUrl = obj.ImageUrl;

            db.Product.Update(existing);
            await db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var entity = await db.Product.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            db.Product.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Product
                .Include(p => p.Category)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
