using BenchmarkDotNet.Running;
using Factorio.Modding.Api.Json.Benchmarks;

BenchmarkRunner.Run<JsonSerializerBenchmarks>();

//JsonSerializerOptions _sourceGenOptions = new SourceGenerationOptions().Options;
//JsonSerializerOptions _reflectionOptions = new ReflectionBasedOptions().Options;

//string _jsonApi = File.ReadAllText("ApiDocs\\prototype-api.json");

//Stopwatch watch = new Stopwatch();
//watch.Start();

//for (int i = 0; i < 100; i++)
//{
//    _ = JsonSerializer.Deserialize<PrototypeApi>(_jsonApi, _reflectionOptions);
//}
//watch.Stop();

//Console.WriteLine(watch.Elapsed);

//Console.ReadLine();