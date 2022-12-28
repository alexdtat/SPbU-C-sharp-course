// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using MultiThreadedQuicksort.ThreadedQuicksort;

const int min = 0;
const int max = 1000;
const int size = 1024000;
const int singleThread = 1;

var multiThreadedQuicksort = new ThreadedQuicksort<int>();
var singleThreadedQuicksort = new ThreadedQuicksort<int>(singleThread);

var randNum = new Random();
var arrayForMultiThreaded = Enumerable
    .Repeat(0, size)
    .Select(i => randNum.Next(min, max))
    .ToArray();
var arrayForSingleThreaded = new int[size];
arrayForMultiThreaded.CopyTo(arrayForSingleThreaded, 0);

var multiThreadedWatch = new Stopwatch();
multiThreadedWatch.Start();
var multiThreaded = multiThreadedQuicksort.Quicksort(arrayForMultiThreaded);
await multiThreaded.ConfigureAwait(false);
multiThreadedWatch.Stop();

var singleThreadedWatch = new Stopwatch();
singleThreadedWatch.Start();
var singleThreaded = singleThreadedQuicksort.Quicksort(arrayForSingleThreaded);
await singleThreaded.ConfigureAwait(false);
singleThreadedWatch.Stop();

// Array.ForEach(arrayForMultiThreaded, Console.WriteLine);
Console.WriteLine(arrayForMultiThreaded.SequenceEqual(arrayForSingleThreaded)
    ? "Arrays are equal!"
    : "ERROR: Arrays aren't equal!");

Console.WriteLine("Multi threaded time: {0}", multiThreadedWatch.Elapsed);
Console.WriteLine("Single threaded time: {0}", singleThreadedWatch.Elapsed);
// Array.ForEach(arrayForMultiThreaded, Console.WriteLine);
// Array.ForEach(arrayForSingleThreaded, Console.WriteLine);
