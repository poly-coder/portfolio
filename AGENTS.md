# Poly-Coder Portfolio AGENTS.md

The role of this file is to describe common mistakes and confusion points that agents might encounter as they work on the codebase. If you ever encounter something in the project that surprises you, please alert the developer working with you and indicate that this is the case in the AGENTS.md file, to help prevent future agents from having the same confusion.

This project is still greenfield and not in production yet. For now, optimize for speed—breaking changes are acceptable until the project stabilizes.

Use rather succinct language when writing human-readable notes, sacrificing formality for clarity and speed.

## Repo Commands

Use `just` to run commands in the repo. The `justfile` is at the repo root.

Daily local dev commands:

```pwsh
just help      # List available commands.
just install   # Install pnpm deps + restore .NET solution.
just dev       # Start Aspire AppHost local dev environment.
just check     # Run node checks, markdown lint, and CSharpier check.
just format    # Run node formatters, markdown lint --fix, and CSharpier format.
just build     # Reserved; currently scaffold placeholder and exits with error.
```

Occasional setup command:

```pwsh
just trust-dev-certs   # Trust local HTTPS dev cert.
```

## Immediate Per-File Formatting

When you create or modify a file, run its formatter/linter command immediately after the edit and before any other command (including build/check/test/dev):

- `.cs`: `just format_cs_file "<file_path>"`
- `.md`: `just format_md_file "<file_path>"`
- `.ts`, `.tsx`, `.js`, `.jsx`: `just format_js_file "<file_path>"`

Do this right after each relevant file edit, then continue with broader repo commands like `just check` or `just format`.
