using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Sanchime.Common.Snowflakes;
using Sanchime.DependencyInjection;
using System.IO.Compression;

namespace Sanchime.AspNetCore;

public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// 基本配置项
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder PrimitiveConfigure(this WebApplicationBuilder builder)
    {
        builder.Host.UseServiceProviderFactory(new AutowiredServiceProviderFactory());
        builder.Services.EnableAutomationInjection();

        builder.Services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        });

        builder.Services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.SmallestSize;
        });
        builder.Services.AddSingleton<ISnowflake>(_ => new Snowflake(new SnowflakeOptions()));

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddExceptionHandler<ExceptionHandler>();

        return builder;
    }
}
