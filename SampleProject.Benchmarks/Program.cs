using System.Runtime.InteropServices;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

using CliWrap;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

public class RuntimeBenchmarks : BenchmarkBase
{
    // [Benchmark]
    // [BenchmarkCategory("Runtime")]
    // public async Task TUnit_AOT()
    // {
    //     await Cli.Wrap(Path.Combine(UnitPath, $"aot-publish-{Framework.Replace(".0", "")}", GetExecutableFileName()))
    //         .WithArguments(["--treenode-filter", $"/*/*/{ClassName}/*"])
    //         .WithStandardOutputPipe(PipeTarget.ToStream(OutputStream))
    //         .ExecuteAsync();
    // }

    [Benchmark]
    public async Task TUnit()
    {
        await Cli.Wrap("dotnet")
            .WithArguments(["run", "--no-build", "-c", "Release", "--framework", Framework])
            .WithWorkingDirectory(UnitPath)
            .WithStandardOutputPipe(PipeTarget.ToStream(_outputStream))
            .ExecuteAsync();
    }

    [Benchmark]
    public async Task XUnit()
    {
        await Cli.Wrap("dotnet")
            .WithArguments(["test", "--no-build", "-c", "Release", "--framework", Framework])
            .WithWorkingDirectory(XUnitPath)
            .WithStandardOutputPipe(PipeTarget.ToStream(_outputStream))
            .ExecuteAsync();
    }
}

[MarkdownExporterAttribute.GitHub]
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

    // protected string GetExecutableFileName()
    // {
    //     return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "TUnitTimer.exe" : "TUnitTimer";
    // }
}