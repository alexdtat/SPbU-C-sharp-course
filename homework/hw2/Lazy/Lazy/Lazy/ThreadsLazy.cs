namespace Lazy;

/// <summary>
/// Lazy evaluation class for multi-threaded usage.
/// </summary>
/// <typeparam name="T">Evaluation's returning value's type.</typeparam>
public class ThreadsLazy<T> : ILazy<T>
{
    private readonly object _lockObject = new();
    private T? _evaluationResult;
    private bool _isEvaluated;
    private readonly Func<T?> _supplier;

    /// <summary>
    /// Constructs lazy evaluation of given method.
    /// </summary>
    /// <param name="supplier">Method to evaluate.</param>
    public ThreadsLazy(Func<T?> supplier)
    {
        _supplier = supplier;
    }

    /// <summary>
    /// Getting nullable result of lazy evaluation. If it were not started, starts and returns the result.
    /// Otherwise returns result of the first evaluation. Guarantees safety in multi-threaded usage.
    /// </summary>
    /// <returns>Nullable laz evaluation result.</returns>
    public T? Get()
    {
        // Outer check is needed to not lock thread if evaluation is already completed and it's result saved.
        if (_isEvaluated) return _evaluationResult;
        lock (_lockObject)
        {
            // Inner check is needed if first check was false,
            // but waiting threads after first evaluation got inside lock.
            if (_isEvaluated) return _evaluationResult;
            _evaluationResult = _supplier.Invoke();
            _isEvaluated = true;

            return _evaluationResult;
        }
    }
}
