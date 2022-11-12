public class PriorityQueueParallel<TPriority, TValue> where TPriority : IComparable
{
    private readonly object _lockObject = new();
    private readonly SortedDictionary<TPriority, Queue<TValue>> _buffer;
    private int _size;

    /// <summary>
    /// Getting size of the queue.
    /// </summary>
    public int Size
    {
        get
        {
            lock (_lockObject)
            {
                return _size;
            }
        }
    }

    public PriorityQueueParallel()
    {
        _buffer = new SortedDictionary<TPriority, Queue<TValue>>();
    }

    /// <summary>
    /// Dequeuing value of element with highest priority. If empty, waiting for enqueuing and then dequeuing.
    /// </summary>
    /// <returns>Value of element with the highest priority.</returns>
    public TValue Dequeue()
    {
        lock (_lockObject)
        {
            if (_size == 0)
            {
                Monitor.Wait(_lockObject);
            }
            
            var result = _buffer[_buffer.Keys.Last()].Dequeue();
            _size--;
            if (_buffer[_buffer.Keys.Last()].Count == 0)
            {
                _buffer.Remove(_buffer.Keys.Last());
            }

            return result;
        }
    }

    /// <summary>
    /// Enqueuing element with given priority.
    /// </summary>
    /// <param name="priority">Priority in queue.</param>
    /// <param name="value">Value to put into the queue.</param>
    public void Enqueue(TPriority priority, TValue value)
    {
        lock (_lockObject)
        {
            if (!_buffer.ContainsKey(priority))
            {
                _buffer.Add(priority, new Queue<TValue>());
            }
            

            _buffer[priority].Enqueue(value);
            _size++;
            
            Monitor.PulseAll(_lockObject);
        }
    }
}
