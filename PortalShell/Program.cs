using GoC.WebTemplate.Components.Core.Services;
using Microsoft.EntityFrameworkCore;
using PortalData.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Required by the Government of Canada Web Template.
// ModelAccessor provides the template model used by controllers and views.
builder.Services.AddModelAccessor();

// Configures the English/French request localization used by the template.
builder.Services.ConfigureGoCTemplateRequestLocalization();

// Add SQLite database support for simple portal data such as
// database-driven links used in the proof of concept.
builder.Services.AddDbContext<PortalShellContext>(options =>
    options.UseSqlite(
        "Data Source=portalshell.db",
        sqliteOptions =>
            sqliteOptions.MigrationsAssembly("PortalShell")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseRequestLocalization();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();