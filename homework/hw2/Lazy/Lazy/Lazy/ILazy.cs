namespace Lazy;

/// <summary>
/// Interface for lazy evaluations.
/// </summary>
/// <typeparam name="T">Evaluation's returning value's type.</typeparam>
public interface ILazy<out T>
{
    /// <summary>
    /// Getting nullable result of lazy evaluation. If it were not started, starts and returns the result.
    /// Otherwise returns result of the first evaluation.
    /// </summary>
    /// <returns>Nullable laz evaluation result.</returns>
    T? Get();
}
