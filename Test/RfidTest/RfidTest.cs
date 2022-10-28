using Cabinet_Library.RfIdReader;
using NUnit.Framework;

namespace UnitTests.RfidTest;

[TestFixture]
public class RfidTest
{
    private RfidReader _uut = new RfidReader();
    private RfidEventArgs args;
    [SetUp]
    public void Setup()
    {
        _uut.RfidEvent += (o, e) => { args = e; };
    }

    [Test]
    [TestCase(123)]
    [TestCase(10000)]
    [TestCase(-9999)]
    public void EventRaised_ReceivedCorrectArgs(int id)
    {
        _uut.SimulateRfidDetected(id);
        Assert.IsTrue(args.Rfid == id);
    }
}
 