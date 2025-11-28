using Microsoft.EntityFrameworkCore;
using HamburgueriaBlazor.Data;
using HamburgueriaBlazor.Repository.IRepository;

namespace HamburgueriaBlazor.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public OrderRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeader)
        {
            using var db = _contextFactory.CreateDbContext();
            orderHeader.OrderDate = DateTime.Now;
            await db.OrderHeader.AddAsync(orderHeader);
            await db.SaveChangesAsync();
            return orderHeader;
        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync(string? userId = null)
        {
            using var db = _contextFactory.CreateDbContext();
            if (!string.IsNullOrEmpty(userId))
            {
                return await db.OrderHeader.Where(u => u.UserId == userId).ToListAsync();
            }
            return await db.OrderHeader.ToListAsync();
        }

        public async Task<OrderHeader> GetAsync(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            return await db.OrderHeader.Include(u => u.OrderDetails).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<OrderHeader> UpdateStatusAsync(int orderId, string status)
        {
            using var db = _contextFactory.CreateDbContext();
            var orderHeader = db.OrderHeader.FirstOrDefault(u => u.Id == orderId);
            if (orderHeader != null)
            {
                orderHeader.Status = status;
                await db.SaveChangesAsync();
            }
            return orderHeader;
        }
    }
}
