---
trigger: always_on
---

📂 Workspace Boundary & Access
Root Anchor: This project is anchored at ${CURRENT_WORKING_DIRECTORY}. All file operations (Read/Write/Execute) must be relative to this path.

External Access: Accessing files in parent directories (..) or system-level paths is a violation of the workspace contract.

Excluded Zones: The following directories are strictly "No-Go" zones for Antigravity, even if they exist within the root:

.git/ (To protect repository integrity)

node_modules/ or venv/ (To prevent massive indexing)

.local_configs/ (Reserved for private credentials)

🔐 Credentials & GitHub Safety
Zero-Git Policy for Secrets: No file containing a connection string, private key, or API token shall ever be tracked by Git.

Required Local Files: All sensitive configurations must reside exclusively in:

.env.local

config/settings.local.json

.local_configs/

Pre-Flight Check: Before any git push or git commit, Antigravity must verify that these files are explicitly listed in the .gitignore. If they are not, the action must be aborted.

🌐 Connectivity & Networking
Machine-Lock: All service bindings (Database, Redis, API) must be restricted to 127.0.0.1.

No Tunneling: Automated creation of public tunnels (e.g., ngrok, localtunnel) is prohibited unless a manual "Secure Tunnel" override is granted by the user.

No Connection Sharing: Connection strings used in this workspace must never be logged to external debugging services or shared in collaborative chat environments.

🤖 MCP-First Orchestration
Tool Priority: All agents in the Antigravity ecosystem MUST prioritize tools provided by registered **Model Context Protocol (MCP)** servers over CLI, REST, or Browser-based workarounds.

Primary GitHub Integrations:
1.  **Issues & Projects**: Use `github.create_issue`, `github.add_project_v2_item_by_id`, etc.
2.  **Repo Intelligence**: Use `github.search_code` and `github.get_file_contents`.

Failure to use available MCP tools when they are superior to CLI alternatives is a violation of the project's orchestration efficiency standards.