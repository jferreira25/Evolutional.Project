using AutoMapper;
using Evolutional.Project.Admin.Filter;
using Evolutional.Project.Admin.Middlewares;
using Evolutional.Project.Domain.Commands.Lessons.Create;
using Evolutional.Project.Domain.Commands.Students.Create;
using Evolutional.Project.Domain.Interfaces;
using Evolutional.Project.Domain.Interfaces.Tools;
using Evolutional.Project.Domain.Services.Xlsx;
using Evolutional.Project.Domain.Tools;
using Evolutional.Project.Infrastructure.Data.Repository.Lesson;
using Evolutional.Project.Infrastructure.Data.Repository.Students;
using Evolutional.Project.Infrastructure.Data.Repository.StudentsLessons;
using Evolutional.Project.Infrastructure.Data.Repository.Users;
using Evolutional.Project.Infrastructure.Service.ServiceHandler;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Collections.Generic;
using System.Reflection;

namespace Evolutional.Project
{
    public class Startup
    {
        public Container DependencyInjectionContainer { get; } = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            DependencyInjectionContainer.Options.DefaultLifestyle = Lifestyle.Scoped;
            DependencyInjectionContainer.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.UseSimpleInjectorAspNetRequestScoping(DependencyInjectionContainer);

            services.AddMediatR(typeof(CreateLessonsCommand).GetTypeInfo().Assembly, typeof(CreateStudentsCommand).GetTypeInfo().Assembly);
            services.AddScoped(typeof(IJwtTokenGenerator), typeof(JwtTokenGenerator));
            services.AddScoped(typeof(ISheetsService), typeof(SheetsService));

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(ILessonsRepository), typeof(LessonsRepository));
            services.AddScoped(typeof(IStudentsRepository), typeof(StudentsRepository));
            services.AddScoped(typeof(IStudentsLessonsProcedure), typeof(StudentsLessonsProcedure));
            

            services.AddAutoMapper();

            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example \"Authorization: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer" }
                         }, new List<string>() }
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Evolutional Project API",
                    Description = "Api para avaliação técnica"
                });
                c.OperationFilter<SwaggerOperationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Evolutional Project Admin V1");
            });

            app.UseRouting();
            app.UseAuthenticationMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
