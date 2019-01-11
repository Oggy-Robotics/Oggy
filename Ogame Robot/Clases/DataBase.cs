using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

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

            public List<string> addLines = new List<string>();
        } 
        public static void Inicialize()
        {
            DataBase dataBase = new DataBase();
            if (DataBase.dataBase==null)
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
}