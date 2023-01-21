using Microsoft.AspNetCore.Authorization;
using MVCBasic.WebUI.ServicesConfiguration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterCookies();
builder.Services.RegisterDateOfBirthPolicy();

builder.Services.AddControllersWithViews(config =>
{
    var defaultAuthBuilder = new AuthorizationPolicyBuilder();
    var defaultAuthPolicy = defaultAuthBuilder
        .RequireAuthenticatedUser()
        .Build();

    // The defaultAuthPolicy will be automatically used in every action unless it has [AllowAnonimous] 
    //config.Filters.Add(new AuthorizeFilter(defaultAuthPolicy));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
