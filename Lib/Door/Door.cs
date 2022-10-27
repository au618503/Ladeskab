using System;
using static Cabinet_Library.Door.Door;


namespace Cabinet_Library.Door
{
    public class DoorEventArgs : EventArgs
    {
        public bool IsOpen { set; get; }
    }

    public class Door : IDoor
    {
        public bool DoorIsOpen { get; private set; }
        public bool DoorIsLocked { get; private set; }
        public Door()
        {
            DoorIsOpen = false;
            DoorIsLocked = false;
        }

        public event EventHandler<DoorEventArgs> DoorEvent;

        public void SimulateDoorOpened()
        {
            if (!DoorIsLocked)
            {
                DoorIsOpen = true;
                DoorEvent.Invoke(this, new DoorEventArgs() { IsOpen = DoorIsOpen });
            }
        }
        public void SimulateDoorClosed()
        {
            if (DoorIsOpen)
            {
                DoorIsOpen = false;
                DoorEvent.Invoke(this, new DoorEventArgs() { IsOpen = DoorIsOpen });
            }
        }
        
        

        public void LockDoor()
        {
            if (!DoorIsOpen)
            {
                DoorIsLocked = true;
            }
        }

        public void UnlockDoor()
        {
            if (!DoorIsOpen)
            {
                DoorIsLocked = false;
            }
        }
    }
}
