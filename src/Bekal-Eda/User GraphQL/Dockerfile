#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["User GraphQL/User.GraphQL.csproj", "User GraphQL/"]
COPY ["UserDomain/User.Domain.csproj", "UserDomain/"]
COPY ["Framework.Auth/Framework.Auth.csproj", "Framework.Auth/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
RUN dotnet restore "User GraphQL/User.GraphQL.csproj"
COPY . .
WORKDIR "/src/User GraphQL"
RUN dotnet build "User.GraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.GraphQL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.GraphQL.dll"]