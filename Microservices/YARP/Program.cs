var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var useYarp = builder.Configuration["GatewayType"] == "Yarp";

if (useYarp)
{
    builder.Services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
}

var app = builder.Build();

if (useYarp)
{
    app.MapReverseProxy();
}

app.Run();
