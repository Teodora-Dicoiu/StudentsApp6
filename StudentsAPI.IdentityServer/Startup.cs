using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentsAPI.Core.Entities;
using StudentsAPI.IdentityServer.Services;

namespace StudentsAPI.IdentityServer
{
    public class Startup
    {

        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStudentsAPIClients, StudentsAPIClients>();
           services.AddSingleton<IClientManagementService, ClientManagementService>();

            services.AddIdentityServer()
                .AddInMemoryApiResources(StudentsAPIResources.Get())
                .AddClientStore<StudentsAPIClientsStore>()
                .AddCertificateFromStore(configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.Authority = "https://localhost:5000";
                  options.Audience = StudentsAPIResource.APIName;
              });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireClaim("Scope", new string[] { StudentsAPIScopes.Admin }));

                options.AddPolicy("RegularUser", policy =>
                    policy.RequireClaim("Scope", new string[] { StudentsAPIScopes.Admin, StudentsAPIScopes.User }));

            });

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
