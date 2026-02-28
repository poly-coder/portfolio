---
description: This file describes the terminal usage guidelines for the project, including shell requirements and output filtering practices.
applyTo: '**/*'
---
# Terminal Instructions

## Shell Requirements

**ALWAYS use PowerShell Core (`pwsh`), never `cmd.exe` or `bash`.**

This project is configured to run on PowerShell Core exclusively. All terminal commands must be compatible with pwsh.

## Output Filtering: Never Use Unix Tools

### ❌ FORBIDDEN: Unix-style filters

Do NOT use these commands:
- `| head` or `| head -n #`
- `| tail` or `| tail -n #`
- `| grep` (use Select-String instead)
- `| cut` (use Select-Object instead)
- `| sed` (use similar PowerShell constructs)

These are Unix utilities and may not be reliably available across all contributor environments. Use PowerShell equivalents instead for consistent cross-platform compatibility.

### ✅ CORRECT: PowerShell equivalents

**To limit output to first N lines:**
```powershell
command | Select-Object -First 10
```

**To limit output to last N lines:**
```powershell
command | Select-Object -Last 10
```

**To skip and take (like head -n +N):**
```powershell
command | Select-Object -Skip 5 -First 10
```

**To filter by pattern (instead of grep):**
```powershell
command | Select-String 'pattern'
```

**To select specific fields (instead of cut):**
```powershell
command | Select-Object -Property Name, FullName
```

## Examples

### ❌ Wrong
```powershell
Get-ChildItem -Recurse | head -20
git log --oneline | head -5
```

### ✅ Correct
```powershell
Get-ChildItem -Recurse | Select-Object -First 20
git log --oneline | Select-Object -First 5
```

## Path Handling

- Use `Join-Path` for cross-platform-safe path construction (works on Windows, macOS, and Linux):
  ```powershell
  $logPath = Join-Path $PSScriptRoot 'logs' 'app.log'
  ```
- Quote paths with spaces: `"$(Join-Path $env:TEMP 'My File.txt')"`
- Use `$PSScriptRoot` for current relative directory context
- Use `Get-Item`, `Get-ChildItem`, `Test-Path` for portable file operations (instead of Unix tools like `ls`, `find`, `[ -f ]`)

## Command Execution

- Prefer PowerShell cmdlets over external executables
- Use `-ErrorAction SilentlyContinue` to suppress errors when appropriate
- Use semicolons `;` to chain commands on one line
- Use pipes `|` for object-based pipelines (PowerShell pipes objects, not text)
