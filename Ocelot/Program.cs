var builder = WebApplication.CreateBuilder(args);

var useOcelot = builder.Configuration["GatewayType"] == "Ocelot";
if (useOcelot)
{
    //builder.Services.AddOcelot(builder.Configuration);
}

var app = builder.Build();

if (useOcelot) 
{
    //await app.UseOcelot();
}

app.Run();