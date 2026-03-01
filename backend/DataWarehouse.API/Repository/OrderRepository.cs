using DataWarehouse.API.Models.RecordModels;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.API.Repository;

public class OrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task CreateOrderAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }
}
