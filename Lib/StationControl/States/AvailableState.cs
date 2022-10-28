using Cabinet_Library.ChargeControl;
using Cabinet_Library.Display;
using Cabinet_Library.Door;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet_Library.StationControl.States
{
    public class AvailableState : StationStateBase
    {
        StationStateID stateId = StationStateID.AVAILABLE;

        public AvailableState(IStationControl context, IChargeControl chargeControl, IDisplay display, IDoor door,
            int? savedId)
            : base(context, chargeControl, display, door, savedId)
        {
            StateID = stateId;
        }

        public override void OnDoorOpened()
        {
            Context.ChangeState(new DoorOpenedState(Context, ChargeControl, Display, Door, SavedId));
        }
        public override void OnRfidDetected(int id)
        {
            if (!ChargeControl.DeviceConnected())
            {
                Display.Show("Connection error. Connect your phone.");
            }
            else
            {
                SavedId = id;
                Door.LockDoor();
                ChargeControl.StartCharge();
                Context.LogDoorLocked(id);
                Display.Show("Charging Cabinet Occupied");
                Context.ChangeState(new OccupiedState(Context, ChargeControl, Display, Door, SavedId));
            }
        }
     

    }
}
