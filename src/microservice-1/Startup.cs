using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace microservice_1
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
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGet("/", async context =>
        {
          await context.Response.WriteAsync("Hello World!");
        });

        endpoints.MapGet("/send", async context =>
        {
          var DaprBaseUrl = $"http://localhost:{Environment.GetEnvironmentVariable("DAPR_HTTP_PORT")}/v1.0";

          var payload = new
          {
            id = 1,
            date = new DateTime(),
            message = Guid.NewGuid().ToString("N")
          };

          var result = await client.PostAsync(
            $"{DaprBaseUrl}/publish/order",
            new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
          );

          await context.Response.WriteAsync(result.StatusCode.ToString());
        });

        endpoints.MapGet("/invoke", async context =>
        {
          var payload = new
          {
            id = 1,
            date = new DateTime(),
            message = Guid.NewGuid().ToString("N")
          };
        });
      });
    }
  }
}
