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
configuration names (names only — see `.ai/architecture.md` for how the
connection string is consumed):

- `AllowedHosts`
- `ConnectionStrings:DbConnectionString`
- `Logging:LogLevel:Default`
- `Logging:LogLevel:Microsoft.AspNetCore`

## Dependencies

`deterministic.dependencies` in `.ai/distillate.json` now walks every manifest in
the repo (five `.csproj` files plus both `package.json` files) and reports 41 raw
entries, up from 20 in the prior distillate — the earlier run only read the repo
root and missed `movies.client/package.json` entirely.
Deduplicated by package:

Backend (.NET, `net8.0`): `MediatR` + `MediatR.Contracts` (CQRS-style command/query
dispatch), `FluentValidation.DependencyInjectionExtensions` (request validation
pipeline behavior), `Mapster` + `Mapster.DependencyInjection` (object-to-object
mapping), `Microsoft.EntityFrameworkCore` + `.Design` + `.Relational` + `.Tools`
+ `.Sqlite`, `Microsoft.Data.Sqlite`, `SQLitePCLRaw.bundle_e_sqlite3`
(persistence), `Microsoft.AspNetCore.OpenApi` + `Swashbuckle.AspNetCore`
(Swagger/OpenAPI), and `Microsoft.AspNetCore.SpaProxy` (SPA dev-server proxying).

Frontend (`movies.client/package.json`, Node/npm) — the SPA's real manifest:
`react` + `react-dom` + `react-router-dom` (app shell/routing), `axios` (API
calls), `@mui/material` + `@emotion/styled` (UI components actually imported by
`App.tsx` and the movie components), `semantic-ui-css` (base stylesheet, imported
in `main.tsx`), `semver` and `yarn` (declared but not imported by any source
file — carried along as-is), and dev-only `vite`, `typescript` +
`typescript-eslint`, `eslint` + its plugins, and `@vitejs/plugin-react` for the
build/lint toolchain.

A second, stray `package.json` at the repo root also declares `@emotion/styled`
and `semantic-ui-react` — `semantic-ui-react` is not imported anywhere in
`movies.client/src` (only `semantic-ui-css` is). It appears to be a leftover file
rather than a real manifest (no `workspaces` key, not referenced by
`movies.client.esproj` or the `.sln`).

## Operational surface

N/A — no operational surface. This is a locally-run development application: no
scheduled tasks, no background services, and no hosted/deployed endpoint are
defined in the repository (`launchSettings.json` only configures local IIS
Express / Kestrel profiles).

## Deeper docs

- `.ai/architecture.md` — component breakdown, data flow, and integration points.
- `.ai/CONTEXT.md` — standards-precedence declaration for this repo.
- `.ai/distillate.json` — machine-readable repo distillate this file was rendered
  from (see its `verification` block for the commit it was checked against).

---
schema_version: 1.0.0
distilled_from_sha: c12684b1aa7a1e4e21e0c5344e5376cb19f64ab8
verified_sha: c12684b1aa7a1e4e21e0c5344e5376cb19f64ab8
verified_at: 2026-08-16T21:50:11Z
