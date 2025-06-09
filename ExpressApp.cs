using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ExpressSharp;

public class ExpressApp
{
    private readonly WebApplication _app;

    public ExpressApp(string[]? args = null)
    {
        var builder = WebApplication.CreateBuilder(args ?? Array.Empty<string>());
        _app = builder.Build();
    }

    public ExpressApp Use(Func<HttpContext, Func<Task>, Task> middleware)
    {
        _app.Use(async (ctx, next) =>
        {
            await middleware(ctx, next);
        });
        return this;
    }

    public ExpressApp Get(string path, RequestDelegate handler)
    {
        _app.MapGet(path, handler);
        return this;
    }

    public ExpressApp Post(string path, RequestDelegate handler)
    {
        _app.MapPost(path, handler);
        return this;
    }

    public ExpressApp Put(string path, RequestDelegate handler)
    {
        _app.MapPut(path, handler);
        return this;
    }

    public ExpressApp Delete(string path, RequestDelegate handler)
    {
        _app.MapDelete(path, handler);
        return this;
    }

    public Task StartAsync(string? url = null)
    {
        if (url != null)
        {
            _app.Urls.Add(url);
        }

        return _app.RunAsync();
    }
}
