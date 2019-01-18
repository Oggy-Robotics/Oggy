using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace Ogame_Robot.Clases
{
    public class DataBase
    {
        public static DataBase dataBase;
        public Thread thread;
        public InicializationFile inicializationFile = new InicializationFile();
        public Game game = new Game();


        public class Game
        {
            public Timer timer = new Timer();
            public BrowserManipulation browserManipulation;

        }


        public class InicializationFile
        {
            public string username;
            public string password;
            public bool autologin;

            public Settings settings = new Settings();
            /*
            public List<string> addLines = new List<string>();

            public static string getAddLine(string nameOfProperty)
            {
                for (int i = 0; i < DataBase.dataBase.inicializationFile.addLines.Count(); i++)
                {
                    if (DataBase.dataBase.inicializationFile.addLines[i].Contains(nameOfProperty + "="))
                    {
                        return DataBase.dataBase.inicializationFile.addLines[i].Split('=')[1];
                    }
                }
                return null;
            }
            public static List<string> getAddLine(string nameOfProperty, char split)
            {
                for (int i = 0; i < DataBase.dataBase.inicializationFile.addLines.Count(); i++)
                {
                    if (DataBase.dataBase.inicializationFile.addLines[i].Contains(nameOfProperty + "="))
                    {
                        return new List<string>(DataBase.dataBase.inicializationFile.addLines[i].Split('=')[1].Split(split));
                    }
                }
                return null;
            }
            */

        }


        public static void Inicialize()
        {
            DataBase dataBase = new DataBase();
            if (DataBase.dataBase == null)
            {
                DataBase.dataBase = dataBase;
            }


        }

        public static void InicializeNewThreath(DataBase dataBase)
        {
            if (DataBase.dataBase == null)
            {
                DataBase.dataBase = dataBase;
            }
        }
    }


    public class Settings
    {
        public Dictionary<int, string> settings = new Dictionary<int, string>();
        public Dictionary<int, string> properties = new Dictionary<int, string>();

        public static string[] settingsName = new string[]
        {
            ///35.233.106.236:3128
            ///IP:port
            "proxy",
            ///1,200,0.0,True,20,3,450,3.5
            ///1,200,0.0,True,20,3,450
            ///false
            ///startingPlanet,distance,distanceCoefficient,simulate,forceMultiplier,minimalHourResources,requestedRank,frequency in hours
            "farminactive",
            ///Izar
            ///name
            "loginuniverse",
            ///2,10
            ///2
            ///false
            ///rezim,frequency in hours
            "autobuilder",
            ///0.5
            ///frequency in hours
            "underattack",

            /*
            "farminactive",
            "farminactive",
            "farminactive",
            "farminactive",
            "farminactive",
            "farminactive",
            */
        };


        public Settings()
        {
            foreach (string item in settingsName)
            {
                settings.Add(settings.Keys.Count(), item.ToLower());
            }
        }


        public static string getProperty(string nameOfSetting)
        {
            try
            {
                return DataBase.dataBase.inicializationFile.settings.properties[DataBase.dataBase.inicializationFile.settings.settings.Values.ToList().IndexOf(nameOfSetting.ToLower())];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<string> getProperty(string nameOfSetting, char split)
        {
            try
            {
                return new List<string>(DataBase.dataBase.inicializationFile.settings.properties[DataBase.dataBase.inicializationFile.settings.settings.Values.ToList().IndexOf(nameOfSetting.ToLower())].Split(split));
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string getProperty(int SettingID)
        {
            try
            {
                return DataBase.dataBase.inicializationFile.settings.properties[SettingID];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<string> getProperty(int SettingID, char split)
        {
            try
            {
                return new List<string>(DataBase.dataBase.inicializationFile.settings.properties[SettingID].Split(split));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int getID(string nameOfSetting)
        {
            try
            {
                return DataBase.dataBase.inicializationFile.settings.settings.Values.ToList().IndexOf(nameOfSetting.ToLower());
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}