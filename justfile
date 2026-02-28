# specify powershell as the shell to execute recipes
set shell := ["pwsh.exe", "-NoProfile", "-c"]

# default command
default: help

# Show available commands
help:
	@just --list

# Install all dependencies (frontend, workers, dotnet restore)
install:
	@echo "[placeholder] install: monorepo packages not scaffolded yet"

# Run the local development environment via .NET Aspire
dev:
	@echo "[placeholder] dev: AppHost not scaffolded yet"

# Build all projects
build:
	@echo "[placeholder] build: frontend/backend not scaffolded yet"