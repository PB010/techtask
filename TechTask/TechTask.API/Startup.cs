﻿using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using TechTask.Application.Filters.Email;
using TechTask.Application.Filters.Validators.CommentValidator;
using TechTask.Application.Filters.Validators.DbLogValidator;
using TechTask.Application.Filters.Validators.GeneralValidator;
using TechTask.Application.Filters.Validators.LogValidator;
using TechTask.Application.Filters.Validators.TaskValidator;
using TechTask.Application.Filters.Validators.UserValidator;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Commands;
using TechTask.Application.Users.Mapping;
using TechTask.Infrastructure.Authentication;
using TechTask.Infrastructure.Services;
using TechTask.Persistence.Context;
using TechTask.Persistence.Helper;

namespace TechTask.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                //.ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserForRegistrationValidation>());

            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration["ConnectionString"],
                b => b.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly
                    .GetName().Name)));
                
            services.Configure<TokenManagement>(Configuration.GetSection("TokenManagement"));
            var token = Configuration.GetSection("TokenManagement").Get<TokenManagement>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<ITokenAuthenticationService, TokenAuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITasksService, TasksService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDbLogService, DbLogService>();

            services.AddScoped<ValidateRouteAttributes>();
            services.AddScoped<ValidateTaskForCreationDto>();
            services.AddScoped<ValidateAssignToUserCommand>();
            services.AddScoped<ValidateRemoveUserFromTaskCommand>();
            services.AddScoped<ValidateApproveTaskCompletion>();
            services.AddScoped<ValidateReopenTask>();
            services.AddScoped<ValidateTaskForUpdate>();
            services.AddScoped<ValidateAddLogToTask>();
            services.AddScoped<ValidateUserForUpdate>();
            services.AddScoped<EmailSenderService>();
            services.AddScoped<ValidateAddComment>();
            services.AddScoped<ValidateGetDbLog>();

            services.AddHttpContextAccessor();
            services.AddMediatR(typeof(RegisterUserCommand));
            services.AddAutoMapper(typeof(UserMappingProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DbSeeder.Seed(context);
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
