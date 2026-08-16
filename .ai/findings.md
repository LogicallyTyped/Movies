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
