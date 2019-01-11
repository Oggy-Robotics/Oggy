namespace Ogame_Robot.Clases.Military
{
    class SmallShieldDomeInfo : DefenseUnitInfo {
        public override UnitType Type {
            get { return UnitType.SmallShieldDome; }
        }

        public override int WeaponPower {
            get { return 1; }
        }

        public override int ShieldPower {
            get { return 2000; }
        }

        public override int StructuralIntegrity {
            get { return 2000; }
        }

        public override Resources Cost {
            get { return new Resources( 10000, 10000, 0 ); }
        }
    }
}