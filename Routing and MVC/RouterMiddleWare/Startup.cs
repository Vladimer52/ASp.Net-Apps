using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RouterMiddleWare
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // определ€ем обработчик маршрута
            var myRouterHandler = new RouteHandler(Handle);
            // создаем маршрут, использу€ обработчик
            var routeBuilder = new RouteBuilder(app, myRouterHandler);
            // само определение маршрута - он должен соответствовать запросу {controller}/{action}/{id?}, id не об€зательный параметр
            routeBuilder.MapRoute("default", "store/{action}/{Id?}");

            app.UseRouter(routeBuilder.Build());

            app.Run(async context =>
            {
                await context.Response.WriteAsync("hello@");
            });


        }
        private async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("Hello ASP.NET");
        }
    }


}
