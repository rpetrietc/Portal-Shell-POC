using GoC.WebTemplate.Components.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services.
builder.Services.AddControllersWithViews();

// Required by the Government of Canada Web Template.
// ModelAccessor provides the template model used by controllers and views.
builder.Services.AddModelAccessor();

// Configures English/French request localization used by the template.
builder.Services.ConfigureGoCTemplateRequestLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Enable the request localization configured above.
app.UseRequestLocalization();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();