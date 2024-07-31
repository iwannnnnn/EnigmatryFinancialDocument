using EnigmatryFinancialDocument.Application;
using EnigmatryFinancialDocument.Application.Handlers.Client.Query;
using EnigmatryFinancialDocument.Application.Handlers.FinancialDocument.Query;
using EnigmatryFinancialDocument.Application.Handlers.Tenant.Query;
using EnigmatryFinancialDocument.Core.Interfaces.Repositories;
using EnigmatryFinancialDocument.Infrastructure;
using EnigmatryFinancialDocument.Infrastructure.Repositories;
using EnigmatryFinancialDocument.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
namespace EnigmatryFinancialDocument.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            SetUpServiceCollection(builder.Services);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Financial Documents API",
                    Description = "An API to manage financial documents"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            SeedData(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Financial Documents API V1");
                c.RoutePrefix = string.Empty; // Serve Swagger UI at app's root
                c.DocumentTitle = "Financial Documents API Documentation"; // Customize the document title
                c.DefaultModelsExpandDepth(-1); // Hide the models section (optional)
                c.DisplayRequestDuration(); // Display request duration (optional)
            });
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var seeder = new DataSeeder(context);
            seeder.SeedData();
        }

        private static void SetUpServiceCollection(IServiceCollection services)
        {
            
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("FinancialDocument"));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IFinancialDocumentRepository, FinancialDocumentRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IFinancialDocumentService, FinancialDocumentService>();
            services.AddScoped<IAnonymizationService, AnonymizationService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetFinancialDocumentQueryHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllTenantsQueryHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllClientsQueryHandler).Assembly));

            services.AddAutoMapper(typeof(MappingProfile).Assembly); // Register AutoMapper with all profiles in the specified assembly


            services.AddControllers();
            services.AddTransient<DataSeeder>();
        }
    }
}