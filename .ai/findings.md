<!-- type: findings | status: active | since: 2026-08-16 | applies-to: Movies -->

# Findings

## repo_distiller: Node dependency parser misses the frontend project's package.json

`deterministic.dependencies` in `.ai/distillate.json` lists only two npm packages
(`@emotion/styled`, `semantic-ui-react`), sourced from the repo-root `package.json`.
The actual frontend project, `movies.client/package.json`, has a much larger and
different dependency set (`react`, `react-dom`, `axios`, `react-router-dom`,
`@mui/material`, `semantic-ui-css`, `vite`, `typescript`, etc.) that is entirely
absent from the deterministic field. The root `package.json` appears to be a stray/
leftover file (no `workspaces` key, not referenced by `movies.client.esproj` or the
`.sln`) rather than the real dependency manifest for the SPA.

This is a parser-coverage gap, not a judgment call: `repo_distiller` should walk
every `package.json` under the repo (or at least the one belonging to the
project referenced by the solution/esproj), not only the repo root. Per the
Stage 2 contract (`conventions/repo-documentation.md`), this deterministic field
was left untouched — flagging here instead of hand-editing it.

Filed: 2026-08-16, TASK-5 supervised pilot doc-verify, `claude-sonnet`.

## repo_distiller: four class libraries misclassified as `"cli"` entrypoints

`deterministic.entrypoints` classifies `Movies.Application.csproj`,
`Movies.Contracts.csproj`, `Movies.Domain.csproj`, and
`Movies.Infrastructure.csproj` all as `"type": "cli"`. None of these four
`.csproj` files sets `<OutputType>Exe</OutputType>` (checked all four directly);
under the default `Microsoft.NET.Sdk`, that means each builds a class library
(DLL), not a runnable console/CLI entrypoint. Only `Movies.Server` (an actual
ASP.NET Core host, correctly typed `"http"`) is runnable in this solution.

README.md § Entrypoints was rendered listing only `Movies.Server` and
`movies.client` as real entrypoints — i.e. the rendered doc silently overrides
this deterministic field rather than reproducing an inaccurate "cli" claim for
four libraries. Flagging here per the Stage-2 contract instead of relying on
the override being self-evident. Left the deterministic field untouched.

## repo_distiller: `<Version>` child-element form not parsed for one PackageReference

`deterministic.dependencies` records `Microsoft.AspNetCore.SpaProxy` with
`"version": null`. `Movies.Server/Movies.Server.csproj` does pin a version for
it, just via the child-element form (`<PackageReference Include="...">`
followed by a nested `<Version>9.0.5</Version>`) rather than the more common
`Version="9.0.5"` XML-attribute form every other `PackageReference` in this
repo uses. The parser appears to only read the attribute form, so this one
entry's real version (9.0.5) was dropped to `null`. Left the deterministic
field untouched; flagging the parser gap.

Filed: 2026-08-16, TASK-5 supervised pilot doc-verify (advisor-prompted
follow-up pass), `claude-sonnet`.

## 2026-08-16 — Stage 2 re-verification after distiller fix (Sonnet, TASK-5 re-run)

