#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GetProduct/GetProductServices.csproj", "GetProduct/"]
COPY ["Framework.Kafka/Framework.Kafka.csproj", "Framework.Kafka/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Product.Domain/Product.Domain.csproj", "Product.Domain/"]
RUN dotnet restore "GetProduct/GetProductServices.csproj"
COPY . .
WORKDIR "/src/GetProduct"
RUN dotnet build "GetProductServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GetProductServices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GetProductServices.dll"]