using DataWarehouse.API.Models.RecordModels;
using DataWarehouse.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.API.Tests;

public class OrderRepositoryTests
{
    // Helper to create in-memory DbContext
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique DB for each test
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task CreateOrderAsync_AddsOrderSuccessfully()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new OrderRepository(context);

        var newOrder = new Order
        {
            OrderDate = DateTime.Now,
            Amount = 150.50m
        };

        // Act
        await repository.CreateOrderAsync(newOrder);
        var orders = await repository.GetOrdersAsync();

        // Assert
        Assert.Single(orders);
        Assert.Equal(150.50m, orders.First().Amount);
    }

    [Fact]
    public async Task GetOrdersAsync_ReturnsMultipleOrders()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new OrderRepository(context);

        await repository.CreateOrderAsync(new Order { OrderDate = DateTime.Now, Amount = 100m });
        await repository.CreateOrderAsync(new Order { OrderDate = DateTime.Now, Amount = 200m });

        // Act
        var orders = await repository.GetOrdersAsync();

        // Assert
        Assert.Equal(2, orders.Count());
        Assert.Contains(orders, o => o.Amount == 100m);
        Assert.Contains(orders, o => o.Amount == 200m);
    }

    [Fact]
    public async Task TotalAmountScalar_WorksCorrectly()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var repository = new OrderRepository(context);

        await repository.CreateOrderAsync(new Order { OrderDate = DateTime.Now, Amount = 100m });
        await repository.CreateOrderAsync(new Order { OrderDate = DateTime.Now, Amount = 250m });

        // Act
        var total = await repository.GetOrdersAsync();
        var sum = total.Sum(o => o.Amount);

        // Assert
        Assert.Equal(350m, sum);
    }
}

