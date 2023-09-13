using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMS.Web.Extensions;
using PMS.Web.Middlewares;
using PMS.Web.Services;
using PMS.WEB.BAL.Services.Derived;
using PMS.WEB.BAL.Services.Interfaces;

namespace PMS.Web
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
            services.ConfigureControllers();

            services.ConfigureJwt(Configuration);
            services.ConfigureCors();

            services.AddSignalR();
            services.ConfigureSqlServer(Configuration);

            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IInboxService, InboxService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IPatientAllergyService, PatientAllergyService>();
            services.AddTransient<IVisitService, VisitService>();
            services.AddTransient<IHospitalUserService, HospitalUserService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPatientSchedulingService, PatientSchedulingService>();
            services.AddTransient<INotification, NotificationsService>();
            //Set up Automapper config
            services.AddAutoMapper(typeof(Startup));

            services.ConfigureIdentity();

            services.ConfigureJwtAuth(Configuration);

            services.ConfigureSwagger();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PMS.Web v1"));
            }
            app.UseCors("ApiCorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
