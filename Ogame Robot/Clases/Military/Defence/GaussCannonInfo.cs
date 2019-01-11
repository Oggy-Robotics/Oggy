namespace Ogame_Robot.Clases.Military
{
    class GaussCannonInfo : DefenseUnitInfo {
        public override UnitType Type {
            get { return UnitType.GaussCannon; }
        }

        public override int WeaponPower {
            get { return 1100; }
        }

        public override int ShieldPower {
            get { return 200; }
        }

        public override int StructuralIntegrity {
            get { return 35000; }
        }

        public override Resources Cost {
            get { return new Resources( 20000, 15000, 2000 ); }
        }
    }
}