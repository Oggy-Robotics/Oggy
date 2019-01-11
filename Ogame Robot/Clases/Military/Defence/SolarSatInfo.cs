namespace Ogame_Robot.Clases.Military
{
    class SolarSatInfo : DefenseUnitInfo {
        public override UnitType Type {
            get { return UnitType.SolarSat;
            }
        }

        public override int WeaponPower {
            get { return 1; }
        }

        public override int ShieldPower {
            get { return 1; }
        }

        public override int StructuralIntegrity {
            get { return 2000; }
        }

        public override Resources Cost {
            get { return new Resources( 0, 2000, 500 ); }
        }
    }
}