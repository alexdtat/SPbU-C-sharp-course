using task1;

namespace task1Tests;

public class Tests
{
    [Test]
    public void DefaultTest()
    {
        var array = new [] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        var expected = new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Assert.That(expected, Is.EqualTo(BubbleSort.Sort(array)));
    }

    [Test]
    [Repeat(25)]
    public void RandomTest()
    {
        const int size = 10;
        const int min = -100;
        const int max = 100;
        var array = new int[size];
        var expected = new int[size];
        var randomNumber = new Random();
        for(var i = 0; i < array.Length; i++)
        {
            array[i] = randomNumber.Next(min, max);
            expected[i] = array[i];
        }
        
        Array.Sort(expected);
        Assert.That(expected, Is.EqualTo(BubbleSort.Sort(array)));
    }
}