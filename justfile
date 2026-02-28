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

# Build all pnpm-based projects
build_js:
	pnpm -r --if-present build

# Build all dotnet-based projects
build_cs:
	dotnet build .\src\backend\backend.slnx

# Build all projects
build: build_js build_cs


# Run node quality checks
check_js:
	pnpm -r --if-present check

# Run markdown quality checks
check_md:
	pnpm exec markdownlint-cli2

# Run dotnet quality checks
check_cs:
	dotnet csharpier check .

# Run all quality checks
check: check_js check_md check_cs

# Auto-format node projects
format_js:
	pnpm -r --if-present format

# Auto-format a single JS/TS file
format_js_file path:
	pnpm exec prettier --write "{{path}}"

# Auto-format markdown
format_md:
	pnpm exec markdownlint-cli2 --fix

# Auto-format a single markdown file
format_md_file path:
	pnpm exec markdownlint-cli2 --fix "{{path}}"

# Auto-format dotnet projects
format_cs:
	dotnet csharpier format .

# Auto-format a single C# file
format_cs_file path:
	dotnet csharpier format "{{path}}"

# Auto-format code and markdown across node and dotnet projects
format: format_js format_md format_cs
