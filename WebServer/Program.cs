using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.IO;

public class Startup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStaticFiles();

        app.UseDefaultFiles();

        var fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory() + "/WebRoot"));
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = fileProvider
        });

        app.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            FileProvider = fileProvider,
            RequestPath = new PathString("/").Value.TrimEnd('/')
        });
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var host = new WebHostBuilder()
            .UseKestrel()
            .UseStartup<Startup>()
            .Build();

        host.Run();
    }
}
