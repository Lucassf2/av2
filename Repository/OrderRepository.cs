using Microsoft.EntityFrameworkCore;
using HamburgueriaBlazor.Data;
using HamburgueriaBlazor.Repository.IRepository;

namespace HamburgueriaBlazor.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public OrderRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
        {
            using var db = _contextFactory.CreateDbContext();
            db.OrderHeader.Add(orderHeader);
            await db.SaveChangesAsync();
            return orderHeader;
        }

        public async Task<OrderHeader> GetAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.OrderHeader
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null)
        {
            using var db = _contextFactory.CreateDbContext();
            var query = db.OrderHeader
                .Include(o => o.OrderDetails)
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(o => o.UserId == userId);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<OrderHeader> UpdateStatusAsync(int orderId, string status)
        {
            using var db = _contextFactory.CreateDbContext();
            var order = await db.OrderHeader.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return null;
            }

            order.Status = status;
            db.OrderHeader.Update(order);
            await db.SaveChangesAsync();
            return order;
        }
    }
}
