var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(config =>
{
    // Check the cookie to confirm that we are authenticated
    config.DefaultAuthenticateScheme = "ClientCookie";

    // When we sign in we will provide a cookie
    config.DefaultSignInScheme = "ClientCookie";

    // When we hit an authorize endpoint, the addOAuth flow will be used
    config.DefaultChallengeScheme = "OurServer";
})
    .AddCookie("ClientCookie")
    .AddOAuth("OurServer", config =>
    {
        config.CallbackPath = "/oauth/callback";
        config.ClientId = "client_id_mock";
        config.ClientSecret = "client_secret_mock";
        config.AuthorizationEndpoint = "https://localhost:7040/oauth/authorize";
        config.TokenEndpoint = "https://localhost:7040/oauth/token";
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
