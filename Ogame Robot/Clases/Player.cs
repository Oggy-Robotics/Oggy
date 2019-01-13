using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogame_Robot.Clases
{

    public class Player
    {

        public List<Planet> planets = new List<Player.Planet>();
        //public Science science;
        public Level[] research;
        public List<UnderAttack> underAtack = new List<UnderAttack>();
     


        public class Planet
        {
            public string name;
            public Coordinates coordinates;
            public int fieldsMax;
            public int fieldsActual;
            public int TemperatureMin;
            public int TemperatureMax;


            public int metal;
            public int crystal;
            public int deuterium;
            public int energy;

            public Level[] suply;
            public ProductionSeting productionSeting = new ProductionSeting();

            public Level[] station;

            /// <summary>
            /// could build? not implemented
            /// </summary>
            public bool[] research;

            public int[] fleet;

            public int[] defence;            
        }

    }
}
