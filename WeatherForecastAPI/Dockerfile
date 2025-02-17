# Use a imagem do SDK do .NET para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copie os arquivos do projeto e restaure as dependências
COPY . .
RUN dotnet restore

# Compile o projeto
RUN dotnet publish -c Release -o out

# Use a imagem do runtime do .NET para rodar o projeto
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Exponha a porta que a aplicação irá rodar
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "WeatherForecastAPI.dll"]