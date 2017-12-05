using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Darren.EF.Client.Logger;
using Microsoft.EntityFrameworkCore;
using Darren.EF.Data;

namespace Darren.EF.Client
{
    public class Startup
    {
        public Startup(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new EfLoggerProvider());
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = "Server=(localdb)\\MSSQLLocalDB;Database=EFCrud;Trusted_Connection=True;MultipleActiveResultSets=true";

            //使用工厂方式
            //services.AddScoped(factory => {
            //    var builder = new DbContextOptionsBuilder<Database>();
            //    builder.UseSqlServer(connection);
            //    return new Database(builder.Options);
            //});

            services.AddDbContext<Database>(options => options.UseSqlServer(connection).EnableSensitiveDataLogging());
            services.AddMvc();
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
        }
    }
}