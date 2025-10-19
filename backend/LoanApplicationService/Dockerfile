FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY LoanApplicationService.WebApi/LoanApplicationService.WebApi.csproj ./LoanApplicationService.WebApi.csproj

RUN dotnet restore LoanApplicationService.WebApi.csproj

COPY LoanApplicationService.Domain/ ./LoanApplicationService.Domain/
COPY LoanApplicationService.Infrastructure/ ./LoanApplicationService.Infrastructure/
COPY LoanApplicationService.WebApi/ ./LoanApplicationService.WebApi/

WORKDIR /src/LoanApplicationService.WebApi
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LoanApplicationService.WebApi.dll"]
