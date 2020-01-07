using System;
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using HttpReports.Web.DataAccessors;
using HttpReports.Web.DataContext;
using HttpReports.Web.Implements;
using HttpReports.Web.Job;
using HttpReports.Web.Models;
using HttpReports.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HttpReportsWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

     
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjection(services);

            services.AddControllersWithViews();
        }

        private void DependencyInjection(IServiceCollection services)
        {
            services.AddSingleton<HttpReportsConfig>();
            services.AddSingleton<JobService>();
            services.AddSingleton<ScheduleService>();

            services.AddTransient<DBFactory>();

            services.AddTransient<DataService>();

            // ע�����ݿ������
            RegisterDBService(services);


            // ��ʼ��ϵͳ����
            InitWebService(services);
        }

        private void InitWebService(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            ServiceContainer.provider = provider;

            // ��ʼ�����ݿ��
            provider.GetService<DBFactory>().InitDB();

            // ������̨����
            provider.GetService<JobService>().Start();

        }

        private void RegisterDBService(IServiceCollection services)
        {
            string dbType = Configuration["HttpReportsConfig:DBType"];

            if (dbType.ToLower() == "sqlserver")
            {
                services.AddTransient<IDataAccessor, DataAccessorSqlServer>();
            }
            else if (dbType.ToLower() == "mysql")
            {
                services.AddTransient<IDataAccessor, DataAccessorMySql>();
            }
            else
            {
                throw new Exception("���ݿ����ô���");
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
