#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Payment.GraphQL/Payment.GraphQL.csproj", "Payment.GraphQL/"]
COPY ["Payment.Domain/Payment.Domain.csproj", "Payment.Domain/"]
COPY ["Framework.Core/Framework.Core.csproj", "Framework.Core/"]
COPY ["Framework.Kafka/Framework.Kafka.csproj", "Framework.Kafka/"]
RUN dotnet restore "Payment.GraphQL/Payment.GraphQL.csproj"
COPY . .
WORKDIR "/src/Payment.GraphQL"
RUN dotnet build "Payment.GraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.GraphQL.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.GraphQL.dll"]