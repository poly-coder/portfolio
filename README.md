# Poly-Coder Portfolio

This repository contains a collection of projects and code samples that demonstrate my skills and experience as a software developer. Each project is designed to showcase different aspects of my expertise, including full-stack development in multiple programming languages, problem-solving abilities, and my commitment to writing clean and efficient code.

Who is "Me", you ask? I am a passionate software developer with a strong background in various programming languages and technologies. I have experience working on a wide range of projects, from small scripts to large-scale applications. My goal is to continuously learn and grow as a developer while contributing to meaningful projects that make a positive impact.

## Supabase Dev Provisioning (Epic 2)

This repository currently uses **manual Supabase dashboard provisioning** for local/dev bootstrap.

### 1) Create a dev Supabase project

- Create one project in the Supabase dashboard for local development.
- Choose a nearby region.
- Save the generated database password in your password manager.

### 2) Enable Auth baseline

- Go to **Authentication → Providers**.
- Keep **Email** provider enabled (MVP auth mode for this stage).

### 3) Gather required values

Collect these values from **Project Settings** and database connection pages:

- `SUPABASE_URL`
- `SUPABASE_PUBLISHABLE_KEY`
- `SUPABASE_SECRET_KEY`
- `SUPABASE_DB_HOST`
- `SUPABASE_DB_PORT`
- `SUPABASE_DB_NAME`
- `SUPABASE_DB_USER`
- `SUPABASE_DB_PASSWORD`
- Optional: `SUPABASE_CONNECTION_STRING`

The local variable contract is defined in `.env.example`.

### 4) Load secrets locally before running Aspire

In PowerShell, set environment variables in your current shell (or source your local env file):

```powershell
$env:SUPABASE_URL = "https://<project-ref>.supabase.co"
$env:SUPABASE_PUBLISHABLE_KEY = "<publishable-key>"
$env:SUPABASE_SECRET_KEY = "<secret-key>"
$env:SUPABASE_DB_HOST = "db.<project-ref>.supabase.co"
$env:SUPABASE_DB_PORT = "5432"
$env:SUPABASE_DB_NAME = "postgres"
$env:SUPABASE_DB_USER = "postgres"
$env:SUPABASE_DB_PASSWORD = "<db-password>"
```

Then run:

```powershell
just dev
```

> `.env`, `.env.*`, and other secret files are gitignored. Commit only `.env.example`.
