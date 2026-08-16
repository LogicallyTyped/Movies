# Movies

## What this is

Movies is a full-stack movie-catalog CRUD application. The backend, `Movies.Server`,
is an ASP.NET Core minimal-API project (`net8.0`) that exposes a REST API for
creating, listing, updating, and deleting movies. It is layered
clean-architecture-style into `Movies.Domain` (entities), `Movies.Application`
(MediatR commands/queries and FluentValidation), `Movies.Infrastructure` (EF Core +
SQLite persistence), and `Movies.Contracts` (shared DTOs, request/response models,
and exceptions used by both the application and API layers). The frontend,
`movies.client`, is a Vite + React + TypeScript single-page app that consumes the
API and is wired into the .NET solution via `Microsoft.AspNetCore.SpaProxy`.

Consumers: the `movies.client` React app (primary UI consumer), and any other HTTP
API client hitting the `/api/movies` endpoints directly (the API also serves a
Swagger UI in development for interactive exploration).

## Quickstart

Prerequisites: .NET 8 SDK, Node.js (for the `movies.client` SPA).

Run the API (serves Swagger UI at `/swagger` in Development, and proxies to the
SPA dev server via `Microsoft.AspNetCore.SpaProxy`):

```bash
dotnet run --project Movies.Server
```

Run the frontend dev server directly (used by the SPA proxy, or standalone):

```bash
cd movies.client
npm install
npm run dev
```

Build the frontend for production:

```bash
cd movies.client
npm run build
```

No automated tests are present in this repository — there is no test project in
`Movies.sln` and no `test`/`lint`-as-CI script beyond `movies.client`'s ESLint
(`npm run lint`).

## Entrypoints

- `Movies.Server` (`Movies.Server/Program.cs`) — ASP.NET Core minimal-API host;
  maps the `/api/movies` routes, serves Swagger UI in Development, and proxies to
  the `movies.client` SPA dev server.
- `movies.client` (`movies.client/src/main.tsx`, via `npm run dev` / `vite`) — the
  React SPA; not a .NET entrypoint but the primary human-facing surface.

HTTP routes exposed by `Movies.Server`:

- `GET /api/movies`
- `GET /api/movies/{id}`
- `POST /api/movies`
- `PUT /api/movies/{id}`
- `DELETE /api/movies/{id}`

## Configuration

Configuration is read from ASP.NET Core's standard `appsettings.json` /
`appsettings.Development.json` / environment-variable layering. Known
configuration names (names only — see `docs/architecture.md` for how the
connection string is consumed):

- `AllowedHosts`
- `ConnectionStrings:DbConnectionString`
- `Logging:LogLevel:Default`
- `Logging:LogLevel:Microsoft.AspNetCore`

## Dependencies

Backend (.NET, `net8.0`): ASP.NET Core minimal APIs, `MediatR` (CQRS-style
command/query dispatch), `FluentValidation` (request validation pipeline behavior),
`Mapster` (object-to-object mapping), `Microsoft.EntityFrameworkCore` +
`Microsoft.EntityFrameworkCore.Sqlite` (persistence), `Swashbuckle.AspNetCore`
(Swagger/OpenAPI), and `Microsoft.AspNetCore.SpaProxy` (SPA dev-server proxying).

Frontend (`movies.client`, Node/npm): React + React DOM, React Router, Axios (API
calls), `@mui/material`, `semantic-ui-react` / `semantic-ui-css`, and Vite +
TypeScript for the build/dev toolchain.

## Operational surface

N/A — no operational surface. This is a locally-run development application: no
scheduled tasks, no background services, and no hosted/deployed endpoint are
defined in the repository (`launchSettings.json` only configures local IIS
Express / Kestrel profiles).

## Deeper docs

- `docs/architecture.md` — component breakdown, data flow, and integration points.
- `.ai/CONTEXT.md` — standards-precedence declaration for this repo.
- `.ai/distillate.json` — machine-readable repo distillate this file was rendered
  from (see its `verification` block for the commit it was checked against).

---
schema_version: 1.0.0
distilled_from_sha: a805589550ccc625d4a0e40842cfe5dc6268b879
verified_sha: a805589550ccc625d4a0e40842cfe5dc6268b879
verified_at: 2026-08-16T16:20:15Z
