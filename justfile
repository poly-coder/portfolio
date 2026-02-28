# specify powershell as the shell to execute recipes
set shell := ["pwsh.exe", "-NoProfile", "-c"]

# default command
default: help

# Show available commands
help:
	@just --list

# Install all dependencies (frontend, workers, dotnet restore)
install:
	@echo "Installing Node dependencies..."
	pnpm install
	@echo "Restoring .NET dependencies..."
	dotnet restore src/backend/Backend.slnx
	dotnet restore src/aspire/AppHost.csproj

# Run the local development environment via .NET Aspire
dev:
	@echo "Starting development environment..."
	dotnet run --project src/aspire/AppHost.csproj

# Build all projects
build:
	@echo "Building frontend workspace..."
	cd src/frontend && pnpm run build
	@echo "Building backend..."
	dotnet build src/backend/Backend.slnx
	dotnet build src/aspire/AppHost.csproj