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
        
        public override void OnDoorOpen()
        {
            Display.Show("Door opened");
            Context.ChangeState(new StationStateBase());
      
        }
        public override void OnRfidDetected(int id)
        {
            if (ChargeControl.DeviceConnected())
            {
                // Do something
            }
        }
     

    }
}
