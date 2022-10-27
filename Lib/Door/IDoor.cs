using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet_Library.Door
{
    public interface IDoor
    {
        // Event triggered on door opened or closed
        event EventHandler<DoorEventArgs> DoorEvent;

        // Direct access to the door status
        bool DoorIsOpen { get; }

        void LockDoor();
        void UnlockDoor();
    }
}
