namespace Ogame_Robot.Clases.Military
{
    class ColonyShipInfo : ShipUnitInfo {
        const int BaseSpeed = 2500;

        public override UnitType Type {
            get { return UnitType.ColonyShip; }
        }

        public override int WeaponPower {
            get { return 50; }
        }

        public override int ShieldPower {
            get { return 100; }
        }

        public override int StructuralIntegrity {
            get { return 30000; }
        }

        public override int Capacity {
            get { return 7500; }
        }

        public override Resources Cost {
            get { return new Resources( 10000, 20000, 10000 ); }
        }

        public override int Value {
            get { return 10; }
        }


        public override int GetBaseSpeed( PlayerMilitary player ) {
            return BaseSpeed;
        }


        public override int GetActualSpeed( PlayerMilitary player ) {
            return (int)(BaseSpeed*(1 + player.ImpulseTech*.2m));
        }


        public override int GetFuelConsumption( PlayerMilitary player ) {
            return 1000;
        }
    }
}