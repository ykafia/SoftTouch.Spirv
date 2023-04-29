// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet;
using BenchmarkDotNet.Running;
using SoftTouch.Spirv.Internals.Benchmarks;

BenchmarkRunner.Run<ParserBench>();

Console.WriteLine("Hello, World!");


