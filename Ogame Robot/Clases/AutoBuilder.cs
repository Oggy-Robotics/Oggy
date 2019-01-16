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
                this.timer = timer;
                this.ID = timer.maxID + 1;
                timer.maxID++;
                this.buildings = buildings;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public AutoBuilder(Timer timer, Buildings buildings, TimeSpan standardFrequency, TimeSpan addTime)
            {
                this.timer = timer;
                this.ID = timer.maxID + 1;
                timer.maxID++;
                this.buildings = buildings;
                nextCall = DateTime.Now + addTime;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public override TimeSpan CallFunction()
            {
                string settings = DataBase.InicializationFile.getAddLine("autobuilder");
                int rezim = 0;
                if (settings != null)
                {
                    if (settings == "False")
                    {
                        timer.packedFunctions.RemoveAt(timer.PositionOfFciByID(ID));
                    }
                    else
                    {
                        List<string> settings2 = new List<string>(settings.Split(','));
                        rezim =Convert.ToInt32(settings2[0]);
                        if (settings2.Count>1)
                        standardFrequency = TimeSpan.FromHours(Convert.ToDouble(settings2[1], System.Globalization.CultureInfo.InvariantCulture));                        
                    }
                }

                numberOfCall++;
                TimeSpan cas = buildings.AutomatickaStavba(buildings.browserManipulation,/*Vychozí režim*/rezim);
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
