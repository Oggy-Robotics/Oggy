namespace ScootSim {
    class SolarSatInfo : DefenseUnitInfo {
        public override UnitType Type {
            get { return UnitType.SolarSat; }
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

        public override Res Cost {
            get { return new Res( 2000, 2000, 0 ); }
        }
    }
}