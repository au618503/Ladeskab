using Cabinet_Library.ChargeControl;
using Cabinet_Library.Display;
using Cabinet_Library.Door;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Cabinet_Library.StationControl.States
{
    public class StationStateBase
    {
        public readonly StationStateID StateID;
        protected IStationControl Context;
        protected IChargeControl ChargeControl;
        protected IDisplay Display;
        protected IDoor Door;
        protected int? SavedId = null;
        public virtual void OnDoorOpened() { }
        public virtual void OnDoorClosed() { }
        public virtual void OnRfidDetected(int id) { }

        public StationStateBase(IStationControl context, IChargeControl chargeControl, IDisplay display, IDoor door, int? savedId)
        {
            Context = context;
            ChargeControl = chargeControl;
            Display = display;
            Door = door;
            SavedId = savedId;
        }
    }
}
