FROM mcr.microsoft.com/dotnet/sdk:6.0

# Set environment variables
ENV ASPNETCORE_URLS="http://*:5000"
ENV ASPNETCORE_ENVIRONMENT="Production"

# Copy files to app directory
COPY . /app

# Set working directory
WORKDIR /app/tokengenerator

# Restore NuGet packages
RUN ["dotnet", "restore"]

# Build the app
RUN dotnet build -c Release -o /bin

# Open port
EXPOSE 5000/tcp

WORKDIR /bin

# Run the app
ENTRYPOINT ["dotnet", "tokengenerator.dll"]

