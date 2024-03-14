#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LookUp.GraphQL/LookUp.GraphQL.csproj", "LookUp.GraphQL/"]
COPY ["Framework.Kafka/Framework.Kafka.csproj", "Framework.Kafka/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["LookUp.Domain/LookUp.Domain.csproj", "LookUp.Domain/"]
COPY ["Framework.Auth/Framework.Auth.csproj", "Framework.Auth/"]
RUN dotnet restore "LookUp.GraphQL/LookUp.GraphQL.csproj"
COPY . .
WORKDIR "/src/LookUp.GraphQL"
RUN dotnet build "LookUp.GraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LookUp.GraphQL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LookUp.GraphQL.dll"]