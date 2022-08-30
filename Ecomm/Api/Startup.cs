using Api.Core;
using Api.Core.Requests;
using Application;
using Application.Commands.Brand;
using Application.Commands.Cart;
using Application.Commands.Category;
using Application.Commands.Order;
using Application.Commands.Photo;
using Application.Commands.Product;
using Application.Commands.User;
using Application.Queries.Brand;
using Application.Queries.Cart;
using Application.Queries.Category;
using Application.Queries.Log;
using Application.Queries.Order;
using Application.Queries.Photo;
using Application.Queries.Product;
using Application.Queries.User;
using Application.Searches;
using DataAccess;
using Implementation.Commands.Brand;
using Implementation.Commands.Cart;
using Implementation.Commands.Category;
using Implementation.Commands.Order;
using Implementation.Commands.Orders;
using Implementation.Commands.Product;
using Implementation.Commands.User;
using Implementation.Logging;
using Implementation.Queries.Brand;
using Implementation.Queries.Cart;
using Implementation.Queries.Category;
using Implementation.Queries.Log;
using Implementation.Queries.Order;
using Implementation.Queries.Photo;
using Implementation.Queries.Product;
using Implementation.Queries.User;
using Implementation.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
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
            // QUERY
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetSingleUserQuery, EfGetSingleUserQuery>();
            services.AddTransient<ISearchBrandsQuery, EfSearchBrandsQuery>();
            services.AddTransient<IGetSingleBrandQuery, EfGetSingleBrandQuery>(); 
            services.AddTransient<IGetOrderQuery, EfGetOrderQuery>();
            services.AddTransient<IGetCartQuery, EfGetCartQuery>();
            services.AddTransient<ISearchPhotosQuery, EfSearchPhotosQuery>();
            services.AddTransient<IGetSinglePhotoQuery, EfGetSinglePhotoQuery>(); 
            services.AddTransient<ISearchProductsQuery, EfSearchProductsQuery>();
            services.AddTransient<IGetSingleProductQuery, EfGetSingleProductQuery>();
            services.AddTransient<ISearchCategoriesQuery, EfSearchCategoriesQuery>();
            services.AddTransient<IGetCategoryQuery, EfGetSingleCategoryQuery>();
            services.AddTransient<ISearchLogsQuery, EfGetLogQuery>(); 

            // COMMAND
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            services.AddTransient<ICreateBrandCommand, EfInsertBrandCommand>();
            services.AddTransient<IRemoveBrandCommand, EfRemoveBrandCommand>();
            services.AddTransient<IUpdateBrandCommand, EfUpdateBrandCommand>();
            services.AddTransient<IAddCartItemCommand, EfAddCartItemCommand>();
            services.AddTransient<IRemoveCartItemCommand, EfRemoveCartItemCommand>();
            services.AddTransient<IAddOrderItemCommand, EfCreateOrderCommand>();
            services.AddTransient<IUpdateOrderCommand, EfUpdateOrderCommand>();
            services.AddTransient<IRemoveOrderCommand, EfRemoveOrderCommand>(); 
            services.AddTransient<ICreatePhotoCommand, EfInsertPhotoCommand>();
            services.AddTransient<IRemovePhotoCommand, EfRemovePhotoCommand>();
            services.AddTransient<ICreateProductCommand, EfInsertProductCommand>(); 
            services.AddTransient<IRemoveProductCommand, EfRemoveProductCommand>();
            services.AddTransient<IUpdateProductCommand, EfUpdateProductCommand>();
            services.AddTransient<ICreateCategoryCommand, EfInsertCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
            services.AddTransient<IRemoveCategoryCommand, EfRemoveCategoryCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
             
            // VALIDATOR
            services.AddTransient<UserEntityValidator>();
            services.AddTransient<BrandEntityValidator>();
            services.AddTransient<PhotoEntityValidator>();
            services.AddTransient<ProductEntityValidator>();
            services.AddTransient<CategoryEntityValidator>();

            //Request
            services.AddTransient<LoginRequest>();

            // SEARCH
            services.AddTransient<UserSearch>();
            services.AddTransient<LogSearch>();

            // LOG
            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();

            // MISC
            services.AddTransient<UseCaseExecutor>();
             
            // DB
            services.AddTransient<Context>();

            // AUTH
            services.AddJwt();
            services.AddHttpContextAccessor();
            services.AddUsesCases();
            services.AddApplicationActor();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });

            app.UseRouting();
            app.UseStaticFiles();
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
