FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY LoanApplicationService/LoanApplicationService.csproj ./LoanApplicationService.csproj

RUN mkdir LoanApplicationService

RUN dotnet restore LoanApplicationService.csproj

COPY LoanApplicationService/ ./LoanApplicationService/

WORKDIR /src/LoanApplicationService
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LoanApplicationService.dll"]
