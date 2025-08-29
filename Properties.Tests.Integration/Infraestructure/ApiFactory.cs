using System.Data.Common;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Properties.Api;
using Properties.Api.IServices;
using Properties.Core.Services;
using Properties.Domain;
using Properties.Domain.DTOs.Owner;
using Properties.Domain.Entities;
using Respawn;
using Testcontainers.SqlEdge;
using Xunit;

namespace Properties.Tests.Integration.Infraestructure
{
    public class ApiFactory : WebApplicationFactory<IApiProperties>, IAsyncLifetime
    {
        private SqlEdgeContainer _sqlEdgeDb = new SqlEdgeBuilder().WithPassword("HeabyHitPass1!")
        .Build();

        private DbConnection _dbConnection = default!;
        private Respawner _respawner = default!;

        public HttpClient HttpClient = default!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<PropertiesDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<PropertiesDbContext>(opts => opts.UseSqlServer(_sqlEdgeDb.GetConnectionString()));
            });
        }

        public async Task InitializeAsync()
        {
            await _sqlEdgeDb.StartAsync();
            HttpClient = CreateClient();
            await InitializeDbRespawner();
        }

        public async Task DisposeAsync()
        {
            await _sqlEdgeDb.StopAsync();
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }

        private async Task InitializeDbRespawner()
        {
            _dbConnection = new SqlConnection(_sqlEdgeDb.GetConnectionString());
            await _dbConnection.OpenAsync();
            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {
                DbAdapter = DbAdapter.SqlServer,
                SchemasToInclude = new[] { "dbo" }
            });
        }
    }
}
