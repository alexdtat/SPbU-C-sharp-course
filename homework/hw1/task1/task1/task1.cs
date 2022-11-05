// See https://aka.ms/new-console-template for more information

using task1;

try
{
    var array = Console.ReadLine()
        ?.Trim()
        .Split()
        .Select(int.Parse)
        .ToArray();

    if (array != null)
    {
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