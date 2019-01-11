namespace Ogame_Robot.Clases.Military
{
    class BattleshipInfo : ShipUnitInfo {
        const int BaseSpeed = 10000;

        public override UnitType Type {
            get { return UnitType.Battleship; }
        }

        public override int WeaponPower {
            get { return 1000; }
        }

        public override int ShieldPower {
            get { return 200; }
        }

        public override int StructuralIntegrity {
            get { return 60000; }
        }

        public override int Capacity {
            get { return 1500; }
        }

        public override Resources Cost {
            get { return new Resources( 45000, 15000, 0 ); }
        }

        public override int Value {
            get { return 60; }
        }


        public override int GetBaseSpeed( PlayerMilitary player ) {
            return BaseSpeed;
        }


        public override int GetActualSpeed( PlayerMilitary player ) {
            return (int)(BaseSpeed*(1 + player.HyperTech*.3m));
        }


        public override int GetFuelConsumption( PlayerMilitary player ) {
            return 500;
        }
    }
}