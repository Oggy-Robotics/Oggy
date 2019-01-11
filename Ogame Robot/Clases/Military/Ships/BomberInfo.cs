namespace Ogame_Robot.Clases.Military
{
    class BomberInfo : UnitInfo {
        const int ImpulseSpeed = 4000;
        const int HyperSpeed = 5000;

        public override UnitType Type {
            get { return UnitType.Bomber; }
        }

        public override int WeaponPower {
            get { return 1000; }
        }

        public override int ShieldPower {
            get { return 500; }
        }

        public override int StructuralIntegrity {
            get { return 75000; }
        }

        public override int Capacity {
            get { return 500; }
        }

        public override Resources Cost {
            get { return new Resources( 50000, 25000, 15000 ); }
        }

        public override int Value {
            get { return 90; } // (2+2) * 25%
        }


        public override int GetRapidFire( UnitType target ) {
            switch( target ) {
                case UnitType.EspProbe:
                case UnitType.SolarSat:
                    return 5;
                case UnitType.RocketLauncher:
                case UnitType.LightLaser:
                    return 20;
                case UnitType.HeavyLaser:
                case UnitType.IonCannon:
                    return 10;
                default:
                    return 1;
            }
        }


        public override int GetBaseSpeed( PlayerMilitary player ) {
            if( player.ImpulseTech >= 8 ) {
                return ImpulseSpeed;
            } else {
                return HyperSpeed;
            }
        }


        public override int GetActualSpeed( PlayerMilitary player ) {
            if( player.HyperTech >= 8 ) {
                return (int)(HyperSpeed * (1 + player.HyperTech * .3m));
            } else {
                return (int)(ImpulseSpeed * (1 + player.ImpulseTech * .2m));
            }
        }


        public override int GetFuelConsumption( PlayerMilitary player ) {
            return 1000;
        }
    }
}