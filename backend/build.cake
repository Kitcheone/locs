//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var framework = Argument("framework", "netcoreapp2.0");
var runtime = Argument("runtime", "win10-x64");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var projectDir = Directory("./src/Locs.Api");
var buildDir = Directory("./src/Locs.Api/bin") + Directory(configuration);
var objectDir = Directory("./src/Locs.Api/obj") + Directory(configuration);
var publishDir = Directory("../../publish");

// Define projects
var projectFile = File("./src/Locs.Api/Locs.Api.csproj");
var testProjectFile = File("./src/Locs.Api.Tests/Locs.Api.Tests.csproj");

// Addins
#addin "Cake.WebDeploy"

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
    CleanDirectory(objectDir);
    CleanDirectory(publishDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore(projectFile);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
        Framework = framework,
        Configuration = configuration,
        OutputDirectory = buildDir
    };

    DotNetCoreBuild(projectFile, settings);
});

Task("Publish")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
    {
    var settings = new DotNetCoreBuildSettings
    {
        Framework = framework,
        Configuration = configuration,
        OutputDirectory = buildDir,
            MSBuildSettings = new DotNetCoreMSBuildSettings()
                .WithProperty("OutputPath", publishDir)
                .WithProperty("DeployOnBuild", "true")
                .WithProperty("WebPublishMethod", "Package")
                .WithProperty("PackageAsSingleFile", "true")
    };

    DotNetCoreBuild(projectFile, settings);
    });

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var settings = new DotNetCoreTestSettings
    {

    };

    DotNetCoreTest(testProjectFile, settings);
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////


Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
