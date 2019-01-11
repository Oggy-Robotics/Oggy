namespace Ogame_Robot.Clases.Military
{
    class RecyclerInfo : ShipUnitInfo {
        const int BaseSpeed = 7500;

        public override UnitType Type {
            get { return UnitType.Recycler; }
        }

        public override int WeaponPower {
            get { return 1; }
        }

        public override int ShieldPower {
            get { return 10; }
        }

        public override int StructuralIntegrity {
            get { return 16000; }
        }

        public override int Capacity {
            get { return 20000; }
        }

        public override Resources Cost {
            get { return new Resources( 10000, 6000, 2000 ); }
        }

        public override int Value {
            get { return 4; } // (6+6) * 25%
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