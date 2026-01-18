---
name: designer
description: Enforces the Antigravity project's specific design system, color palette, and UI/UX patterns.
---

# Designer Skill

This skill ensures all UI components and pages adhere to the established "AI SQL Profiler" design system.

## Design Philosophy

1. **Developer-First**: Clean, functional, and information-dense but readable.
2. **Glassmorphism Lite**: Subtle shadows, semi-transparent backgrounds, and blurred overlays in dark mode.
3. **Responsive**: All designs MUST be mobile-first and adaptive.
4. **Theme Parity**: Every component must look premium in both **Light** and **Developer Dark** themes.

## Color Palette (CSS Variables)

Always use these variables instead of hardcoded hex values:

| Variable | Light (Default) | Dark (`.dark`) | Purpose |
| :--- | :--- | :--- | :--- |
| `--bg-primary` | `#f0f4f8` | `#0d1117` | Main page background |
| `--bg-surface` | `#ffffff` | `#010409` | Cards and containers |
| `--bg-secondary` | `#e2e8f0` | `#161b22` | Navigation and sidebars |
| `--border-color` | `#cbd5e1` | `#30363d` | Dividers and borders |
| `--text-primary` | `#1e293b` | `#c9d1d9` | Headings and main text |
| `--text-secondary`| `#64748b` | `#8b949e` | Subtitles and meta data |
| `--accent` | `#3b82f6` | `#58a6ff` | Links, icons, and primary buttons |

## UI Patterns

### 1. Statistics Cards
- Use a grid for stats.
- Background: `--bg-surface`.
- Border: `1px solid --border-color`.
- Rounded corners: `12px`.
- Transition: `transform 0.2s` for hover lift.

### 2. Information Density
- Use `1rem` base padding for mobile, `1.5rem` to `2rem` for desktop.
- Monospace font for SQL and code snippets: `'Segoe UI Mono', 'Courier New', monospace`.

### 3. Status Badges
- **Success**: Green background (light) / Green border (dark).
- **Failure**: Red background (light) / Red border (dark).
- Use emojis or SVG icons for immediate visual feedback.

## Usage Instructions

When creating new `.razor` pages or components:
1. Always wrap the main content in a container that respects the grid.
2. Ensure every interactive element has a `:hover` or `:active` state.
3. Use Tailwind classes derived from the theme where possible (e.g., `bg-light-bg dark:bg-dev-dark`).
4. If styling a complex component, add a `<style>` block at the bottom using the CSS variables defined in `site.css`.
