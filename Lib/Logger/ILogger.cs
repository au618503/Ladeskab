using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet_Library.Logger;

public interface ILogger
{
    public void LogDoorLocked(int id);
    public void LogDoorUnlocked(int id);
    public void LogChargingError(double currentValue);
}

