# Aspire Quickstart (Level 1)

## Goal
Fast path to get an Aspire app running locally.

## What Aspire is
Aspire provides tools, templates, and packages for building observable, production-ready distributed apps with a code-first app model and unified local tooling.

## Minimal command set
1. Verify CLI
   - `aspire --version`
   - `aspire --help`
2. Start new project
   - `aspire new`
3. Or initialize existing solution
   - `aspire init`
4. Run locally
   - `aspire run`

## CLI snapshot
From local `aspire --help`:
- Global options: `--debug`, `--non-interactive`, `--wait-for-debugger`, `--help`, `--version`
- Commands: `new`, `init`, `run`, `add`, `publish` (Preview), `config`, `cache`, `deploy` (Preview), `do` (Preview), `update` (Preview), `mcp`

## When to move to next level
- Need command sequencing for real project changes -> `WORKFLOWS.md`
- Need integration choices -> `INTEGRATIONS.md`
- Hit runtime/cert/container issues -> `TROUBLESHOOTING.md`
