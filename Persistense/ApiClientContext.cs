using Domain.Entities.SU;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistense;

public class ApiClientContext : DbContext
{
    private readonly string _connectionString;
    public ApiClientContext(string connectionString) => _connectionString = connectionString;
    public DbSet<ApiClient> SuApiClient { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_connectionString).LogTo(Console.WriteLine);
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<ApiClient>().HasKey(b => b.Id);
}
