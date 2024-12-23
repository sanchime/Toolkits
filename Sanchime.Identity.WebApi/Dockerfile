# 请参阅 https://aka.ms/customizecontainer 以了解如何自定义调试容器，以及 Visual Studio 如何使用此 Dockerfile 生成映像以更快地进行调试。

# 此阶段用于在快速模式(默认为调试配置)下从 VS 运行时
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# 此阶段用于生成服务项目
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sanchime.Identity.WebApi/Sanchime.Identity.WebApi.csproj", "Sanchime.Identity.WebApi/"]
COPY ["Sanchime.AspNetCore/Sanchime.AspNetCore.csproj", "Sanchime.AspNetCore/"]
COPY ["Sanchime.Common/Sanchime.Common.csproj", "Sanchime.Common/"]
COPY ["Sanchime.DependencyInjection/Sanchime.DependencyInjection.csproj", "Sanchime.DependencyInjection/"]
COPY ["Sanchime.EntityFrameworkCore/Sanchime.EntityFrameworkCore.csproj", "Sanchime.EntityFrameworkCore/"]
COPY ["Sanchime.DynamicQueryable/Sanchime.DynamicQueryable.csproj", "Sanchime.DynamicQueryable/"]
COPY ["Sanchime.EventFlows/Sanchime.EventFlows.csproj", "Sanchime.EventFlows/"]
COPY ["Sanchime.Identity/Sanchime.Identity.csproj", "Sanchime.Identity/"]
RUN dotnet restore "./Sanchime.Identity.WebApi/Sanchime.Identity.WebApi.csproj"
COPY . .
WORKDIR "/src/Sanchime.Identity.WebApi"
RUN dotnet build "./Sanchime.Identity.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 此阶段用于发布要复制到最终阶段的服务项目
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sanchime.Identity.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 此阶段在生产中使用，或在常规模式下从 VS 运行时使用(在不使用调试配置时为默认值)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sanchime.Identity.WebApi.dll"]