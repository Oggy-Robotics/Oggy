using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Firefox;
using System.Threading;
using OpenQA.Selenium.Support.UI;


namespace Ogame_Robot.Clases
{
    public class NewBrowserThreat
    {
        DataBase dataBase;


        public NewBrowserThreat(DataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        public static void Start()
        {

            NewBrowserThreat browserThreat = new NewBrowserThreat(DataBase.dataBase);

            Thread browser = new Thread(browserThreat.Inicialize);
            DataBase.dataBase.thread = browser;
            browser.Start();
        }

        public void Inicialize()
        {
            DataBase.dataBase = this.dataBase;
            Test01();
        }



        public void Test01()
        {
            BrowserManipulation browser = new BrowserManipulation();
            DataBase.dataBase.game.browserManipulation = browser;
            Buildings buildings = new Buildings(browser);
            browser.StartBrowser();
            browser.Loggin();
            browser.LogginUniverse("2");

            browser.UnderAttack();
            //browser.FarmInactive(1, 160, 0.2, true, 12, 3,450);
            //browser.InfoPlayer();
            try
            {
                /*
                Military.Military.GetMyFleetTotalInfo(1, browser,true);
                browser.GalaxyScan(1, new Coordinates { galaxy = 1, system = 446, planet = 4, moon = 0 }, 30, 1);
                browser.GalaxyScan(1, new Coordinates { galaxy = 1, system = 10, planet = 0, moon = 0 }, new Coordinates { galaxy = 1, system = 1, planet = 0, moon = 0 },0);
                ProductionSeting productionSeting = browser.InfoProductionSeting(2);
                browser.MessagesOpenType(2, 4);
                browser.InfoPlanet(1);
                browser.ChangeMenu(BrowserInfo.Menutab.Zasobovani);
                browser.UpgradeSuply(BrowserInfo.Suply.Deuterium);
                browser.ChangePlanet(1);
                browser.UpgradeStation(BrowserInfo.Stations.Hangar);
                browser.UpgradeResearch(BrowserInfo.Research.BattleArmor);
                browser.BuildHangar(BrowserInfo.Hangar.Fighter, 1);
                browser.BuildDefence(BrowserInfo.Defence.Rocket, 1);
                browser.FleetSend(new Coordinates { galaxy = 1, system = 446, planet = 4, moon = 1 },new int[] {0,1,0,0,0,0,0,0,0,0,0,0,0}
                , new Coordinates { galaxy = 4, system = 43, planet = 8, moon = 1 },5,4,new Resources {metal=0,crystal=0,deuterium=0 });
                Timer.InfoPlayer infoPlayer = new Timer.InfoPlayer(browser, TimeSpan.FromHours(2), TimeSpan.FromMinutes(20));
                */
            }
            catch (Exception e)
            {
                FilesOperations.ErrorLogFileAddError(e);
                int i3 = 0;
                Test01();
            }


            //testing buildings functions
            //buildings.Inicialization();

            //end of code zarážka
            




            //test automatu
            Timer.FarmInactive farmInactive = new Timer.FarmInactive(DataBase.dataBase.game.timer, browser, TimeSpan.FromHours(3.5));//farming
            Timer.UnderAttack underAttack = new Timer.UnderAttack(DataBase.dataBase.game.timer, browser, TimeSpan.FromMinutes(10));//fleetsave-unfinished
            //Timer.AutoBuilder autoBuilder = new Timer.AutoBuilder(DataBase.dataBase.game.timer, buildings, TimeSpan.FromMinutes(20));//Autobuilder! ^^
            DataBase.dataBase.game.timer.Start();//start of the Repeater

        }



    }
}