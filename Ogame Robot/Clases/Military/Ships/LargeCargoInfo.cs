namespace Ogame_Robot.Clases.Military
{
    class LargeCargoInfo : ShipUnitInfo {
        const int BaseSpeed = 7500;

        public override UnitType Type {
            get { return UnitType.LargeCargo; }
        }

        public override int WeaponPower {
            get { return 5; }
        }

        public override int ShieldPower {
            get { return 25; }
        }

        public override int StructuralIntegrity {
            get { return 12000; }
        }

        public override int Capacity {
            get { return 25000; }
        }

        public override Resources Cost {
            get { return new Resources( 6000, 6000, 0 ); }
        }

        public override int Value {
            get { return 3; } // (6+6) * 25%
        }


        public override int GetBaseSpeed( PlayerMilitary player ) {
            return BaseSpeed;
        }


        public override int GetActualSpeed( PlayerMilitary player ) {
            return (int)(BaseSpeed*(1 + player.CombustTech*.1m));
        }


        public override int GetFuelConsumption( PlayerMilitary player ) {
            return 50;
        }
    }
}