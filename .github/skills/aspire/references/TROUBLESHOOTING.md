# Aspire Troubleshooting & References (Level 4)

## Common issue classes
- Untrusted localhost certificate
- Container runtime unhealthy
- Resource name already in use
- Missing connection string
- Unsecure transport configuration needs

## Triage order
1. Re-run with visibility
   - `aspire run --debug`
2. Confirm environment and tooling
   - container runtime healthy
   - local cert trust valid
3. Check command mode
   - use `--non-interactive` in CI
4. Use Aspire troubleshooting docs for issue-specific fixes

## Main troubleshooting links
- https://learn.microsoft.com/en-us/dotnet/aspire/troubleshooting/allow-unsecure-transport
- https://learn.microsoft.com/en-us/dotnet/aspire/troubleshooting/untrusted-localhost-certificate
- https://learn.microsoft.com/en-us/dotnet/aspire/troubleshooting/name-is-already-in-use
- https://learn.microsoft.com/en-us/dotnet/aspire/troubleshooting/container-runtime-unhealthy
- https://learn.microsoft.com/en-us/dotnet/aspire/troubleshooting/connection-string-missing

## Additional references
- Aspire docs hub: https://learn.microsoft.com/en-us/dotnet/aspire/
- Aspire API reference: https://learn.microsoft.com/en-us/dotnet/api?view=dotnet-aspire-13.0&preserve-view=true
- Aspire OSS repo: https://github.com/dotnet/aspire
- Aspire samples: https://github.com/dotnet/aspire-samples
