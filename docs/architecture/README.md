# Architecture Overview

## Introduction

This document provides an overview of the architecture for the polyglot personal portfolio and learning lab. The system is designed as a monorepo that houses a frontend, a robust backend, and intelligent background workers. It leverages modern tools across the JavaScript/TypeScript and .NET ecosystems to serve as a unified platform for personal branding, content management, experimentation, and AI integrations.

## Technology Stack

- **Orchestration & Deployment**: .NET Aspire (Local Orchestration) targeting Azure Container Apps (ACA).
- **Task Runner**: `just` for cross-platform CLI standardization.
- **Frontend**: TanStack Start (React, full-stack routing/data fetching) hosted in `src/frontend` (pnpm workspace).
- **Backend API**: .NET 10 API (in `src/backend`) utilizing Marten for Document DB capabilities over PostgreSQL.
- **Workers (AI & Workflows)**: Node.js worker ecosystem in `src/workers` (pnpm workspace) integrating n8n, Mastra, Motia, and Temporal.io.
- **Database & Authentication**: Supabase (PostgreSQL hosting for Marten, and Supabase Auth for identity management).

## System Design

### 1. Monorepo Structure

The repository follows a polyglot monorepo structure:

- `src/frontend/`: Contains the TanStack Start web application.
- `src/backend/`: Contains the .NET solution (`.slnx`), including the Core API and Marten implementation.
- `src/workers/`: Contains Node.js backend services dedicated to workflows and AI agents (n8n, Temporal workers, etc.).
- `src/apphost.cs` (root-level script): The .NET Aspire orchestration entrypoint scaffold; frontend/backend/workers/container wiring is added in later epics.

### 2. Data Flow & Component Interaction

- **Client to Backend**: The TanStack Start frontend securely calls the .NET backend API for dynamic content (Articles, Portfolio items) and specific application state.
- **Backend to Database**: The .NET Backend uses Marten to persist complex object structures (like markdown articles, app configurations, game scores) as JSON documents directly into the Supabase Postgres database.
- **Auth**: Frontend acts as a client to Supabase Auth. Protected admin routes require valid JWTs. The .NET backend validates these JWTs using the Supabase JWT secret.
- **Workflows & AI**: The `src/workers` handle asynchronous heavy lifting. For example, a content distiller workflow managed by Temporal might invoke Mastra/Motia AI agents to summarize news, and then send the output to the .NET API to be saved via Marten.

## Conclusion

This architecture embraces a "best tool for the job" philosophy. By relying on .NET Aspire to orchestrate diverse project types, it keeps local development frictionless while ensuring a clear path to scalable cloud deployments on Azure Container Apps.

## Supabase Provisioning Runbook (Dev)

This runbook defines the baseline for Epic 2 task: **Provision Supabase Project (Postgres + Auth)**.

### Scope

- Environment: local/dev only.
- Provisioning mode: manual in Supabase dashboard.
- Auth baseline: email/password provider only.

### Provisioning Steps

1. Create a Supabase project dedicated to development.
2. Enable/confirm Email provider in Supabase Auth settings.
3. Capture API values and DB connection values.
4. Populate local environment variables (contract in `.env.example`).
5. Start Aspire via `just dev` so backend/frontend/workers can consume the same Supabase contract once scaffolded.

### Required Local Configuration Contract

- `SUPABASE_URL`
- `SUPABASE_PUBLISHABLE_KEY`
- `SUPABASE_SECRET_KEY`
- `SUPABASE_DB_HOST`
- `SUPABASE_DB_PORT`
- `SUPABASE_DB_NAME`
- `SUPABASE_DB_USER`
- `SUPABASE_DB_PASSWORD`
- Optional: `SUPABASE_CONNECTION_STRING`

### Verification Checklist

- Supabase dev project exists and is accessible.
- Email/password auth is enabled.
- API keys and DB values are retrievable and stored securely.
- Local shell can load values and start AppHost (`just dev`).
