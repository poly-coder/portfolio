# specify powershell as the shell to execute recipes
set shell := ["pwsh.exe", "-NoProfile", "-c"]

# default command
default: help

# Show available commands
help:
	@just --list

# Install all dependencies (frontend, workers, dotnet restore)
install:
	dotnet dev-certs https --trust

# Run the local development environment via .NET Aspire
dev:
	dotnet run .\src\aspire.cs

# Build all projects
build:
	@echo "[placeholder] build: frontend/backend not scaffolded yet"