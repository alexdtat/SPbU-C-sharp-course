using Lazy;

namespace LazyTests;

/// <summary>
/// Testing Lazy class.
/// </summary>
public class LazyTests
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

        var nullLazy = LazyFactory.CreateLazy(NullTask);
        string? expected = null;
        Assert.That(nullLazy.Get(), Is.EqualTo(expected));
    }

    /// <summary>
    /// Testing with SumMethod (evaluating sum of random ints in a generated array).
    /// The result should remain the same for several consequential invocations.
    /// </summary>
    [Test]
    [Repeat(25)]
    public void SummingTest()
    {
        var sumMethod = new SumMethod();
        var expected = sumMethod.Expected;
        var sumLazy = LazyFactory.CreateLazy(sumMethod.GetSum);
        Assert.Multiple(() =>
            {
                Assert.That(sumLazy.Get(), Is.EqualTo(sumLazy.Get()));
                Assert.That(sumLazy.Get(), Is.EqualTo(expected));
            }
        );
    }
}
