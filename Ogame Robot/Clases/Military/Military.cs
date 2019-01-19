
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ogame_Robot.Clases.Military
{
    public class Military
    {
        public BrowserManipulation browser;
        public Level[] research;





        public static UnitInfo[] GetMyFleetInfo()
        {
            UnitInfo[] myShips = new UnitInfo[14];
            for (int i = 0; i < 14; i++)
            {
                myShips[i] = UnitInfo.GetInfo((UnitType)i);
            }
            return myShips;
        }
        /// <summary>
        /// získá podrobnější data o flotile a jejich poměry
        /// </summary>
        /// <param name="planet"></param>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static MyFleetInfo GetMyFleetTotalInfo(int planet, BrowserManipulation browser)
        {
            return GetMyFleetTotalInfo(false, browser.InfoHangar(planet));
        }
        public static MyFleetInfo GetMyFleetTotalInfo(bool multiplier,int[] fleetOrigin)
        {
            MyFleetInfo myFleet = MyFleetInfo.Inicialization();
            //retype from [13] to [14]
            int[] fleet = new int[14];
            for (int i = 0; i < fleetOrigin.Count(); i++)
            {
                fleet[i] = fleetOrigin[i];
            }

            myFleet.myHangar = fleet;
            UnitInfo[] myShips = GetMyFleetInfo();
            if (multiplier)
            {
                for (int i = 0; i < 14; i++)
                {
                    myFleet.ResuourcesTypeInfo[i] = myShips[i].Cost.metal + Convert.ToInt32( myShips[i].Cost.crystal*1.5) + myShips[i].Cost.deuterium*3;
                    myFleet.ResuourcesTypeTotal[i] = myFleet.ResuourcesTypeInfo[i] * myFleet.myHangar[i];
                    myFleet.myHangarResuourcesTotal += myFleet.ResuourcesTypeTotal[i];
                }
            }
            else
            {
                for (int i = 0; i < 14; i++)
                {
                    myFleet.ResuourcesTypeInfo[i] = myShips[i].Cost.metal + myShips[i].Cost.crystal + myShips[i].Cost.deuterium;
                    myFleet.ResuourcesTypeTotal[i] = myFleet.ResuourcesTypeInfo[i] * myFleet.myHangar[i];
                    myFleet.myHangarResuourcesTotal += myFleet.ResuourcesTypeTotal[i];
                }
            }

            myFleet.resuourcesTypeTotalPercentage = MyFleetInfo.ResuourcesTypeTotalPercentage(myFleet.myHangar);

            return myFleet;
        }

        public static UnitInfo[] GetMyDefenceInfo()
        {
            UnitInfo[] myDefence = new UnitInfo[8];
            for (int i = 0; i < 8; i++)
            {
                myDefence[i] = UnitInfo.GetInfo((UnitType)(i+14));
            }
            return myDefence;
        }
        public static MyDefenceInfo GetMyDefenceTotalInfo(int planet, BrowserManipulation browser)
        {
            return GetMyDefenceTotalInfo(false, browser.InfoDefence(planet));
        }
        public static MyDefenceInfo GetMyDefenceTotalInfo(bool multiplier, int[] defenceOrigin)
        {
            MyDefenceInfo myDefence = MyDefenceInfo.Inicialization();
            //retype from [9] to [8]
            int[] defence = new int[8];
            for (int i = 0; i < defence.Count(); i++)
            {
                defence[i] = defenceOrigin[i];
            }

            myDefence.MyDefence = defence;
            UnitInfo[] myDefenceUnits = GetMyDefenceInfo();
            if (multiplier)
            {
                for (int i = 0; i < 8; i++)
                {
                    myDefence.ResuourcesTypeInfo[i] = myDefenceUnits[i].Cost.metal + Convert.ToInt32(myDefenceUnits[i].Cost.crystal * 1.5) + myDefenceUnits[i].Cost.deuterium * 3;
                    myDefence.ResuourcesTypeTotal[i] = myDefence.ResuourcesTypeInfo[i] * myDefence.MyDefence[i];
                    myDefence.myDefenceResuourcesTotal += myDefence.ResuourcesTypeTotal[i];
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    myDefence.ResuourcesTypeInfo[i] = myDefenceUnits[i].Cost.metal + myDefenceUnits[i].Cost.crystal + myDefenceUnits[i].Cost.deuterium;
                    myDefence.ResuourcesTypeTotal[i] = myDefence.ResuourcesTypeInfo[i] * myDefence.MyDefence[i];
                    myDefence.myDefenceResuourcesTotal += myDefence.ResuourcesTypeTotal[i];
                }
            }

            myDefence.resuourcesTypeTotalPercentage = MyDefenceInfo.ResuourcesTypeTotalPercentage(myDefence.MyDefence);

            return myDefence;
        }

    }

    public struct MyFleetInfo
    {
        /// <summary>
        /// amouth
        /// </summary>
        public int[] myHangar;
        public int[] ResuourcesTypeInfo;
        public int[] ResuourcesTypeTotal;
        /// <summary>
        /// even the spy probes and satelits
        /// </summary>
        public int myHangarResuourcesTotal;

        ///// <summary>
        ///// 0.10 =10% surek z celku
        ///// </summary>
        public double[] resuourcesTypeTotalPercentage;

        public static MyFleetInfo Inicialization()
        {
            MyFleetInfo myFleetInfo;
            myFleetInfo.myHangar = new int[14];
            myFleetInfo.ResuourcesTypeInfo = new int[14];
            myFleetInfo.ResuourcesTypeTotal = new int[14];
            myFleetInfo.resuourcesTypeTotalPercentage = new double[14];
            myFleetInfo.myHangarResuourcesTotal = 0;
            return myFleetInfo;
        }

        public static double[] ResuourcesTypeTotalPercentage(int[] ships)
        {
            return ResuourcesTypeTotalPercentage(ships,false);
        }
        public static double[] ResuourcesTypeTotalPercentage(int[] ships, bool multiplier)
        {
            MyFleetInfo myFleet = MyFleetInfo.Inicialization();

            myFleet.myHangar = ships;
            UnitInfo[] myShips = Military.GetMyFleetInfo();

            if (multiplier)
            {
                for (int i = 0; i < 14; i++)
                {
                    myFleet.ResuourcesTypeInfo[i] = myShips[i].Cost.metal + Convert.ToInt32(myShips[i].Cost.crystal * 1.5) + myShips[i].Cost.deuterium * 3;
                    myFleet.ResuourcesTypeTotal[i] = myFleet.ResuourcesTypeInfo[i] * myFleet.myHangar[i];
                    myFleet.myHangarResuourcesTotal += myFleet.ResuourcesTypeTotal[i];
                }
            }
            else
            {
                for (int i = 0; i < 14; i++)
                {
                    myFleet.ResuourcesTypeInfo[i] = myShips[i].Cost.metal + myShips[i].Cost.crystal + myShips[i].Cost.deuterium;
                    myFleet.ResuourcesTypeTotal[i] = myFleet.ResuourcesTypeInfo[i] * myFleet.myHangar[i];
                    myFleet.myHangarResuourcesTotal += myFleet.ResuourcesTypeTotal[i];
                }
            }
            if(myFleet.myHangarResuourcesTotal != 0)
            {
                for (int i = 0; i < 14; i++)
                {
                    myFleet.resuourcesTypeTotalPercentage[i] = myFleet.ResuourcesTypeTotal[i] / (double)myFleet.myHangarResuourcesTotal;
                }
            }
            else
            {
                for (int i = 0; i < 14; i++)
                {
                    myFleet.resuourcesTypeTotalPercentage[i] = 0;
                }
            }
            return myFleet.resuourcesTypeTotalPercentage;
        }
    }
    public struct MyDefenceInfo
    {
        /// <summary>
        /// amouth
        /// </summary>
        public int[] MyDefence;
        public int[] ResuourcesTypeInfo;
        public int[] ResuourcesTypeTotal;
        /// <summary>
        /// even the spy probes and satelits
        /// </summary>
        public int myDefenceResuourcesTotal;

        ///// <summary>
        ///// 0.10 =10% surek z celku
        ///// </summary>
        public double[] resuourcesTypeTotalPercentage;

        public static MyDefenceInfo Inicialization()
        {
            MyDefenceInfo myDefenceInfo;
            myDefenceInfo.MyDefence = new int[8];
            myDefenceInfo.ResuourcesTypeInfo = new int[8];
            myDefenceInfo.ResuourcesTypeTotal = new int[8];
            myDefenceInfo.resuourcesTypeTotalPercentage = new double[8];
            myDefenceInfo.myDefenceResuourcesTotal = 0;
            return myDefenceInfo;
        }

        public static double[] ResuourcesTypeTotalPercentage(int[] defences)
        {
            return ResuourcesTypeTotalPercentage(defences, false);
        }
        public static double[] ResuourcesTypeTotalPercentage(int[] defences, bool multiplier)
        {
            MyDefenceInfo myDefence = MyDefenceInfo.Inicialization();

            myDefence.MyDefence = defences;
            UnitInfo[] myDefences = Military.GetMyDefenceInfo();

            if (multiplier)
            {
                for (int i = 0; i < 8; i++)
                {
                    myDefence.ResuourcesTypeInfo[i] = myDefences[i].Cost.metal + Convert.ToInt32(myDefences[i].Cost.crystal * 1.5) + myDefences[i].Cost.deuterium * 3;
                    myDefence.ResuourcesTypeTotal[i] = myDefence.ResuourcesTypeInfo[i] * myDefence.MyDefence[i];
                    myDefence.myDefenceResuourcesTotal += myDefence.ResuourcesTypeTotal[i];
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    myDefence.ResuourcesTypeInfo[i] = myDefences[i].Cost.metal + myDefences[i].Cost.crystal + myDefences[i].Cost.deuterium;
                    myDefence.ResuourcesTypeTotal[i] = myDefence.ResuourcesTypeInfo[i] * myDefence.MyDefence[i];
                    myDefence.myDefenceResuourcesTotal += myDefence.ResuourcesTypeTotal[i];
                }
            }
            if (myDefence.myDefenceResuourcesTotal != 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    myDefence.resuourcesTypeTotalPercentage[i] = myDefence.ResuourcesTypeTotal[i] / (double)myDefence.myDefenceResuourcesTotal;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    myDefence.resuourcesTypeTotalPercentage[i] = 0;
                }
            }
            return myDefence.resuourcesTypeTotalPercentage;
        }
    }



}