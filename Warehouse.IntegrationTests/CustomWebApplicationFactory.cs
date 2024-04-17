using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MsSql;
using Warehouse.Persistence.EF.Persistence.Contexts;

namespace Warehouse.IntegrationTests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build();
    private readonly string _connectionString;

    public CustomWebApplicationFactory()
    {
        //_dbContainer = new MsSqlBuilder()
        //    .WithPassword("$trongPassword")
        //    .WithPortBinding(1433, true)
        //    .WithEnvironment("ACCEPT_EULA", "Y")
        //    .WithEnvironment("MSSQL_SA_PASSWORD", "$trongPassword")
        //    .Build();

        _dbContainer.StartAsync().Wait();

        //var host = _dbContainer.Hostname;
        //var port = _dbContainer.GetMappedPublicPort(1433);
        //_connectionString = $"Server={host},{port};Database=master;User Id=sa;Password=$trongPassword;TrustServerCertificate=True";
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
