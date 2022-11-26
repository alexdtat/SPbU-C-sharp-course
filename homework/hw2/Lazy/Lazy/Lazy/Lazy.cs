namespace Lazy;

/// <summary>
/// Lazy evaluation class for single-threaded usage.
/// </summary>
/// <typeparam name="T">Evaluation's returning value's type.</typeparam>
public class Lazy<T> : ILazy<T>
{
    private readonly Func<T?> _supplier;
    private T? _evaluationResult;
    private bool _isEvaluated;

    /// <summary>
    /// Constructs lazy evaluation of given method.
    /// </summary>
    /// <param name="supplier">Method to evaluate.</param>
    public Lazy(Func<T?> supplier)
    {
        _supplier = supplier;
    }

    /// <summary>
    /// Getting nullable result of lazy evaluation. If it were not started, starts and returns the result.
    /// Otherwise returns result of the first evaluation.
    /// </summary>
    /// <returns>Nullable laz evaluation result.</returns>
    public T? Get()
    {
        if (_isEvaluated) return _evaluationResult;
        _evaluationResult = _supplier.Invoke();
        _isEvaluated = true;

        return _evaluationResult;
    }
}
