# AI Integration Strategy: SP Optimization

## 🏗️ The Approach

To implement actual AI logic in our placeholder `AIService.OptimizeStoredProcedureAsync`, we should use a **Reasoning Model** (e.g., Gemini 1.5 Pro) with a **System Instruction** specifically tuned for SQL Performance.

### Proposed System Prompt:
> "You are an expert SQL Performance Engineer. Analyze the provided stored procedure and execution context. Suggest a version that is optimized for execution time and resource usage. Use CTEs where appropriate and ensure all Dapper parameters are maintained."

### Implementation Snippet (C#):
```csharp
public async Task<string> OptimizeStoredProcedureAsync(string spContent)
{
    // 1. Selection based on AI Expert Skill: 
    // SP Optimization requires deep reasoning -> Gemini 1.5 Pro.

    var client = new GeminiClient(_apiKey);
    var response = await client.GenerateContentAsync(new Request {
        SystemPrompt = "SQL Performance Specialist...",
        UserMessage = $"Optimize this SP: {spContent}"
    });

    return response.Text;
}
```

## 📈 Trend Application
By using **Prompt Caching** (supported by Gemini and Claude), we can reduce costs by 90% when asking for multiple optimizations on similar schemas during a single Code Guard cycle.
