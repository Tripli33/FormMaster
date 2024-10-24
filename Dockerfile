FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app 
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FormMaster.WEB/FormMaster.WEB.csproj", "FormMaster.WEB/"]
RUN dotnet restore "FormMaster.WEB/FormMaster.WEB.csproj"
COPY . . 
WORKDIR "/src/FormMaster.WEB"
RUN dotnet build "./FormMaster.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FormMaster.WEB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FormMaster.WEB.dll"]