using GraphQL.API.Data;
using GraphQL.API.GraphQL;
using GraphQL.API.GraphQL.Messaging;
using GraphQL.API.Repositories;
using GraphQL.API.Repositories.Interface;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.API
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
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AppDb")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductReviewRepository, ProductReviewRepository>();

            services.AddScoped<IServiceProvider>(s => new FuncServiceProvider(
                s.GetRequiredService));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddScoped<GoodsSchema>();
            services.AddSingleton<ReviewMessageService>();

            services.AddGraphQL()
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddSystemTextJson()
                .AddDataLoader()
                .AddWebSockets();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,AppDbContext dbContext)
        {
            app.UseWebSockets();
            app.UseGraphQLWebSockets<GoodsSchema>("/graphql");
            app.UseGraphQL<GoodsSchema>();
            app.UseGraphQLPlayground(new Server.Ui.Playground.GraphQLPlaygroundOptions());
            dbContext.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
