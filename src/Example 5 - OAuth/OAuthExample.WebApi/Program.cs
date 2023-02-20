using Microsoft.IdentityModel.Tokens;
using System.Text;
using OAuthExample.WebApi.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("OAuth")
    .AddJwtBearer("OAuth", config =>
    {
        var secretEncoded = Encoding.UTF8.GetBytes(TokenConstants.Secret);
        var securityKey = new SymmetricSecurityKey(secretEncoded);

        config.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = TokenConstants.Issuer,
            ValidAudience = "https://localhost:7041",
            IssuerSigningKey = securityKey
        };
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
