<!-- type: context | status: active | since: 2026-08-16 | applies-to: Movies -->

# Project context: Movies

## Identity
- **Repo:** `github.com/LogicallyTyped/Movies` (PUBLIC)
- **Local:** `C:\source\github.com\LogicallyTyped\Movies\`
- **Employer:** none (personal project)
- **Standards overlay:** personal only — `C:\source\github.com\ProgramandoBonito\conventions\`

## Purpose
Tutorial-shaped full-stack "Movies" CRUD app: an ASP.NET Core Minimal API backend
(`Movies.Server`, target `net8.0`) split into `Movies.Domain` / `Movies.Application` /
`Movies.Infrastructure` / `Movies.Contracts` (clean-architecture-style layering), using
MediatR, EF Core, SQLite (`movies.db` present in `Movies.Server/`), and FluentValidation for
request validation, plus a centralized `NotFoundException` + error-handling middleware and
CORS configuration. The frontend is `movies.client`, a Vite + React + TypeScript SPA
(`movies.client.esproj`, wired into the .NET solution via `SpaProxy`), pulling in
`semantic-ui-react` and `@emotion/styled` for UI. Commit history ("Implement movie management
API and delete functionality", "Add validation and request handling with FluentValidation",
"Enhance routing and middleware configuration") reads as incremental, tutorial-driven
feature-by-feature construction rather than a from-scratch original design; the exact source
tutorial/course is not confirmed from repo content alone. No README exists at the repo root
(`movies.client/README.md` is the Vite scaffold default, not project documentation).

The `.ai/` directory also holds `distillate.json` and `distillation-ollama.md` (untracked,
produced by an earlier doc-machinery pass — not touched by this file's commit).

## Deviations

None declared.
