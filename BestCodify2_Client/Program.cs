using BestCodify.Common;
using BestCodify2_Client.Service;
using BestCodify2_Client.Service.IServices;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BestCodify2_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(ResultConstant.BaseApiUrl) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ICourseService, CourseService>();
            await builder.Build().RunAsync();
        }
    }
}
