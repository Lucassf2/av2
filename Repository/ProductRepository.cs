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
        public ProductRepository(IDbContextFactory<ApplicationDbContext> contextFactory, IWebHostEnvironment webHostEnvironment)
        {
            _contextFactory = contextFactory;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Product> CreateAsync(Product obj)
        {
            using var db = _contextFactory.CreateDbContext();
            await db.Product.AddAsync(obj);
            await db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var obj = await db.Product.FirstOrDefaultAsync(x => x.Id == id);
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('/'));
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            if (obj != null)
            {
                db.Product.Remove(obj);
               return (await db.SaveChangesAsync())>0;
            }
            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var obj = await db.Product.FirstOrDefaultAsync(x => x.Id == id);
            if(obj == null)
            {
                return new Product();
            }
            return obj;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.Product.Include(c=>c.Category).ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            using var db = _contextFactory.CreateDbContext();
            var objFromDb = await db.Product.FirstOrDefaultAsync(x => x.Id == obj.Id);
            if (objFromDb is not null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.ImageUrl = obj.ImageUrl;
                objFromDb.SpecialTag = obj.SpecialTag;

                
                db.Product.Update(objFromDb);
                await db.SaveChangesAsync();
                return objFromDb;
            }
            return obj;
        }
    }
}
