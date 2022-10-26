namespace Cabinet_Library
{
    public class LogFile
    {
        public void LogDoorLocked(int id)
        {
            using (StreamWriter sw = File.AppendText("logfile.txt"))
            {
                sw.WriteLine(DateTime.Now + ": Skab låst med RFID: " + id);
            }
        }

        public void LogDoorUnlocked(int id)
        {
            using (StreamWriter sw = File.AppendText("logfile.txt"))
            {
                sw.WriteLine(DateTime.Now + ": Skab låst op med RFID: " + id);
            }
        }
    }
}
