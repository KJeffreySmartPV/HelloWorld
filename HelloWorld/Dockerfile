#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["HelloWorld.csproj", "HelloWorld-linux/"]
RUN dotnet restore "HelloWorld-linux/HelloWorld.csproj"
COPY . ./HelloWorld-linux
WORKDIR "/src/HelloWorld-linux"
RUN dotnet build "HelloWorld.csproj" -c Release -o /app/build -r linux-x64

FROM build AS publish
RUN dotnet publish "HelloWorld.csproj" -c Release -o /app/publish -r linux-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloWorld.dll"]