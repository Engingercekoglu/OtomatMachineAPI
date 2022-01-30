using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OtomatMachine.Bussiness.Repositories.Base;
using OtomatMachine.Bussiness.Repositories.PaymentType;
using OtomatMachine.Bussiness.Repositories.Product;
using OtomatMachine.Bussiness.Repositories.ProductType;
using OtomatMachine.Bussiness.Repositories.ReceiptTransaction;
using OtomatMachine.Bussiness.Services.PaymentType;
using OtomatMachine.Bussiness.Services.Product;
using OtomatMachine.Bussiness.Services.ProductType;
using OtomatMachine.Bussiness.Services.ReceiptTransaction;
using OtomatMachine.Entity.Context;
using OtomatMachine.Entity.SeedData;
using System;
using System.IO;
using System.Reflection;

namespace OtomatMachineAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddDbContext<DataContext>(options =>

                options.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetSection("Conn").Value)
            );
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
            services.AddScoped<IReceiptTransactionService, ReceiptTransactionService>();
            services.AddScoped<IReceiptTransactionRepository, ReceiptTransactionRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OtomatMachineAPI",
                    Version = "v1",
                    Description = "An API to perform OtomatMachine operations",
                    TermsOfService = new Uri("https://ode.al/")
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                new ProductTypeSeedData().Seed(app);
                new PaymentTypeSeedData().Seed(app);
                new ProductSeedData().Seed(app);

                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseSwagger();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OtomatMachineAPI v1");
                //c.InjectStylesheet("/Assests/css/custom-swagger-ui.css");


            });
        }
    }
}
