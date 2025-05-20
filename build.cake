#addin "nuget:?package=Cake.MinVer&version=3.0.0"
#addin "nuget:?package=Cake.Args&version=3.0.0"

var target = ArgumentOrDefault<string>("Target") ?? "Default";
var buildVersion = MinVer(s => s.WithTagPrefix("v").WithDefaultPreReleasePhase("preview"));

Task("Clean")
    .Does(() =>
{
    EnsureDirectoryDoesNotExist("./artifact/");
    CleanDirectories("./**/^{bin,obj}");
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetRestore("./Cake.GitHub.Endpoints.sln", new DotNetRestoreSettings
    {
        LockedMode = true,
    });
});

Task("Build")
    .IsDependentOn("Restore")
    .DoesForEach(new[] { "Debug", "Release" }, (configuration) =>
{
    DotNetBuild("./Cake.GitHub.Endpoints.sln", new DotNetBuildSettings
    {
        Configuration = configuration,
        NoRestore = true,
        NoIncremental = false,
        MSBuildSettings = new DotNetMSBuildSettings
        {
            Version = buildVersion.Version,
            AssemblyVersion = buildVersion.AssemblyVersion,
            FileVersion = buildVersion.FileVersion,
            ContinuousIntegrationBuild = BuildSystem.IsLocalBuild,
        },
    });
});

Task("Pack")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetPack("./src/Cake.GitHub.Endpoints/Cake.GitHub.Endpoints.csproj", new DotNetPackSettings
    {
        Configuration = "Release",
        NoRestore = true,
        NoBuild = true,
        OutputDirectory = "./artifact/nuget",
        MSBuildSettings = new DotNetMSBuildSettings
        {
            Version = buildVersion.Version,
            PackageReleaseNotes = $"https://github.com/louisfischer/Cake.GitHub.Endpoints/releases/tag/v{buildVersion.Version}"
        }
    });
});

Task("Push")
    .IsDependentOn("Pack")
    .WithCriteria(() => ArgumentOrDefault<bool>("NUGET_PUSH"))
    .Does(context =>
{
    var url = context.ArgumentOrDefault<string>("NUGET_URL");
    if (string.IsNullOrWhiteSpace(url))
    {
        context.Information("No NuGet URL specified. Skipping publishing of NuGet packages");
        return;
    }

    var apiKey = context.ArgumentOrDefault<string>("NUGET_API_KEY");
    if (string.IsNullOrWhiteSpace(apiKey))
    {
        context.Information("No NuGet API key specified. Skipping publishing of NuGet packages");
        return;
    }

    var nugetPushSettings = new DotNetNuGetPushSettings
    {
        Source = url,
        ApiKey = apiKey,
    };

    foreach (var nugetPackageFile in GetFiles("./artifact/nuget/*.nupkg"))
        DotNetNuGetPush(nugetPackageFile.FullPath, nugetPushSettings);
});

Task("Publish")
    .IsDependentOn("Push")
    .WithCriteria(() => GetFiles("./artifact/nuget/**/*")?.Count > 0)
    .WithCriteria(() => GitHubActions.IsRunningOnGitHubActions)
    .WithCriteria(() => string.Equals("refs/heads/main", GitHubActions.Environment.Workflow.Ref, StringComparison.OrdinalIgnoreCase))
    .Does(async () =>
        await GitHubActions.Commands.UploadArtifact(Directory("./artifact/nuget"), $"Cake.GitHub.Endpoints.{buildVersion.Version}"));

Task("Default")
    .IsDependentOn("Publish");

RunTarget(target);
