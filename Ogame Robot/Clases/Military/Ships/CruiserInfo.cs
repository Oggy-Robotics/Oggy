namespace Ogame_Robot.Clases.Military
{
    class CruiserInfo : UnitInfo
    {
        const int BaseSpeed = 15000;

        public override UnitType Type {
            get { return UnitType.Cruiser; }
        }

        public override int WeaponPower {
            get { return 400; }
        }

        public override int ShieldPower {
            get { return 50; }
        }

        public override int StructuralIntegrity {
            get { return 27000; }
        }

        public override int Capacity {
            get { return 800; }
        }

        public override Resources Cost {
            get { return new Resources( 20000, 7000, 2000 ); }
        }

        public override int Value {
            get { return 29; }
        }


        public override int GetRapidFire( UnitType target ) {
            switch( target ) {
                case UnitType.EspProbe:
                case UnitType.SolarSat:
                    return 5;
                case UnitType.LightFighter:
                    return 6;
                case UnitType.RocketLauncher:
                    return 10;
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