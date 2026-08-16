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
