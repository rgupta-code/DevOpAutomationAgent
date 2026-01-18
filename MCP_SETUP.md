# GitHub MCP Server Setup Guide

To switch from the CLI to the more powerful **Model Context Protocol (MCP)** server, follow these steps:

## 1. Prerequisites
Ensure you have **Node.js** installed (v18 or higher). 
*Your current version is v22.13.1 (all set).*

## 2. Generate/Retrieve your GitHub Token
You can use your existing GitHub CLI session to get your token. Run this in your terminal:
```powershell
gh auth token
```
Copy this token. You will need it for the configuration.

## 3. Configure your MCP Client
Depending on which application you are using to talk to me, you need to update its configuration file.

### 🏠 For Claude Desktop (Windows)
1. Open this file: `%APPDATA%\Claude\claude_desktop_config.json`
2. Add the following to the `mcpServers` section:
```json
{
  "mcpServers": {
    "github": {
      "command": "npx",
      "args": [
        "-y",
        "@modelcontextprotocol/server-github"
      ],
      "env": {
        "GITHUB_PERSONAL_ACCESS_TOKEN": "PASTE_YOUR_TOKEN_HERE"
      }
    }
  }
}
```

### 🖱️ For Cursor (Windows)
1. Go to **Settings** -> **Features** -> **MCP**.
2. Add a new server with:
   - **Name**: `github`
   - **Type**: `command`
   - **Command**: `npx -y @modelcontextprotocol/server-github`
3. Add Environment Variable:
   - **Key**: `GITHUB_PERSONAL_ACCESS_TOKEN`
   - **Value**: `PASTE_YOUR_TOKEN_HERE`

## 4. Verification
Once you save the config, restart your AI client. I should now have access to tools like:
- `create_issue`
- `update_project_item`
- `search_code`

---
**Note**: I have updated the **Manager Skill** to prioritize using these MCP tools once they are active.
