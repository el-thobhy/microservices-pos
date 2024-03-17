#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Store.GraphQL/Store.GraphQL.csproj", "Store.GraphQL/"]
COPY ["Framework.Kafka/Framework.Kafka.csproj", "Framework.Kafka/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Store.Domain/Store.Domain.csproj", "Store.Domain/"]
COPY ["Framework.Auth/Framework.Auth.csproj", "Framework.Auth/"]
RUN dotnet restore "Store.GraphQL/Store.GraphQL.csproj"
COPY . .
WORKDIR "/src/Store.GraphQL"
RUN dotnet build "Store.GraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Store.GraphQL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.GraphQL.dll"]