using System;
using static Cabinet_Library.Door;


namespace Cabinet_Library
{
    public class DoorEventArgs : EventArgs
    {
        public bool IsOpen { set; get; }
        public bool IsLocked { set; get; }
    }

    public interface IDoor
    {
        // Event triggered on door opened or closed
        event EventHandler<DoorEventArgs> DoorEvent;

        // Direct access to the door status
        bool DoorIsOpen { get; }
        bool DoorIsLocked { get; }
        int DoorClosedEvent { get; set; }

        void OnDoorOpen();
        void OnDoorClose();
        void LockDoor();
        void UnlockDoor();
    }

    public class Door : IDoor
    {
        
        public Door()
        {
            DoorIsOpen = false;
            DoorIsLocked = false;
        }

        public enum Doorstate
        {
            Locked,
            Unlocked
        }
        public event EventHandler<DoorEventArgs> DoorEvent;
        public bool DoorIsOpen { get; private set; }
        public bool DoorIsLocked { get; private set; }
        
     

        public void OnDoorOpen()
        {
            DoorIsOpen = true;
            OnNewDoorStatus();
        }
        public void OnDoorClose()
        {
            DoorIsOpen = false;
            OnNewDoorStatus();
        }
        public void LockDoor()
        {
            DoorIsLocked = true;
            OnNewDoorStatus();
        }
        public void UnlockDoor()
        {
            DoorIsLocked = false;
            OnNewDoorStatus();
        }
        private void OnNewDoorStatus()
        {
            DoorEvent?.Invoke(this, new DoorEventArgs() { IsOpen = DoorIsOpen, IsLocked = DoorIsLocked });
        }
        private void ChangeDoorState(Door door)
        {
            if (_doorEvent == DoorState.Unlocked)
            {
                DoorOpened();

            }
            else
            {
                DoorClosed();

            }
        }
        
        
}
