using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpReports.Web.DataAccessors;
using HttpReports.Web.DataContext;
using HttpReports.Web.Filters;
using HttpReports.Web.Models;
using HttpReports.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpReports.Web
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
            services.Configure<CookiePolicyOptions>(options =>
            { 
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });  
           

            DependencyInjection(services);   
            

            services.AddMvc(x => { 
                // ȫ�ֹ�����
                x.Filters.Add<GlobalAuthorizeFilter>();
                x.Filters.Add<GlobalExceptionFilter>();

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 

        }

         
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        { 
            app.UseStaticFiles();
            app.UseCookiePolicy(); 
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void DependencyInjection(IServiceCollection services)
        {
            services.AddSingleton<HttpReportsConfig>();  
            services.AddTransient<DBFactory>();

            services.AddTransient<DataService>();

            // ��ʼ�����ݿ��
            services.BuildServiceProvider().GetService<DBFactory>().InitDB();

            // ע�����ݿ������
            RegisterDBService(services);  

        }

        private void RegisterDBService(IServiceCollection services)
        {
            string dbType = Configuration["HttpReportsConfig:DBType"]; 

            if (dbType.ToLower() == "sqlserver")
            {
                services.AddScoped<IDataAccessor, DataAccessorSqlServer>();
            }  
            else if (dbType.ToLower() == "mysql")
            {
                services.AddScoped<IDataAccessor,DataAccessorMySql>(); 
            }
            else
            {
                throw new Exception("���ݿ����ô���"); 
            }  
        }   
    }
}
