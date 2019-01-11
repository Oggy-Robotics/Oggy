namespace Ogame_Robot.Clases.Military
{
    class EspProbeInfo : ShipUnitInfo {
        const int BaseSpeed = 100000000;

        public override UnitType Type {
            get { return UnitType.EspProbe; }
        }

        public override int WeaponPower {
            get { return 0; }
        }

        public override int ShieldPower {
            get { return 0; }
        }

        public override int StructuralIntegrity {
            get { return 1000; }
        }

        public override int Capacity {
            get { return 0; }
        }

        public override Resources Cost {
            get { return new Resources( 0, 1000, 0 ); }
        }

        public override int Value {
            get { return 0; }
        }


        public override int GetRapidFire( UnitType target ) {
            return 1;
        }


        public override int GetBaseSpeed( PlayerMilitary player ) {
            return BaseSpeed;
        }


        public override int GetActualSpeed( PlayerMilitary player ) {
            return (int)(BaseSpeed*(1 + player.CombustTech*.1m));
        }


        public override int GetFuelConsumption( PlayerMilitary player ) {
            return 1;
        }
    }
}