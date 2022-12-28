using System.Runtime.InteropServices;

namespace MultiThreadedQuicksort.ThreadedQuicksort;

public class ThreadedQuicksort<T> where T : IComparable<T>
{
    private int _threadsResource;
    private readonly object _lockObject = new ();

    public ThreadedQuicksort(int threadsResource = 4)
    {
        _threadsResource = threadsResource;
    }

    public async Task Quicksort(T[] array)
    {
        await Quicksort(array, 0, array.Length - 1);
    }

    private async Task Quicksort(T[] array, int left, int right)
    {
        if (right <= left) return;
        lock (_lockObject)
        {
            Console.Write(_threadsResource + " ");
            _threadsResource--;
        }

        var lowestTemporary = left;
        var greatestTemporary = right;
        var pivot = array[left];
        var i = left + 1;
        while (i <= greatestTemporary)
        {
            var comparisonResult = array[i].CompareTo(pivot);
            switch (comparisonResult)
            {
                case < 0:
                    Swap(array, lowestTemporary++, i++);
                    break;
                case > 0:
                    Swap(array, i, greatestTemporary--);
                    break;
                default:
                    i++;
                    break;
            }
        }

        switch (_threadsResource)
        {
            case > 2:
            {
                var taskLeft = Task.Run(() => Quicksort(array, left, lowestTemporary - 1));
                var taskRight = Task.Run(() => Quicksort(array, greatestTemporary + 1, right));

                // await Task.WhenAll(taskLeft, taskRight).ConfigureAwait(false);
                break;
            }
            case > 1:
            {
                var taskLeft = Task.Run(() => Quicksort(array, left, lowestTemporary - 1));
                SingleThreadedQuicksort(array, greatestTemporary + 1, right);

                // await taskLeft.ConfigureAwait(false);
                break;
            }
            default:
            {
                SingleThreadedQuicksort(array, left, lowestTemporary - 1);
                SingleThreadedQuicksort(array, greatestTemporary + 1, right);
                break;
            }
        }

        lock (_lockObject)
        {
            _threadsResource++;
        }
    }

    private static void Swap(T[] array, int i, int j)
    {
        (array[i], array[j]) = (array[j], array[i]);
    }

    private static void SingleThreadedQuicksort(T[] array, int left, int right)
    {
        while (true)
        {
            if (right <= left) return;

            var lowestTemporary = left;
            var greatestTemporary = right;
            var pivot = array[left];
            var i = left + 1;
            while (i <= greatestTemporary)
            {
                var comparisonResult = array[i].CompareTo(pivot);
                switch (comparisonResult)
                {
                    case < 0:
                        Swap(array, lowestTemporary++, i++);
                        break;
                    case > 0:
                        Swap(array, i, greatestTemporary--);
                        break;
                    default:
                        i++;
                        break;
                }
            }

            SingleThreadedQuicksort(array, left, lowestTemporary - 1);
            left = greatestTemporary + 1;
        }
    }
}
