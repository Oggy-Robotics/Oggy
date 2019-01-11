namespace Ogame_Robot.Clases.Military
{
    class LargeShieldDomeInfo : DefenseUnitInfo {
        public override UnitType Type {
            get { return UnitType.LargeShieldDome; }
        }

        public override int WeaponPower {
            get { return 1; }
        }

        public override int ShieldPower {
            get { return 10000; }
        }

        public override int StructuralIntegrity {
            get { return 100000; }
        }

        public override Resources Cost {
            get { return new Resources( 50000, 50000, 0 ); }
        }
    }
}