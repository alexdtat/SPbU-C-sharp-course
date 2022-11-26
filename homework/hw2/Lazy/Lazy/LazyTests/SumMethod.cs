namespace LazyTests;

/// <summary>
/// Class for generating array of ints and summing them. Used in tests.
/// </summary>
public class SumMethod
{
    private const int Size = 10;
    private const int Min = -100;
    private const int Max = 100;
    private readonly List<int> _list;
    public readonly int Expected;

    /// <summary>
    /// Constructing class with array of random ints and evaluating their sum.
    /// </summary>
    public SumMethod()
    {
        var random = new Random();
        _list = Enumerable.Range(0, Size).Select(n => random.Next(Min, Max)).ToList();
        Expected = _list.Sum();
    }

    /// <summary>
    /// Evaluating sum of array' ints (this method is being used as lazy evaluating in tests).
    /// </summary>
    /// <returns></returns>
    public int GetSum()
    {
        var result = 0;
        foreach (var num in _list)
        {
            result += num;
        }

        return result;
    }
}
