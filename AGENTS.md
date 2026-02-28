# Poly-Coder Portfolio AGENTS.md

The role of this file is to describe common mistakes and confusion points that agents might encounter as they work on the codebase. If you ever encounter something in the project that surprises you, please alert the developer working with you and indicate that this is the case in the AGENTS.md file, to help prevent future agents from having the same confusion.

This project is still greenfield and not in production yet. For now, optimize for speed—breaking changes are acceptable until the project stabilizes.

Use rather succinct language when writing human-readable notes, sacrificing formality for clarity and speed.

## Repo Commands

Use `just` to run commands in the repo. The `justfile` is located at the root of the project, and you can add commands there as needed. For example, to start the development environment, you can run:

```pwsh
just dev    # Starts the Aspire AppHost entrypoint (service wiring is still being scaffolded).
```
