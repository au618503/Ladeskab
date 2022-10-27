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
        
        
        public virtual void OnDoorOpen() { }
        public virtual void OnDoorClosed() { }

        public virtual void OnRfidDetected(int id) { }
        
        
        
        //         . Systemet aflæser RFID - tagget.Hvis RFID er identisk med det, der blev brugt til at låse skabet med, stoppes opladning, ladeskabets låge låses op og oplåsningen logges.
//9.Brugeren åbner ladeskabet, fjerner ladekablet fra sin telefon og tager telefonen
    }
}
