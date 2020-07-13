using System;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CloudNative.CloudEvents;

namespace microservice_2
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

        endpoints.MapGet("/dapr/subscribe", async context =>
        {
          var subs = "[ { \"topic\": \"order\", \"route\": \"receive\" } ]";

          Console.WriteLine(subs);

          await context.Response.WriteAsync(subs);
        });

        endpoints.MapPost("/receive", async context =>
        {
          var cloudEvent = await context.Request.ReadCloudEventAsync();

          Console.WriteLine($"received: {JsonSerializer.Serialize(cloudEvent)}");

          await context.Response.WriteAsync("received");
        });

        endpoints.MapPost("/ringring", async context =>
        {
          var cloudEvent = await context.Request.ReadCloudEventAsync();

          Console.WriteLine($"ringring: {JsonSerializer.Serialize(cloudEvent)}");

          await context.Response.WriteAsync("ringring-ed");
        });
      });
    }
  }
}
