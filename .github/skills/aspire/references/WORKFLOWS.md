# Aspire Workflows (Level 2)

## Standard dev loop
1. Tooling check
   - `aspire --version`
   - `aspire --help`
2. Project bootstrap
   - new repo/project: `aspire new`
   - existing solution: `aspire init`
3. Compose app model
   - add integrations: `aspire add <integration>`
4. Run and validate locally
   - `aspire run`
5. Iterate
   - `aspire update`
   - `aspire config`
6. Produce artifacts / deploy (preview)
   - `aspire publish`
   - `aspire deploy`
   - `aspire do <step>`

## Response pattern for agents
- Start with the shortest runnable command sequence.
- Add caveats only for preview commands or known blockers.
- Keep output focused on user objective: run locally, add integration, or publish/deploy.

## Non-interactive automation
Use `--non-interactive` for CI and scripted runs.

## Debug startup
Use `--debug` and optionally `--wait-for-debugger` when troubleshooting command execution.
