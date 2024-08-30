var builder = WebApplication.CreateBuilder(args);

builder.PrimitiveConfigure();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddEventFlow(option =>
{
    option.RegisterAssemblies(typeof(IdentityContext).Assembly);

    option.AddPipeline(typeof(EventFlowLoggingPipeline<,>));
    option.AddPipeline(typeof(EventFlowValidationPipeline<,>));
});
builder.Services.AddDbContextPool<IdentityContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.Configure<IdentityConfiguration>(builder.Configuration.GetSection(nameof(IdentityConfiguration)));
builder.Services.AddOptions<IdentityConfiguration>();

builder.Services.AddSingleton<IMapper, Mapper>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            //.WithOrigins("http://localhost:5173/", "http://127.0.0.1:5173/")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            ;
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false, //�Ƿ���֤Issuer
        ValidateAudience = false, //�Ƿ���֤Audience
        ValidateIssuerSigningKey = true, //�Ƿ���֤SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["IdentityConfiguration:Secret"]!)), //SecurityKey
        ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
        ClockSkew = TimeSpan.FromSeconds(30), //����ʱ���ݴ�ֵ�������������ʱ�䲻ͬ�����⣨�룩
        RequireExpirationTime = true,
    };
});

builder.Services.AddAuthorization();

// ��α
builder.Services.AddAntiforgery();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestQuery 
        | HttpLoggingFields.RequestScheme 
        | HttpLoggingFields.RequestPath
        | HttpLoggingFields.RequestMethod
        | HttpLoggingFields.RequestProtocol
        | HttpLoggingFields.ResponseStatusCode
        | HttpLoggingFields.Duration;
    options.CombineLogs = true;
});


var app = builder.Build();

//app.UseRouting();
app.UseCors();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();
app.UsePrimitiveConfigure();
app.UseHttpLogging();

await app.Services.MigrateDatabaseAsync<IdentityContext, IdentityDataSeeder>();

app.UseMiniApi()
    .RequireAuthorization()
    .MapGroup("v1")
#if DEBUG
    .AllowAnonymous()
#endif
    .AddIdentityGroup();


app.Run();