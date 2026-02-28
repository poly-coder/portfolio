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

These commands do not exist in PowerShell Core and will cause failures.

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

- Use backslash `\` for path separators (Windows native)
- Quote paths with spaces: `"C:\Path With Spaces\file.txt"`
- Use `$PSScriptRoot` for current directory context when needed
- Use `Get-Item`, `Get-ChildItem`, `Test-Path` for file operations (not Unix tools like `ls`, `find`, `[ -f ]`)

## Command Execution

- Prefer PowerShell cmdlets over external executables
- Use `-ErrorAction SilentlyContinue` to suppress errors when appropriate
- Use semicolons `;` to chain commands on one line
- Use pipes `|` for object-based pipelines (PowerShell pipes objects, not text)
