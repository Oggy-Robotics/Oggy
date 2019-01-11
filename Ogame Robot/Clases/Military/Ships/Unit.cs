namespace Ogame_Robot.Clases.Military
{
    public struct Unit
    {
        public Unit( int shield, int armor , int weapons) {
            Shield = shield;
            Armor = armor;
            Weapons = weapons;
        }
        public int Shield;
        public int Armor;
        public int Weapons;
    }
}