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
			var cartItems = await db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();
			db.ShoppingCart.RemoveRange(cartItems);
			return await db.SaveChangesAsync() > 0;
		}

		public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string? userId)
		{
			using var db = _contextFactory.CreateDbContext();
			return await db.ShoppingCart.Where(u => u.UserId == userId).Include(u => u.Product).ToListAsync();
		}

		public async Task<bool> UpdateCartAsync(string userId, int productId, int updateBy)
		{
			if (string.IsNullOrEmpty(userId))
			{
				return false;
			}

			using var db = _contextFactory.CreateDbContext();

			var cart = await db.ShoppingCart.FirstOrDefaultAsync(u => u.UserId == userId && u.ProductId == productId);
			if (cart == null)
			{
				cart = new ShoppingCart
				{
					UserId = userId,
					ProductId = productId,
					Count = updateBy
				};

				await db.ShoppingCart.AddAsync(cart);
			}
			else
			{
				cart.Count += updateBy;
				if (cart.Count <= 0)
				{
					db.ShoppingCart.Remove(cart);
				}
			}
			return await db.SaveChangesAsync() > 0;
		}

        public async Task<int> GetTotalCartCartCountAsync(string? userId)
        {
            using var db = _contextFactory.CreateDbContext();
            int cartCount = 0;
            var cartItems = await db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();

            foreach (var item in cartItems)
            {
                cartCount += item.Count;
            }
            return cartCount;
        }
	}
}
