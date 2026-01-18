# Designer Pattern Example: Task Card

## Razor Implementation
```html
<div class="task-card">
    <div class="task-header">
        <span class="badge success">✅ Completed</span>
        <span class="timestamp">10:45 AM</span>
    </div>
    <h3>Optimize SP_GetRevenue</h3>
    <p>Modified indexes on AccountTable to improve query performance by 40%.</p>
</div>

<style>
    .task-card {
        background: var(--bg-surface);
        border: 1px solid var(--border-color);
        border-radius: 12px;
        padding: 1.5rem;
        transition: all 0.2s ease-in-out;
    }

    .task-card:hover {
        transform: translateY(-4px);
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        border-color: var(--accent);
    }

    .task-header {
        display: flex;
        justify-content: space-between;
        margin-bottom: 0.75rem;
    }

    h3 {
        color: var(--text-primary);
        margin: 0.5rem 0;
    }

    p {
        color: var(--text-secondary);
        font-size: 0.9rem;
    }

    .badge {
        font-size: 0.8rem;
        font-weight: 600;
        padding: 2px 8px;
        border-radius: 4px;
    }

    .badge.success {
        background: rgba(34, 197, 94, 0.1);
        color: #22c55e;
        border: 1px solid rgba(34, 197, 94, 0.2);
    }
</style>
```

This example demonstrates the core philosophy:
- Uses variables for theming.
- Includes micro-interactions (`hover`).
- Follows the "AI SQL Profiler" card pattern.
