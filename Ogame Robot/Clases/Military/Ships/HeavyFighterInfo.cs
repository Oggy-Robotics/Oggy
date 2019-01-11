namespace Ogame_Robot.Clases.Military
{
    class HeavyFighterInfo : UnitInfo {
        const int BaseSpeed = 10000;

        public override UnitType Type {
            get { return UnitType.HeavyFighter; }
        }

        public override int WeaponPower {
            get { return 150; }
        }

        public override int ShieldPower {
            get { return 25; }
        }

        public override int StructuralIntegrity {
            get { return 10000; }
        }

        public override int Capacity {
            get { return 100; }
        }

        public override Resources Cost {
            get { return new Resources( 6000, 4000, 0 ); }
        }

        public override int Value {
            get { return 10; }
        }


        public override int GetRapidFire( UnitType target ) {
            switch( target ) {
                case UnitType.EspProbe:
                case UnitType.SolarSat:
                    return 5;
                case UnitType.SmallCargo:
                    return 3;
                default:
                    return 1;
            }
        }


        public override int GetBaseSpeed( PlayerMilitary player ) {
            return BaseSpeed;
        }


        public override int GetActualSpeed( PlayerMilitary player ) {
            return (int)(BaseSpeed*(1 + player.ImpulseTech*.2m));
        }


        public override int GetFuelConsumption( PlayerMilitary player ) {
            return 75;
        }
    }
}