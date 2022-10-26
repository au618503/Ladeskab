using Cabinet_Library.Logger;
using System.Net.Sockets;

namespace UnitTests.LoggerTests;

public class TestLogFile
{
    private const int id = 5;
    private LogFile _logger;
    [SetUp]
    public void Setup()
    {
        _logger = new LogFile();
    }

    [Test]
    public void TestLog_FileSaved()
    {
        _logger.LogDoorLocked(id);
        bool exists = (File.Exists(LogFile.FileName));
        Assert.IsTrue(exists);
    }

    [Test]
    public void TestLog_DoorLocked_LogCorrect()
    {
        _logger.LogDoorLocked(id);
        string logcontents = File.ReadAllText(LogFile.FileName);
        bool exists = logcontents.Contains("Cabinet locked. \nRFID: " + id);
        Assert.IsTrue(exists);
    }

    [Test]
    public void TestLog_DoorUnlocked_LogCorrect()
    {
        _logger.LogDoorUnlocked(id);
        string logcontents = File.ReadAllText(LogFile.FileName);
        bool exists = logcontents.Contains("Cabinet unlocked. \nRFID: " + id);
        Assert.IsTrue(exists);
    }
}