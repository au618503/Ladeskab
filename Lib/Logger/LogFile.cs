namespace Cabinet_Library.Logger
{
    public class LogFile : ILogger
    {
        public const string FileName = "logfile.txt";
        public void LogDoorLocked(int id)
        {
            using (StreamWriter sw = File.AppendText(FileName))
            {
                sw.WriteLine(DateTime.Now + ": Cabinet locked. \nRFID: " + id);
            }
        }
        public void LogDoorUnlocked(int id)
        {
            using (StreamWriter sw = File.AppendText(FileName))
            {
                sw.WriteLine(DateTime.Now + ": Cabinet unlocked. \nRFID: " + id);
            }
        }
    }
}
