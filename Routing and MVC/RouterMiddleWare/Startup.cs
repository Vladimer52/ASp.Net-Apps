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
            // ���������� ���������� ��������
            var myRouterHandler = new RouteHandler(Handle);
            // ������� �������, ��������� ����������
            var routeBuilder = new RouteBuilder(app, myRouterHandler);
            // ���� ����������� �������� - �� ������ ��������������� ������� {controller}/{action}/{id?}, id �� ������������ ��������
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
