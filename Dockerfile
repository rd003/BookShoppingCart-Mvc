FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY /*.sln .
COPY BookShoppingCartMvcUI/*.csproj ./BookShoppingCartMvcUI/
RUN dotnet restore

# copy everything else and build app
COPY BookShoppingCartMvcUI/. ./BookShoppingCartMvcUI/
WORKDIR /source/BookShoppingCartMvcUI
RUN dotnet publish -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "BookShoppingCartMvcUI.dll"]
