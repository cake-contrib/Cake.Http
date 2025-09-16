# Copilot instructions for Cake.Http

Purpose: help AI coding agents get productive quickly in this repo — build, test, common patterns, and important gotchas.

Quick commands
- `dotnet build` : build the solution (`Cake.Http.slnx`).
- `dotnet test src/Cake.Http.Tests` : run tests; tests target `net8.0;net9.0`.
- Use the Cake script for full CI flow: install Cake.Tool and run `dotnet cake build.cake --target=Tests --configuration=Release`.
- Pack/publish via Cake: `dotnet cake build.cake --target=Push --NUGET_PUSH=true --NUGET_URL=<url> --NUGET_API_KEY=<key>`.

Big picture
- This project implements Cake build aliases that wrap HTTP calls. The runtime surface is a set of static Cake alias methods in `src/Cake.Http/HttpClientAliases.cs` (GET/POST/PUT/PATCH/etc.).
- `HttpSettings` (`src/Cake.Http/HttpSettings.cs`) is the central configuration object used by the aliases and the custom handler.
- `CakeHttpClientHandler` (`src/Cake.Http/CakeHttpClientHandler.cs`) composes `HttpSettings` into an `HttpClientHandler` and performs request/response logging via the `ICakeContext` logger.

Key files to inspect for behavior
- `src/Cake.Http/HttpClientAliases.cs` : public Cake aliases. Keep the `[CakeMethodAlias]` attributes and method signatures stable.
- `src/Cake.Http/HttpSettingsExtensions.cs` : fluent helpers used by consumers: `UseBearerAuthorization`, `SetJsonRequestBody`, `SetMultipartFormDataRequestBody`, `SetFormUrlEncodedRequestBody`, `AppendHeader`, `UseClientCertificates`, `IgnoreServerCertificateErrors`, `EnsureSuccessStatusCode`.
- `src/Cake.Http/CakeHttpClientHandler.cs` : header composition, cookie handling, client cert application, request/response logging, and EnsureSuccessStatusCode handling.
- `src/Cake.Http/JsonEncoder.cs` : serialization helper used by `SetJsonRequestBody`.

Project-specific conventions & important patterns
- Aliases are implemented as synchronous wrappers over async `HttpClient` calls (e.g. using `.GetAwaiter().GetResult()` / `Task.Run(...).ConfigureAwait(false).GetAwaiter().GetResult()`). When modifying code, preserve these synchronous entry points because Cake alias consumers expect sync methods.
- `HttpSettings.RequestBody` is a `byte[]`. Most helpers set it (JSON, form-url-encoded, multipart). Respect that binary contract.
- Logging: `CakeHttpClientHandler` logs request/response at Diagnostic/Verbose via `ICakeContext.Log`. Logging of bodies can be disabled per-request with `SuppressLogResponseRequestBodyOutput()` or the `LogRequestResponseOutput` flag on `HttpSettings`.
- Certificates: add client certs with `UseClientCertificates(...)` and optionally set `IgnoreServerCertificateErrors(true)`. The handler uses `DangerousAcceptAnyServerCertificateValidator` when server-untrusted behavior is requested — document and be careful.
- EnsureSuccessStatusCode behavior: `EnsureSuccessStatusCode()` sets two flags — `EnsureSuccessStatusCode` and `ThrowExceptionOnNonSuccessStatusCode`. The handler will call `response.EnsureSuccessStatusCode()` and will only rethrow when `ThrowExceptionOnNonSuccessStatusCode` is true.

Testing notes
- Tests use xUnit and NSubstitute. Data fixtures are in `src/Cake.Http.Tests/Data` and copied to output (see the test csproj).
- Run `dotnet test src/Cake.Http.Tests --configuration Debug` locally. To exercise a single TF, pass `-f net9.0` etc.
- Integration-like tests use fixtures under `src/Cake.Http.Tests/Fixtures` (e.g. `WebServerFixture`) — inspect these before changing networking/logging behavior.

Common maintenance tasks
- Bumping versions and packaging is handled by `build.cake` using `Cake.MinVer`. When changing public API (aliases, settings), update tests and ensure packaging metadata remains stable.
- Keep `Cake.Core` package version in tests consistent with the project; tests reference `Cake.Core` in the test csproj.

Gotchas and things an agent should not change
- Do not remove or rename `[CakeMethodAlias]` and `[CakeAliasCategory]` attributes — they are the public contract for Cake integration.
- Avoid changing the sync-over-async patterns unless you also update all alias entry points and tests — Cake aliases are expected to be synchronous.
- Be conservative when changing default values (e.g., `LogRequestResponseOutput` defaults to `true`) because tests and consumers rely on them.

Where to look for context
- Start at `src/Cake.Http/HttpClientAliases.cs`, then `HttpSettings`, `HttpSettingsExtensions`, and `CakeHttpClientHandler`.
- Tests: `src/Cake.Http.Tests/Unit` and `src/Cake.Http.Tests/Fixtures` for examples of using `HttpSettings` and how the handler is exercised.

If anything is unclear
- Ask for the intended target (change API, tests, or CI) and whether you should preserve backward compatibility for Cake aliases.
