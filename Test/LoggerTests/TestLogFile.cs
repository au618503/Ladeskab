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
        if (File.Exists(LogFile.FileName))
        {
            File.Delete(LogFile.FileName);
        }
        _logger.LogDoorLocked(id);
        bool exists = (File.Exists(LogFile.FileName));
        Assert.IsTrue(exists);
    }

    [Test]
    public void TestLog_DoorLocked_LogCorrect()
    {
        if (File.Exists(LogFile.FileName))
        {
            File.Delete(LogFile.FileName);
        }
        _logger.LogDoorLocked(id);
        string logcontents = File.ReadAllText(LogFile.FileName);
        bool exists = logcontents.Contains("Cabinet locked. \nRFID: " + id);
        Assert.IsTrue(exists);
    }

    [Test]
    public void TestLog_DoorUnlocked_LogCorrect()
    {
        if (File.Exists(LogFile.FileName))
        {
            File.Delete(LogFile.FileName);
        }
        _logger.LogDoorUnlocked(id);
        string logcontents = File.ReadAllText(LogFile.FileName);
        bool exists = logcontents.Contains("Cabinet unlocked. \nRFID: " + id);
        Assert.IsTrue(exists);
    }

    [Test]
    public void TestLog_ErrorLogged()
    {
        if (File.Exists(LogFile.FileName))
        {
            File.Delete(LogFile.FileName);
        }

        double current = 1420.42;
        _logger.LogChargingError(current);
        string logcontents = File.ReadAllText(LogFile.FileName);
        bool exists = logcontents.Contains("Charging error. Current value: " + current);
        Assert.IsTrue(exists);
    }
}