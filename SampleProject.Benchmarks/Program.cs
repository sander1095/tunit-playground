using System.Runtime.InteropServices;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

using CliWrap;

BenchmarkRunner.Run(typeof(Program).Assembly);

public class RuntimeBenchmarks : BenchmarkBase
{
    [Benchmark]
    public async Task TUnit_AOT()
    {
        // Build the TUnit project using `dotnet publish -c Release --framework net9.0 --output aot-publish-net9 --property:Aot=true --runtime win-x64`
        // Change the runtime to your desired platform!
        await Cli.Wrap(Path.Combine(UnitPath, $"aot-publish-{Framework.Replace(".0", "")}", GetExecutableFileName()))
            .WithStandardOutputPipe(PipeTarget.ToStream(_outputStream))
            .ExecuteAsync();
    }

    [Benchmark]
    public async Task TUnit()
    {
        // Build the TUnit project using `dotnet build -c Release --framework net9.0`
        // Change the runtime to your desired platform!
        await Cli.Wrap("dotnet")
            .WithArguments(["run", "--no-build", "-c", "Release", "--framework", Framework])
            .WithWorkingDirectory(UnitPath)
            .WithStandardOutputPipe(PipeTarget.ToStream(_outputStream))
            .ExecuteAsync();
    }

    [Benchmark]
    public async Task XUnit()
    {
        // Build the XUnit project using `dotnet build -c Release --framework net9.0`
        // Change the runtime to your desired platform!
        await Cli.Wrap("dotnet")
            .WithArguments(["test", "--no-build", "-c", "Release", "--framework", Framework])
            .WithWorkingDirectory(XUnitPath)
            .WithStandardOutputPipe(PipeTarget.ToStream(_outputStream))
            .ExecuteAsync();
    }
}

[SimpleJob(RuntimeMoniker.Net90)]
public class BenchmarkBase
{
    protected readonly Stream _outputStream = Console.OpenStandardOutput();

    protected static readonly string UnitPath = GetProjectPath("SampleProject.TUnit");
    protected static readonly string XUnitPath = GetProjectPath("SampleProject.XUnit");

    protected static readonly string Framework = GetFramework();

    private static string GetFramework()
    {
        return $"net{Environment.Version.Major}.{Environment.Version.Minor}";
    }

    private static string GetProjectPath(string name)
    {
        var folder = new DirectoryInfo(Environment.CurrentDirectory);

        while (folder.Name != "tunit-playground")
        {
            folder = folder.Parent!;
        }

        return Path.Combine(folder.FullName, name);
    }

    protected string GetExecutableFileName()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "SampleProject.TUnit.exe" : "SampleProject.TUnit.exe";
    }
}