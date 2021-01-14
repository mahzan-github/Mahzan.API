FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY ./*.sln ./
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

RUN dotnet restore "./src/Mahzan.Models/Mahzan.Models.csproj"
RUN dotnet restore "./src/Mahzan.Dapper/Mahzan.Dapper.csproj"
RUN dotnet restore "./src/Mahzan.Business/Mahzan.Business.csproj"
RUN dotnet restore "./src/Mahzan.API/Mahzan.API.csproj"

# copy everything else and build app
COPY src ./src
RUN dotnet publish "./src/Mahzan.API/Mahzan.API.csproj" -c Release -o "../../dist"

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
WORKDIR /app
COPY --from=build-env /dist .

ENV ASPNETCORE_URLS="http://*:5000"
ENV ASPNETCORE_ENVIRONMENT="Development"

EXPOSE 5000

ENTRYPOINT ["dotnet", "Mahzan.API.dll"]
