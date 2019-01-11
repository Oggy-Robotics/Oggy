namespace Ogame_Robot.Clases.Military
{
    class HeavyLaserInfo : DefenseUnitInfo {
        public override UnitType Type {
            get { return UnitType.HeavyLaser; }
        }

        public override int WeaponPower {
            get { return 250; }
        }

        public override int ShieldPower {
            get { return 100; }
        }

        public override int StructuralIntegrity {
            get { return 8000; }
        }

        public override Resources Cost {
            get { return new Resources( 6000,2000, 0 ); }
        }
    }
}