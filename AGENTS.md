# Poly-Coder Portfolio AGENTS.md

The role of this file is to describe common mistakes and confusion points that agents might encounter as they work on the codebase. If you ever encounter something in the project that surprises you, please alert the developer working with you and indicate that this is the case in the Agents.md file, to help prevent future agents from having the same confusion.

This is still a greenfield project, not yet in production. Feel free for the time being to make any changes to the project without any concern for breaking changes. The goal is to get to a good state as quickly as possible, and then we can worry about breaking changes later on.

Use rather succinct language when writing human-readable notes, sacrificing formality for clarity and speed.

## Repo Commands

Use `just` to run commands in the repo. The `justfile` is located at the root of the project, and you can add commands there as needed. For example, to start the development environment, you can run:

```pwsh
just dev    # Starts the Aspire AppHost entrypoint (service wiring is still being scaffolded).
```
