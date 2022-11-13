using task2;

namespace task2Tests;

public class Tests
{
    private static object[] _testData =
    {
        new object[]
        {
            "ABACABA", "BCABAAA"
        },
        new object[]
        {
            "The fluttering of eyelids in the spring sun. A thought that arises, only to disappear again. And yet there's a pattern emerging...",
            "..srtonfesg,aegtntAydesnng..  .  gse hpn ihhrpy hmttsy o nnnaurtTtttgl arrgdrefneriuiAiiiot  h pasaeeepae'deii  ahett     uaoslle "
        },
        new object[]
        {
            "32647421146735", "21435771632464"
        },
        new object[]
        {
            "7vFIm50sZNS0we7vpDQ1", "5SQm1epvFZDNswIv0770"
        },
        new object[]
        {
            "", ""
        }
    };

    [Test]
    [TestCaseSource(nameof(_testData))]
    public void ParametrizedTest(string inputText, string expectedTransformedText)
    {
        var (transformedText, endPosition) = Bwt.DirectTransformation(inputText);
        Assert.Multiple(() =>
        {
            Assert.That(transformedText, Is.EqualTo(expectedTransformedText));
            Assert.That(Bwt.ReverseTransformation(transformedText, endPosition), Is.EqualTo(inputText));
        });
    }
}
