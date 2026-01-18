---
name: manager
description: Provides a framework for task management, feature tracking, and progress reporting, mirroring GitHub Issue workflows.
---

# Manager Skill

This skill allows the agent to act as a Project Manager, ensuring that every feature request is tracked, prioritized, and updated as work progresses.

## Task Tracking Mechanism

Since direct API access to GitHub might be restricted, this skill uses a **Local Task Mirror** (`TASKS.md`) in the workspace root to track the state of the project.

### Workflow
1. **New Feature Request**: When a new feature is requested, the agent creates a "Task" entry in `TASKS.md` with a unique ID and status `[Backlog]`.
2. **Start Development**: When work begins, the status is moved to `[In Progress]`.
3. **Completion**: Once tests pass and code is pushed, the status is moved to `[Done]`.
4. **Synchronization**: Every time `TASKS.md` is updated, the agent should report the change to the user so they can manually sync with GitHub Issues if desired.

## Task Structure in `TASKS.md`

Tasks must follow this format:
- `[Status] #ID: Task Title`
- **Description**: Brief objective.
- **Sub-tasks**: Checklist of implementation steps.
- **Linked Commits**: Hashes of code related to this task.

## Status Definitions

- **Backlog**: Requested but not yet started.
- **In Progress**: Currently being developed or tested.
- **Blocked**: Waiting on external input or another task.
- **Done**: Code pushed, tests passed, and documentation completed.

## Usage Instructions

When acting as the Manager:
1. **Initialize**: If `TASKS.md` doesn't exist, create it from the template in `resources/template_tasks.md`.
2. **Audit tasks**: At the start of every session, read `TASKS.md` to understand context.
3. **Auto-Update**: As soon as a file is modified that relates to a task, update the `TASKS.md` file to reflect "In Progress" or "Done".
4. **Report**: In the final message of a task, provide a summary of which tasks were updated.
