using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogame_Robot.Clases.Military
{
    abstract class DefenseUnitInfo : UnitInfo {
        public override int GetRapidFire( UnitType target ) {
            return 1;
        }

        public override int Capacity {
            get { return 0; }
        }

        public override int Value {
            get { return 0; }
        }

        public override int GetBaseSpeed(PlayerMilitary player ) {
            return 0;
        }

        public override int GetActualSpeed(PlayerMilitary player ) {
            return 0;
        }

        public override int GetFuelConsumption(PlayerMilitary player ) {
            return 0;
        }
    }
}
