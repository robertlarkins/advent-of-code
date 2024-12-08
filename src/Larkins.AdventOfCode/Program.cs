// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Larkins.AdventOfCode.AdventOfCode2024.Day06GuardGallivant;
using Larkins.AdventOfCode.Utilities;

BenchmarkRunner.Run<BenchmarkClass>();

[MemoryDiagnoser]
public class BenchmarkClass
{
    private IEnumerable<string> input;
    
    public BenchmarkClass()
    {
        var inputFilePath = @"";
        var reader = new TextFileReader();
        input = reader.ReadTextToSeparateLines(inputFilePath);
    }
    
    [Benchmark]
    public void RunPart1()
    {
        var solver = new Year2024Day06Part01Solver(input);
        solver.Solve();
    }
    
    [Benchmark]
    public void RunPart2()
    {
        var solver = new Year2024Day06Part02Solver(input);
        solver.Solve();
    }
}
