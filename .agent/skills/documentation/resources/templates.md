# Documentation Templates

## Service Documentation Template

```markdown
# [Service Name]

## Overview
Brief description of the service's responsibility and its role in the system.

## Dependencies
- `ServiceA` (Required for X)
- `IServiceB` (Injected via constructor)

## Public API

### `Task<Result> MethodNameAsync(Parameter p)`
Detailed description of what the method does, inputs, and outputs.

**Example:**
```csharp
var result = await service.MethodNameAsync(myData);
```

## Internal Logic
High-level description of any complex algorithms or data flows.
```

## Walkthrough Template

```markdown
# Walkthrough: [Feature Name] - [Date]

## Summary
One-sentence summary of the objective.

## Steps Taken
1. **[Step 1]**: Description of work.
2. **[Step 2]**: Description of work.

## Files Modified
- `Path/To/File.cs`: Primary change.
- `Path/To/Test.cs`: Coverage added.

## Verification Results
- [ ] Build Succeeded
- [ ] Tests Passed (X/X)
- [ ] Manual verification on screen Y
```
