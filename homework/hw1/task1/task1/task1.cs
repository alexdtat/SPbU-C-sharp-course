using task1;

Console.WriteLine("Please, input array for sorting. Use spaces as delimiters:");
try
{
    var array = Console.ReadLine()
        ?.Trim()
        .Split()
        .Select(int.Parse)
        .ToArray();

    if (array != null)
    {
        Console.WriteLine("Sorted array:");
        foreach (var element in BubbleSort.Sort(array))
        {
            Console.Write(element + " ");
        }
    }
    else
    {
        Console.Write("Null input");
    }
}
catch (FormatException e)
{
    Console.WriteLine(e);
}
catch (ArgumentNullException e)
{
    Console.WriteLine(e);
}
