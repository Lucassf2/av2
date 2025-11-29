using Microsoft.EntityFrameworkCore;
using HamburgueriaBlazor.Data;
using HamburgueriaBlazor.Repository.IRepository;

namespace HamburgueriaBlazor.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ShoppingCartRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> ClearCartAsync(string? userId)
        {
            using var db = _contextFactory.CreateDbContext();
            var cartItems = await db.ShoppingCart
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (cartItems.Count == 0)
            {
                return false;
            }

            db.ShoppingCart.RemoveRange(cartItems);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.ShoppingCart
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> UpdateCartAsync(string userId, int product, int updateBy)
        {
            using var db = _contextFactory.CreateDbContext();
            var cartItem = await db.ShoppingCart
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == product);

            if (cartItem == null)
            {
                if (updateBy <= 0)
                {
                    return false;
                }

                cartItem = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = product,
                    Count = updateBy
                };

                db.ShoppingCart.Add(cartItem);
            }
            else
            {
                cartItem.Count += updateBy;

                if (cartItem.Count <= 0)
                {
                    db.ShoppingCart.Remove(cartItem);
                }
                else
                {
                    db.ShoppingCart.Update(cartItem);
                }
            }

            await db.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalCartCartCountAsync(string? userId)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.ShoppingCart
                .Where(c => c.UserId == userId)
                .SumAsync(c => c.Count);
        }
    }
}
