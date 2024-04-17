using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.IntegrationTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();

    public CustomWebApplicationFactory()
    {
        _dbContainer.StartAsync().Wait();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(WarehouseDbContext));
            services.AddDbContext<WarehouseDbContext>(options =>
                options.UseSqlServer(_dbContainer.GetConnectionString()));
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _dbContainer.DisposeAsync();
        }
    }
}
