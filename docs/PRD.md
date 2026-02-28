# Product Requirements Document (PRD)

## 1. Project Overview

A personal portfolio and interactive learning lab. It serves as a public-facing resume, a blog/content hub, and a private playground for testing new technologies (AI coding, Mastra, n8n, Motia, TanStack, .NET, Temporal.io, and Marten).

## 2. Target Audience (Personas)

- **Recruiters & Engineering Managers**: Want to quickly assess skills, view project showcases, and read architectural thoughts. Needs a fast, responsive, and impressive UI.
- **Developers/Readers**: Visiting to read technical articles, blogs, and distilled news feeds.
- **The Creator (Admin)**: Needs a frictionless way to author content, trigger workflows, and experiment with code. Requires a secure, authenticated admin dashboard.

## 3. Core Features

### 3.1. Personal Software Developer/Architect Portfolio

- Interactive resume, skills matrix, and timeline.
- Project showcase with case studies, tech stack tags, and architecture diagrams.

### 3.2. Content Management & Articles

- Custom Blog/Article engine built on a .NET backend using Marten (storing articles as documents in Postgres).
- Markdown/MDX rendering on the TanStack Start frontend.
- Admin portal to Create, Read, Update, Delete (CRUD) articles (Auth protected via Supabase).

### 3.3. Personal News & Video Distiller

- Automated workflows pulling external news feeds and video transcripts.
- AI summaries and categorization using Mastra/Motia.
- Temporal.io integrating the resilience and scheduling of these long-running extraction jobs, feeding data to the .NET/Marten database.

### 3.4. Personal Tools & Mini-Games

- Web-based utilities (e.g., custom developer calculators).
- Tiny browser games built with React and TanStack to showcase frontend interactive skills.

## 4. Non-Functional Requirements

- **Performance**: High Lighthouse scores for the public-facing portfolio (TanStack Start SSR/SSG/hydration strategies).
- **Security**: Supabase Auth configured with RLS (Row Level Security) on the Postgres DB. The .NET API must validate Supabase JWT tokens for admin routes.
- **Developer Experience (DX)**: Unified `just` commands. One-command startup via .NET Aspire (`just dev`).
- **Deployability**: Containerized via .NET Aspire publishing tools to deploy onto Azure Container Apps.
