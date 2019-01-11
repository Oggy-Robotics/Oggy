namespace Ogame_Robot.Clases.Military
{
    class LightLaserInfo : DefenseUnitInfo {
        public override UnitType Type {
            get { return UnitType.LightLaser; }
        }

        public override int WeaponPower {
            get { return 100; }
        }

        public override int ShieldPower {
            get { return 25; }
        }

        public override int StructuralIntegrity {
            get { return 2000; }
        }

        public override Resources Cost {
            get { return new Resources( 1500, 500, 0 ); }
        }
    }
}