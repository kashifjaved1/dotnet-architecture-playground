using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SupplyChainManagement.src.Core.Factories
{
    public class DbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext // Added this factory for tooling (migrations etc.)
    {
        public T CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("src/Web/appsettings.json")
                .Build();
            
            var connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<T>();
            optionsBuilder.UseSqlServer(connectionString);
            
            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options)!;
        }
    }
}
