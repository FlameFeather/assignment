using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
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
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "assignment", Version = "v1" });
			});
			services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(option =>
			{
				option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuer = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = "demo_issuer",
					ValidAudience = "demo_audience",
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dfs4af5gfdsghgfhdfghdsfgsdf6dsa6f")),
					ClockSkew = TimeSpan.Zero
				};
			});
			services.AddCors(options => {
				options.AddPolicy("AllowOrigin", policy => {
					//policy.AllowAnyOrigin()//允许所有站点跨域请求
					policy.SetIsOriginAllowed(_ => true)// 允许部分站点跨域请求.WithOrigins("https://localhost:15580")
					.AllowAnyMethod() // 允许所有请求方法
					.AllowAnyHeader() // 允许所有请求头
					.AllowCredentials(); // 允许Cookie信息
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "assignment v1"));
			}
			app.UseRouting();
			app.UseCors("AllowOrigin");
			app.UseAuthentication();

			app.UseAuthorization();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
