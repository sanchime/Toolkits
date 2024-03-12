using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sanchime.Identity;
using Sanchime.ProjectManager.WebApi.ApiGroups;
using System.Text;
using Sanchime.Identity.WebApi;
using Sanchime.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.PrimitiveConfigure();
builder.Services.AddEventFlow(option =>
{
    option.RegisterAssemblies(typeof(IdentityContext).Assembly);
});
builder.Services.AddDbContext<IdentityContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.Configure<IdentityConfiguration>(builder.Configuration.GetSection(nameof(IdentityConfiguration)));
builder.Services.AddOptions<IdentityConfiguration>();

builder.Services.AddAutoMapper(typeof(IdentityContext).Assembly);


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false, //是否验证Issuer
        ValidateAudience = false, //是否验证Audience
        ValidateIssuerSigningKey = true, //是否验证SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["IdentityConfiguration:Secret"]!)), //SecurityKey
        ValidateLifetime = true, //是否验证失效时间
        ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
        RequireExpirationTime = true,
    };
});

builder.Services.AddAuthorization();

// 防伪
builder.Services.AddAntiforgery();


var app = builder.Build();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();
app.UsePrimitiveConfigure();

await app.Services.MigrateDatabaseAsync<IdentityContext, IdentityDataSeeder>();

app.UseMiniApi()
    .RequireAuthorization()
    .MapGroup("v1")
#if DEBUG
    .AllowAnonymous()
#endif
    .AddIdentityGroup();

app.Run();