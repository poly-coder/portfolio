# Epic List

## Epic 1: Monorepo Foundation & DevX

- [x] Initialize Git repository and `.gitignore` for node and .NET environments.
- [x] Set up Root Task Runner (`justfile`) with placeholder commands.
- [x] Create base `pnpm-workspace.yaml` for `src/frontend` and `src/workers` node modules.
- [x] Create base .NET solution (`.slnx`) for `src/backend`.
- [ ] Initialize .NET Aspire AppHost (`src/aspire`) to orchestrate frontend, backend, and worker startup in one local command.

## Epic 2: Core API Backend & Database Setup

- [ ] Provision Supabase Project (Postgres + Auth).
- [ ] Scaffold .NET 9 Web API in `src/backend`.
- [ ] Integrate Marten into the .NET API and connect to Supabase Postgres.
- [ ] Define initial Document Schemas (Articles, Config, User Profiles, etc.).
- [ ] Implement secure endpoints with JWT validation using Supabase Auth keys.

## Epic 3: Frontend Foundation & Auth

- [ ] Scaffold TanStack Start application in `src/frontend`.
- [ ] Initialize TailwindCSS and core design system structure.
- [ ] Integrate Supabase Client for authentication (Login/Logout/Session management).
- [ ] Create protected Admin layout and public portfolio layout.

## Epic 4: Personal Portfolio & Articles UI (CMS)

- [ ] Build public portfolio landing page and project showcase.
- [ ] Build public Article List and Article detail (Markdown parsing) pages.
- [ ] Build Admin Article Editor (Markdown/Rich Text).
- [ ] Connect TanStack frontend to the .NET API for CMS CRUD operations via React Query.

## Epic 5: Personal Tools & Playgrounds

- [ ] Implement custom web calculator tool.
- [ ] Implement integrated tiny browser game.
- [ ] Wire tools into the main portfolio navigation and testing routes.

## Epic 6: AI Data Distiller Workflows

- [ ] Scaffold Node.js workflow project in `src/workers`.
- [ ] Set up Temporal.io worker and connect to local/cloud Temporal Cluster.
- [ ] Build n8n/Mastra/Motia integrations to pull news/video transcripts.
- [ ] Implement AI summarization agent.
- [ ] Expose distilled data to the .NET API directly via Marten DB inserts or webhooks.
- [ ] Create frontend UI to consume the user-distilled news feed.
