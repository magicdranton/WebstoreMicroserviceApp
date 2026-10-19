using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// add services to container
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddRateLimiter(rateLimOpts =>
{
    rateLimOpts.AddFixedWindowLimiter("fixed", opts =>
    {
        opts.Window = TimeSpan.FromSeconds(10);
        opts.PermitLimit = 5;
    });
});

var app = builder.Build();

// configure request pipeline
app.UseRateLimiter();
app.MapReverseProxy();

app.Run();
