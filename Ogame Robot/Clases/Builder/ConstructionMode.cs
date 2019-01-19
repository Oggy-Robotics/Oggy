using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogame_Robot.Clases
{
    public struct ConstructionMode
    {
        public Coordinates planet;
        public List<Mode> mode;
        


        public struct Mode
        {
            public bool build;
            public double q_cost;
            public byte min;
            public byte max;

        }
    }

    /*
    public struct ConstructionMode
    {
        public Coordinates planet;
        public ByCost byCost;
        //suply//poměry
        public double metal;
        public double crystal;
        public double deu;
        //energy
        public double solar;
        public double fusion;
        public double solarSatelit;
        //storrage
        public double storageQcost;
        public double storageQres;
        //robots
        public int upToLV_RF;//10
        public int upToLV_RNF;//1-3
        public int main_RF;//if resources are availiable build this preferably to the max lv
        public int main_RNF;
        //research
        public double researchPlanet;
        public double researchQcost;
        public byte researchMin;
        public byte researchMax;
        //i have 1M metal -i will build the cheepest building =>hangar cost 900k research cost 600k researchQcost=0.5 hangarQcost=1.0
        // => i will build hangar becase 50% from 1m = 500k and research cost 600k
        //hangar
        public bool battlePlanet;
        public bool hangarQcost;
        public byte hangarMin;
        public byte hangarMax;
        public double alisiloQcost;
        public byte alisiloMin;
        public byte alisiloMax;
        public double rocketsiloQcost;
        public byte rocketsiloMin;
        public byte rocketsiloMax;
    }
    [Flags]
    public enum ByCost
    {
        none = 0,
        //suply
        metal = 1,
        crystal = 2,
        deu = 4,
        //energy
        solar = 8,
        fusion = 16,
        solarSatelit = 32,
        //robots
        robotF = 64,
        robotNF = 128,
    }
    */
}
