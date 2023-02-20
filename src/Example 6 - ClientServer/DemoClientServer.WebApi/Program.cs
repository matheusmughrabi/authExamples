using DemoClientServer.WebApi.Constants;
using DemoClientServer.WebApi.Policies;
using DemoClientServer.WebApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


var tokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuer = true,
    ValidIssuer = TokenConstants.Issuer,

    ValidateAudience = true,
    ValidAudience = TokenConstants.Audience,

    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstants.Secret)),

    RequireExpirationTime = true,
    ValidateLifetime = true,

    ClockSkew = TimeSpan.Zero
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = tokenValidationParameters;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(PolicyConstants.ProductCreate, options => options.RequireClaim(ClaimConstants.ProductClaim, ClaimConstants.CreateClaimValue));
    options.AddPolicy(PolicyConstants.ProductOwnerOnly, policy => policy.Requirements.Add(new ProductOwnerOnlyRequirement()));
});

builder.Services.AddSingleton<IAuthorizationHandler, ProductOwnerOnlyHandler>();

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
