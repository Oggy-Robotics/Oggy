
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



        public Military Inicialization(BrowserManipulation browser)
        {
            Military military = new Military();
            military.browser = browser;
            military.research = browser.InfoResearch();
            PlayerMilitary player = new PlayerMilitary();
            UnitInfo unitInfo = UnitInfo.GetInfo(UnitType.Destroyer);

            Wessel wessel = new Wessel();
            wessel.cost = unitInfo.Cost;



            player.UnitTypes.Add(UnitType.Destroyer, 500);
            return military;
        }

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
            return GetMyFleetTotalInfo(false, browser.InfoHangar());
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
            return null;
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


    /// <summary>
    /// start from 0
    /// </summary>
    public enum Fleet
    {
        Fighter = 0,
        HeavyFighter,
        Cruiser,
        BattleShip,
        BattleCruiser,
        Bomber,
        Destorier,
        DeatStar,
        Transport,
        LargeTransport,
        ColonizationShip,
        Recycler,
        SpyProbe,
        SolarSatelite
    }


    public class Wessel
    {
        public Resources cost;
        public int hull;
        public int shield;
        public int weapons;
        public int spead;
        public int storage;
        public int fuelConsumation;


    }
}