# specify powershell as the shell to execute recipes
set shell := ["pwsh.exe", "-NoProfile", "-c"]
set dotenv-filename := ".env.local"
set dotenv-load := true

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

# Ensure local environment file is present before running recipes that depend on Supabase config
load-local-env:
	@if (-not (Test-Path ".env.local")) { Write-Host "Error: .env.local not found. Copy .env.example to .env.local and set values first." -ForegroundColor Red; exit 1 }
	@Write-Host "Loaded .env.local" -ForegroundColor Green

# Run the local development environment via .NET Aspire
dev: load-local-env
	@cd src && aspire run --project ./apphost.cs

# Build all projects
build:
	@Write-Host "Error: build target not yet implemented - frontend/backend scaffolding in progress" -ForegroundColor Red
	exit 1
