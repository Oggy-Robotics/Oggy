using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogame_Robot.Clases
{
    public partial class Timer
    {
        //autostavba
        public class AutoBuilder : PackedFunction
        {
            public Buildings buildings;
            public int numberOfCall = 0;

            public AutoBuilder(Timer timer, Buildings buildings, TimeSpan standardFrequency)
            {
                this.buildings = buildings;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public AutoBuilder(Timer timer, Buildings buildings, TimeSpan standardFrequency, TimeSpan addTime)
            {
                this.buildings = buildings;
                nextCall = DateTime.Now + addTime;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public override TimeSpan CallFunction()
            {
                numberOfCall++;
                TimeSpan cas = buildings.AutomatickaStavba(buildings.browserManipulation, 1);
                if (standardFrequency > cas)
                {
                    if (cas == TimeSpan.FromSeconds(-1))
                    {
                        return standardFrequency;
                    }
                    else
                    {
                        return cas;
                    }
                }
                else { return standardFrequency; }
            }
        }




    }
}
