using SupplyChainManagement.src.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ProjectSetup(builder.Configuration);

builder.BuildAndConfigureRequestPipeline().Run();