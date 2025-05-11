using Microsoft.EntityFrameworkCore;
using OrderService.src.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.src.Infrastructure;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<SagaState> SagaStates => Set<SagaState>();
}

public class SagaState
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string CurrentStep { get; set; } = "Start";
    public string? FailureReason { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}