**First finding above ("Node dependency parser misses the frontend project's
package.json") — RESOLVED.** The distiller has been fixed and the distillate
regenerated (`distilled_from_sha` unchanged at
`c12684b1aa7a1e4e21e0c5344e5376cb19f64ab8` — same commit, re-distilled):
`deterministic.dependencies` now walks both `package.json` files (repo root
and `movies.client/`) as well as all five `.csproj` files, and reports 41
entries (up from 20), including the real frontend manifest (`react`,
`react-dom`, `axios`, `react-router-dom`, `@mui/material`, `vite`,
`typescript`, etc. from `movies.client/package.json`). README § Dependencies
has been rewritten to reflect the full deduplicated set including the
front-end packages. Note the raw 41-entry array still contains duplicates
(e.g. `MediatR`, `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.Design`,
and `@emotion/styled` each appear twice — legitimately, from being referenced
by more than one `.csproj` / by both `package.json` files) — left as-is per
the verify-stage hard rule against hand-editing `deterministic.*`; the README
presents the deduplicated, human-readable view.

**Second and third findings above ("four class libraries misclassified as
`cli`" and "`<Version>` child-element form not parsed") — still open,
unaffected by this fix.** Re-checked against the regenerated distillate:
`deterministic.entrypoints` still types all four class-library `.csproj`
files as `"cli"`, and `Microsoft.AspNetCore.SpaProxy` still resolves to
`"version": null`. Neither was in scope of the two bugs this re-distill
fixed. Left untouched per the same hard rule; README continues to override
both (real entrypoints only; SpaProxy's true 9.0.5 version noted where it
matters).

**Interpretive fields reverted on regeneration, re-corrected.** Regenerating
the distillate reset `interpretive.integration_points` to the distiller's raw
output (`MediatR`, `FluentValidation`, `Mapster`, `Entity Framework Core with
SQLite`) — all in-process library usage, not external systems per the
schema's field definition. Re-corrected to the two genuine external
integration points already established in `docs/architecture.md` (the SQLite
database via EF Core, and the `movies.client` frontend SPA consuming the API
over HTTP).

### 2026-08-16 — Overnight Code Review (Opus high)
- **Status:** Open
- **Standard:** `conventions/repo-distillate-schema.md` —
  `interpretive.integration_points[]` maps to "§ `docs/architecture.md` (External
  integration points)", i.e. external systems only (verified at
  `repo-distillate-schema.md:59-60`). Note: `conventions/code-quality.md` is
  scoped "all stacks (code, not docs)" and so does not govern these doc artifacts
  — no code-quality violations are in scope. The two hand-authored deliverables
  reviewed (`README.md`, `docs/architecture.md`) have **no violations found**.

Reviewed the full branch diff vs `master` (`.ai/distillate.json`,
`.ai/distillation-ollama.md`, `.ai/findings.md`, `README.md`,
`docs/architecture.md`) at Opus-high effort — correctness, security, missed edge
cases, STANDARDS compliance.

**Verification (README.md + docs/architecture.md — all claims checked against
source, all confirmed):**
- `net8.0` across all five `.csproj` files.
- All five routes (`GET/POST/PUT/DELETE /api/movies`, `GET /api/movies/{id}`)
  match `MoviesEndPoints.cs`.
- CORS origin `https://localhost:58292` and SpaProxy `9.0.5` match
  `Program.cs` / csproj.
- Connection string `Data Source=movies.db` (relative path) matches
  `appsettings.json`; documented by name only, value never exposed.
- `movies.db` + `-shm`/`-wal` sidecars are git-tracked (matches gotcha).
- Named types (`MovieDto`, `CreateMovieRequest`, `UpdateMovieRequest`,
  `GetMoviesResponse`, `ValidationError`, `CustomValidationException`,
  `BaseEntity`, `Movie`, and the source-typo `NotFoundExecption`) all exist as
  documented.
- Dependency count 41; MUI/emotion imported in `App.tsx` + movie components,
  `semantic-ui-css` in `main.tsx`, `semantic-ui-react`/`semver`/`yarn` unused —
  all match.
- `launchSettings.json` profiles (IIS Express / Kestrel http+https, all local)
  match "no operational surface."
- Security: no secrets, connection-string values, ARNs, or tokens present in the
  diff (checklist item from `code-quality.md` § Code review checklist satisfied).

**Single finding (low severity, borderline):**
- **`.ai/distillation-ollama.md:24-29`** — the `## Integration Points` section
  lists in-process libraries (`MediatR`, `FluentValidation`, `Mapster`,
  `Entity Framework Core with SQLite`) as integration points. This contradicts
  the corrected canonical views in `distillate.json` and `docs/architecture.md`,
  which name only the two genuine external integrations (SQLite DB via EF Core,
  frontend SPA over HTTP), per the `repo-distillate-schema.md` field definition
  (`integration_points` → "External integration points").
  - *Mitigation:* the file carries a `Do not hand-edit` header and is an explicit
    raw machine-generated provenance artifact from `ops_tools.repo_distiller`
    (model `qwen2.5-coder:14b`). The downstream re-correction is already recorded
    in this findings file (see the "Interpretive fields reverted on regeneration"
    note directly above). No reader-facing canonical doc is wrong.
  - *Recommendation:* leave the artifact untouched (hand-editing it violates its
    own contract and the verify-stage hard rule against editing distiller output);
    resolution belongs in `ops_tools.repo_distiller`'s integration_points
    prompt/logic, tracked as the existing distiller parser/prompt gaps in this
    file. No action required on the reviewed branch.

**Conclusion:** the branch is accurate and safe to merge as documentation. No
correctness, security, or edge-case defects in the hand-authored deliverables.
