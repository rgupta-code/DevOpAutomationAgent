---
description: Daily Code Guardian execution cycle including sync, build, code review, test, and report persistence.
---

// turbo-all

1. **Environmental Sync**
   Synchronize with the remote repository to ensure the guardian is running on the latest code.
   ```powershell
   git pull origin main
   ```

2. **Main Project Build**
   Verify the main application compiles correctly before proceeding.
   ```powershell
   dotnet build DotNetProjectForAntigravity/DotNetProjectForAntigravity.csproj
   ```

3. **Code Guard Audit & Automated Test Expansion**
   Perform a comprehensive quality audit of the `DotNetProjectForAntigravity/Services` and `DotNetProjectForAntigravity/Data` directories.
   - **Audit Logic**: Compare implementation logic against the `Tests/` suite to identify coverage gaps.
   - **Automated Remediation**: If public methods or critical logic paths lack corresponding tests, the agent MUST synthesize and inject the missing test cases into the appropriate suite.
   - **Architectural Integrity**: Verify compliance with dependency injection patterns and security best practices.

4. **Test Execution**
   Run the full NUnit test suite (including any newly added tests) and capture the output.
   ```powershell
   dotnet test Tests/Tests.csproj
   ```

5. **Data Persistence**
   Analyze the test output and perform the following:
   - Generate a new JSON summary file in `DotNetProjectForAntigravity/Data/Summaries/` using the `DailySummary` model.
   - Update `DotNetProjectForAntigravity/TestReport.html` with visual results.
   - Update `DotNetProjectForAntigravity/Walkthrough.md` with the latest activities and a list of any newly added tests.

6. **Version Control Sync**
   Commit the generated reports, summaries, and any newly created tests back to the repository.
   ```powershell
   git add .
   git commit -m "chore(guard): Daily Code Guard execution review and test expansion [skip ci]"
   git push origin main
   ```

7. **Status Check**
   Verify the dashboard is updated and that all new tests are successfully reflected in the JSON summary.
