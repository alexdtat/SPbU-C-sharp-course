using Lazy;

namespace LazyTests;

/// <summary>
/// Testing ThreadedLazy class.
/// </summary>
public class ThreadsLazyTests
{
    /// <summary>
    /// Testing null evaluation.
    /// </summary>
    [Test]
    public void NullTest()
    {
        string? NullTask()
        {
            return null;
        }

        var nullLazy = LazyFactory.CreateThreadsLazy(NullTask);
        string? expected = null;
        Assert.That(nullLazy.Get(), Is.EqualTo(expected));
    }

    /// <summary>
    /// Testing in different Tasks with SumMethod (evaluating sum of random ints in a generated array).
    /// </summary>
    [Test]
    [Repeat(25)]
    public async Task ResultsTest()
    {
        const int numOfTasks = 10;
        var sumMethod = new SumMethod();
        var threadsLazySum = LazyFactory.CreateThreadsLazy(sumMethod.GetSum);
        var firstTask = new Task<int>(() =>
            {
                var result = threadsLazySum.Get();
                Thread.Sleep(500);
                return result;
            }
        );
        firstTask.Start();

        var otherTasks = new Task<int>[numOfTasks];
        var results = new int[numOfTasks];
        for (var i = 0; i < numOfTasks; i++)
        {
            otherTasks[i] = new Task<int>(() => threadsLazySum.Get());
        }

        foreach (var task in otherTasks)
        {
            task.Start();
        }

        for (var i = 0; i < numOfTasks; i++)
        {
            results[i] = await otherTasks[i];
        }

        var firstResult = await firstTask;

        Assert.Multiple(() =>
        {
            Assert.That(sumMethod.Expected, Is.EqualTo(firstResult));
            foreach (var result in results)
            {
                Assert.That(result, Is.EqualTo(firstResult));
            }
        });
    }

    /// <summary>
    /// Testing races during getting random numbers combined with Thread.Sleep() in different Tasks.
    /// Result of the first started task should be final result.
    /// </summary>
    [Test]
    [Repeat(25)]
    public async Task RacesTest()
    {
        const int numOfTasks = 10;
        var random = new Random();
        var threadsLazyRandom = LazyFactory.CreateThreadsLazy(() =>
        {
            Thread.Sleep(1000);
            return random.Next(100);
        });
        var firstTask = new Task<int>(() => threadsLazyRandom.Get());
        var otherTasks = new Task<int>[numOfTasks];
        var results = new int[numOfTasks];
        for (var i = 0; i < numOfTasks; i++)
        {
            otherTasks[i] = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                return threadsLazyRandom.Get();
            });
        }

        firstTask.Start();
        foreach (var task in otherTasks)
        {
            task.Start();
        }


        var firstResult = await firstTask;
        for (var i = 0; i < numOfTasks; i++)
        {
            results[i] = await otherTasks[i];
        }

        Assert.Multiple(() =>
        {
            foreach (var result in results)
            {
                Assert.That(result, Is.EqualTo(firstResult));
            }
        });
    }
}
