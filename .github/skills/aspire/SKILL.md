---
name: aspire
description: Guidance for .NET Aspire architecture, app orchestration, and aspire CLI workflows. Use when users ask about Aspire setup, AppHost, integrations, local orchestration, or Aspire publish/deploy flows.
argument-hint: Describe your Aspire goal, current project state, and whether you need setup, integration, troubleshooting, or deploy guidance.
user-invokable: false
---

# aspire

Use this skill for .NET Aspire architecture, app orchestration, and CLI workflows.

## Trigger phrases
- .NET Aspire
- aspire CLI
- apphost orchestration
- distributed app local run
- Aspire integrations/deployment

## Expected input
- User objective (for example: initialize existing solution, add integration, troubleshoot local run, publish/deploy)
- Current state (new repo, existing solution, AppHost exists or not)
- Constraints (local only vs CI, interactive vs non-interactive)

## Expected output
- A minimal, runnable command sequence tailored to the objective
- Any required caveats (preview command notes, environment prerequisites)
- Optional next step if user needs deeper guidance from Level 2-4 docs

## Use for
- Creating or initializing Aspire projects (`aspire new`, `aspire init`)
- Running AppHost locally (`aspire run`)
- Adding and updating integrations (`aspire add`, `aspire update`)
- Generating/deploying artifacts with preview commands (`aspire publish`, `aspire deploy`, `aspire do`)
- Service discovery, service defaults, dashboard-centered dev loops

## Do not use for
- Non-Aspire .NET apps with no AppHost/distributed composition
- Azure resource troubleshooting after deployment (use Azure diagnostics skills)
- Pure Kubernetes or Docker pipelines not using Aspire

## Progressive disclosure
Start at the smallest doc that answers the question, then expand only if needed.

1. Level 1: Quick answer
   - Read: `QUICKSTART.md`
   - Use when user asks for setup/run/basic commands.

2. Level 2: Build workflow
   - Read: `WORKFLOWS.md`
   - Use when user wants sequence planning: scaffold → integrate → run → publish/deploy.

3. Level 3: Integrations map
   - Read: `INTEGRATIONS.md`
   - Use when choosing storage/database/messaging/caching/framework integrations.

4. Level 4: Troubleshooting & references
   - Read: `TROUBLESHOOTING.md`
   - Use when there are cert/runtime/connection-string/container-health issues.

## How to use supplemental docs
- Always begin with Level 1 for basic setup/run questions.
- Move to Level 2 when the user needs multi-step planning or command sequencing.
- Move to Level 3 only when selecting integrations by category.
- Move to Level 4 only when the user reports failures or asks for deeper references.
- Keep answers concise and include only the level(s) needed for the current prompt.

## Step-by-step procedure
1. Classify the request: setup, workflow, integrations, or troubleshooting.
2. Load only the minimum level doc required (Level 1 first by default).
3. Produce a concise command plan using `aspire` CLI commands appropriate to the request.
4. Include prerequisites only when required for execution (cert/runtime/CI mode).
5. If blocked, escalate to Level 4 troubleshooting links and ask for the missing context needed to proceed.

## Examples
- Input: "Initialize Aspire in my existing solution and run it locally."
   - Output style: `aspire init` then `aspire run`, plus any prerequisite checks.
- Input: "Which messaging integration should I use with Aspire?"
   - Output style: Brief options from Level 3 (Service Bus/Event Hubs/RabbitMQ/Kafka/NATS) with selection guidance.
- Input: "aspire run fails with certificate trust issues."
   - Output style: Level 4 triage sequence plus direct troubleshooting doc links.

## Canonical source
- Main docs hub: https://learn.microsoft.com/en-us/dotnet/aspire/
- Prefer this hub for current links to Aspire fundamentals, integrations, deployment, and troubleshooting.