using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SKINET.App.Configuration;
using SKINET.App.Exceptions;
using SKINET.Business.Models.Identity;
using SKINET.Data.Context;
using SKINET.Data.Identity;
using SKINET.Data.SeedData;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AppIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Identityconnection")));
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);

    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddControllers();
builder.Services.ResolveDependecies();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddIdentityConfig(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();
/* app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Content")
    ),
    RequestPath = "/content"
}); */

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerDocumention();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync(); //apply migrations and create db if it does not exist
        await StoreContextSeed.SeedAsync(context, loggerFactory);

        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var identityContext = services.GetRequiredService<AppIdentityContext>();
        await identityContext.Database.MigrateAsync();
        await AppIdentityContextSeed.SeedUsersAsync(userManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred during the migration");
    }
}

await app.RunAsync();
