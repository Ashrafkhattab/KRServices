
using BL.Helpers;
using BL.Shared;
using DL.Data;
using KRSServices.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace KRSServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region swager Auth
     //       builder.Services.AddSwaggerGen(c =>
     //       {
     //           c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo", Version = "v1" });
     //       });
     //       builder.Services.AddSwaggerGen(swagger =>
     //       {
     //           //This�is�to�generate�the�Default�UI�of�Swagger�Documentation����
     //           swagger.SwaggerDoc("v2", new OpenApiInfo
     //           {
     //               Version = "v1",
     //               Title = "ASP.NET�5�Web�API",
     //               Description = " ITI Projrcy"
     //           });

     //           //�To�Enable�authorization�using�Swagger�(JWT)����
     //           swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
     //           {
     //               Name = "Authorization",
     //               Type = SecuritySchemeType.ApiKey,
     //               Scheme = "Bearer",
     //               BearerFormat = "JWT",
     //               In = ParameterLocation.Header,
     //               Description = "Enter�'Bearer'�[space]�and�then�your�valid�token�in�the�text�input�below.\r\n\r\nExample:�\"Bearer�eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
     //           });
     //           swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
     //{
     //    {
     //    new OpenApiSecurityScheme
     //    {
     //    Reference = new OpenApiReference
     //    {
     //    Type = ReferenceType.SecurityScheme,
     //    Id = "Bearer"
     //    }
     //    },
     //    new string[] {}
     //    }
     //});
     //       });

            #endregion

            builder.Services.AddDbContext<KRSDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUniteOfWork, UniteOfWork>(); ;
            builder.Services.AddAutoMapper(m => m.AddProfile(new Mapping()));
            builder.Services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                          .Select(e => e.ErrorMessage).ToArray();

                    var apiValidationResponse = new ApiValidationResponse() { Errors = errors };

                    return new BadRequestObjectResult(apiValidationResponse);
                };

            });
            builder.Services.AddCors(corsoption => corsoption.AddPolicy("MyPolicy",
                corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod()
                .AllowAnyHeader().AllowAnyOrigin()));
            var app = builder.Build();
            app.UseCors("MyPolicy");
            app.UseRouting();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}