# specify powershell as the shell to execute recipes
set shell := ["pwsh.exe", "-NoProfile", "-c"]

# default command
default: help

# Show available commands
help:
	@just --list

# Install all dependencies (frontend, workers, dotnet restore)
install:
	pnpm install
	dotnet restore .\src\backend\backend.slnx

# Trust local HTTPS development certificate (manual/opt-in)
trust-dev-certs:
	dotnet dev-certs https --trust

# Run the local development environment via .NET Aspire
dev:
	cd src && aspire run --project ./apphost.cs

# Build all projects
build:
	@Write-Host "Error: build target not yet implemented - frontend/backend scaffolding in progress" -ForegroundColor Red
	exit 1
