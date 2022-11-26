namespace Lazy;

/// <summary>
/// Factory creating single-threaded or multi-threaded lazy evaluation instances.
/// </summary>
public static class LazyFactory
{
    /// <summary>
    /// Creating single-threaded lazy evaluation instance.
    /// </summary>
    /// <param name="supplier">Method to evaluate.</param>
    /// <typeparam name="T">Evaluation's returning value's type.</typeparam>
    /// <returns>Single-threaded lazy evaluation instance of given method.</returns>
    public static Lazy<T> CreateLazy<T>(Func<T> supplier)
    {
        return new Lazy<T>(supplier);
    }

    /// <summary>
    /// Creating multi-threaded lazy evaluation instance.
    /// </summary>
    /// <param name="supplier">Method to evaluate.</param>
    /// <typeparam name="T">Evaluation's returning value's type.</typeparam>
    /// <returns>Multi-threaded lazy evaluation instance of given method.</returns>
    public static ThreadsLazy<T> CreateThreadsLazy<T>(Func<T> supplier)
    {
        return new ThreadsLazy<T>(supplier);
    }
}
