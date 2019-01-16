using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ogame_Robot.Clases
{
    public class BrowserInfo
    {
        
        public class Login
        {
            public static string idusernameLogin = "usernameLogin";
            public static string idpasswordLogin = "passwordLogin";
            public static string urlMain = "https://cz.ogame.gameforge.com/";
            public static string idlogintab = "ui-id-1";
            public static string idloginSubmit = "loginSubmit";
            public static string relxpathPlayNow = "//button[@class='button button-primary button-lg']";

            public static string relxpathPlayOnUniverseButton= "//div[@id='accountlist']//div[@class='rt-tbody']/div[&]/div[1]/div[11]/button[1]";
            public static string relxpathPlayOnUniverseName= "//div[@id='accountlist']//div[@class='rt-tbody']/div[&]/div[1]/div[4]/div[1]";
            ///html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/div[2]
            ////html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[4]/div[1] -universeName
            ///html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[11]/button[1] -universebutton1
            ////html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[2]/section[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[11]/button[1] -universebutton2

        }
        public class FleetPanel
        {
            public static string panel = "//div[@id='messages_collapsed']//p[1]//p[1]";
            /// <summary>
            /// $=line &=elementfrom left to right
            /// </summary>
            public static string Table = "//table[@id='eventContent']/tbody[1]/tr[$]/td[&]";///img[1]

            public static int timeUntil = 1;//3hod 37min 8s
            public static int arrivalTime = 2;//13:14:20
            public static int missionFleet = 3;
            public static int originFleet = 4;//Ztorm 
            public static int coordsOrigin = 5;//[1:446:4]
            public static int detailsFleet = 6;//134
            public static int icon_movement = 7;
            public static int destFleet = 8;//The land of Dragons
            public static int destCoords = 9;//[1:296:6] //white charakters
            public static int sendProbe = 10;
            public static int sendMail = 11;

            public static string idHeaderTest = "//div[@id='eventHeader']";
        }
        public class Menutab
        {
            public static string menu = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[2]/div[1]/ul[1]/li[&]/a[1]";
            public static string SelectedIcon = "//a[contains(@class,'selected')]";
            public static string Prehled = menu.Replace('&', '1');
            public static string Zasobovani = menu.Replace('&', '2');
            public static string Továrny = menu.Replace('&', '3');
            public static string Obchodnik = menu.Replace('&', '4');
            public static string Výzkum = menu.Replace('&', '5');
            public static string Hangar = menu.Replace('&', '6');
            public static string Obrana = menu.Replace('&', '7');
            public static string Letky = menu.Replace('&', '8');
            public static string Galaxie = menu.Replace('&', '9');
            public static string Aliance = menu.Replace("&", "10");


        }
        public class Galaxy
        {
            public static string idGalaxyInput = "galaxy_input";
            public static string idSystemInput = "system_input";
            public static string idSexpeditionButton = "system_input";
            public static string relxpathGo = "//div[@class='btn_blue']";
            public static string xpathGo = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[1]/form[1]/div[1]";
            public static string idGalaxyLoading = "id='galaxyLoading'";
             

            ////table[@id='galaxytable']//tbody         //-shortage
            //planets with planet / without planet
            public static string xpathTablePlanetExisted = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[2]/div[1]/a[1]/img[1]";
            public static string xpathTablePlanetUnknown = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[2]";
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[1]/td[2]/div[1]/a[1]/img[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[10]/td[2]/div[1]/a[1]/img[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[5]/td[2]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[15]/td[2]
            
            //activity on planet
            public static string xpathTablePlanetExistedActivity = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[2]/div[1]/div[1]/img[1]";
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[4]/td[2]/div[1]/div[1]/img[1]  //player is now activ
            
            //moon with/without
            public static string xpathTableMoonExisted = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[4]/a[1]/div[1]";
            public static string xpathTableMoonUnknown = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[4]";
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[2]/td[4]/a[1]/div[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[7]/td[4]/a[1]/div[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[1]/td[4]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[15]/td[4]           
            
            //derbis field
            public static string xpathTableDerbysExisted = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[5]/a[1]/div[1]";
            public static string xpathTableDerbysUnknown = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[5]";
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[9]/td[5]/a[1]/div[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[1]/td[5]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[15]/td[5]            
            
            //player
            public static string xpathTablePlayerMe = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[6]/span[1]";
            public static string xpathTablePlayerAnother = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[6]/a[1]/span[1]";
            public static string xpathTablePlayerExtaInfo = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[6]/span[1]";
            public static string xpathTablePlayerExtaInfoWithHonorRank = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[6]/span[2]";
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[4]/td[6]/span[1]  //my name
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[8]/td[6]/a[1]/span[1] //another player name with aditions
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[9]/td[6]/a[1]/span[1] //another player name without aditions
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[1]/td[6]       //tabulka s celím textem
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[15]/td[6]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[8]/td[6]/span[1]/span[1]/span[1]  //i -7days inactive
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[8]/td[6]/span[1]   // text with aditional info such = Iiz...

            //alliance
            public static string xpathTableAllianceInfo = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[7]/span[1]";
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[4]/td[7]/span[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[7]/td[7]/span[1]

            //rank  UV
            public static string xpathTablePlayerRankUV = "//table[@id='galaxytable']//tbody[1]//tr[&]//td[6]//a[2]";

            //spying
            public static string xpathTableSendSpy = "//table[@id='galaxytable']//tbody[1]/tr[&]/td[8]/span[1]/a[1]/span[1]";
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[1]/div[4]/div[1]/table[1]/tbody[1]/tr[8]/td[8]/span[1]/a[1]/span[1]

            //head info
            public static string idHeadProbes = "probeValue";
            public static string idHeadRecycler = "recyclerValue";
            public static string idHeadMissile = "missileValue";
            public static string idHeadSlots_twoinone = "slotValue";
            public static string idHeadSlotsUsed = "slotUsed";




        }
        public class SettingsSuply
        {
            public static string settingsSuply = "//ul[@id='menuTable']//li[2]//span[1]//a[1]//div[1]";
            public static string relxpathFactorKey = "//span[@class='factorkey']";

            ////form[@method='POST']//table[1]/tbody[1]/tr[$]/td[&]     $=řádky 1-18, &=sloupce 1-6  
            ////table[@class='list listOfResourceSettingsPerPlanet']//tbody[1]/tr[$]/td[&]   alternativ road
            //public static string relxpathtable = "//table[@class='list listOfResourceSettingsPerPlanet']//tbody[1]/tr[$]/td[&]";   alternativ road
            public static string relxpathtable = "//form[@method='POST']//table[1]/tbody[1]/tr[$]/td[&]";
            public static string relxpathtablePercentage = "//form[@method='POST']//table[1]/tbody[1]/tr[$]/td[&]/span[1]/a[1]";
            public static string xpathPlanetName = "//html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/h2[1]";

        }
        public class Overview
        {
            public static string idMyWorlds = "myWorlds";//confusing -same element diferent id throught net, use xpathPlanetList instead.//5 and less planets
            public static string idMyPlanets = "myPlanets";////div[@id='myWorlds']//div[2]//div[&]//a[1]//span[2]
            public static string relxpathPlanetsCoordinatesUntil5 = "//div[@id='myWorlds']//div[2]//div[&]//a[1]//span[2]";
            public static string relxpathPlanetsCoordinates6AndMore = "//div[@id='myPlanets']//div[2]//div[&]//a[1]//span[2]";
            public static string relxpathPlanetsUntil5 = "//div[@id='myWorlds']//div[2]//div[&]//a[1]";
            public static string relxpathPlanets6AndMore = "//div[@id='myPlanets']//div[2]//div[&]//a[1]";

            public static string xpathPlanetList = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[4]/div[1]/div[1]";
            public static string xpathPlanetThePlanet = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[4]/div[1]/div[1]/div[2]/div[&]/a[1]";
            public static string xpathPlanetsName = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[4]/div[1]/div[1]/div[2]/div[&]/a[1]/span[1]";
            public static string xpathPlanetsCoord = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[4]/div[1]/div[1]/div[2]/div[&]/a[1]/span[2]";
            public static string xpathPlanetsAmounth = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[4]/div[1]/div[1]/div[1]/p[1]/span[1]";
            public static string idPlanetsNameTabPrehled = "planetNameHeader";
            public static string idUnderAttack = "attack_alert";
            public static string xpathPlanetUnderAttack = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[4]/div[1]/div[1]/div[2]/div[&]/a[2]/span[1]";
            public static string xpathPlanetMenuName = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[1]/h2[1]";
            public static string idPlayerName = "playerName";
        }
        public class Messages
        {
            public static string xpathMessagesIcon = "//a[contains(@class,'comm_menu messages tooltip js_hideTipOnMobile')]";
            /*
            public static string xpathMessagesIcon = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[7]/a[1]";
            public static string xpathMessagesIcon2 = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[10]/a[1]";
            */
            public static string relxpathMessagesIcon = "//a[contains(@class,'comm_menu messages tooltip js_hideTipOnMobile')]";
            /// <summary>
            /// &=1-6
            /// </summary>
            public static string xpathType = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[2]/div[1]/ul[1]/li[&]";
            /// <summary>
            /// $=1-2,&=1-5
            /// </summary>
            public static string xpathSubType = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[2]/div[1]/div[$]/div[1]/div[1]/div[1]/ul[1]/li[&]";
            /// <summary>
            /// 1/2 =aktual/max
            /// </summary>
            public static string xpathPageNumber = "//ul[@class='tab_inner ctn_with_trash clearfix']//ul[1]//li[3]";
            public static string xpathPageNext = "//ul[@class='tab_inner ctn_with_trash clearfix']//ul[1]//li[4]";
            /// <summary>
            /// $=číslo zprávy &=1-5=řádek zprávy
            /// </summary>
            public static string xpathMessageLine = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//div[&]";
            /// <summary>
            /// first leter is ' '!
            /// </summary>
            public static string xpathMessageLinePlayerName = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//div[1]//span[2]";
            public static string xpathMessageLineMetal = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//div[2]//span[1]//span[1]";
            public static string xpathMessageLineCrystal = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//div[2]//span[1]//span[2]";
            public static string xpathMessageLineDeu = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//div[2]//span[1]//span[3]";
            public static string xpathMessageLineResources = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/ul[1]/li[$]/span[1]/div[2]/span[2]";
            public static string xpathMessageLineFleet = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//div[5]//span[1]";
            public static string xpathMessageLineDefence = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//div[5]//span[2]";
            public static string xpathMessageLineLootable = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//div[4]//span[1]";



            public static string xpathMessagePlanetNameAndCoord = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//div[2]//span[1]//a[1]";
            /// <summary>
            /// return 2 elements probably the second one,but contains attribute: class="msg_date fright"
            /// </summary>
            public static string xpathMessageDateAndTime = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//div[2]//span[2]//span[1]";
            public static string xpathMessageApiPicture = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/ul[1]/li[$]/div[3]/div[1]/span[1]";
            /// <summary>
            /// $=~15-~40 =hightly unstable
            /// </summary>
            public static string xpathMessageApiNumber = "//html[1]/body[1]/div[$]/div[3]/div[1]/input[1]";
            public static string xpathMessageAttackAlreadyUnderAttack = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]/div[3]/a[3]/span[1]/img[1]";


            ////ul[@class='tab_inner ctn_with_trash clearfix']//ul[1]//li[3]  //1/2 =number of page
            ////ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//div[&]  //$=číslo zprávy &=1-5=řádek zprávy
            ////ul[@class='tab_inner ctn_with_trash clearfix']//li[2]//span[1]//div[1]
            ////ul[@class='tab_inner ctn_with_trash clearfix']//li[1]//span[1]//div[2]
            ////ul[@class='tab_inner ctn_with_trash clearfix']//li[1]//span[1]//div[5]
            ////ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//div[2]//span[1]//a[1]    //name and coord of the planet
            ///
            ////ul[@class='tab_inner ctn_with_trash clearfix']//li[1]//div[1]//span[1]//span[1]   //api obrázek
            ///html[1]/body[1]/div[33]/div[3]/div[1]/input[1]   //api number hightly unstable
            ///html[1]/body[1]/div[40]/div[3]/div[1]/input[1]

            //from foreign spy
            /// <summary>
            /// class="espionageDefText"
            /// </summary>
            public static string xpathMessageLineForeignSpy = "//ul[@class='tab_inner ctn_with_trash clearfix']//li[$]//span[1]//span[1]";


        }

        public class TrashSimLib
        {
            public static string mainPage = "https://trashsim.universeview.be/";
            public static string relxpathdefendersAPI = "//div[@class='party-api-wrapper clearfix']//input[@type='text']";
            public static string relxpathAtackerTechWeapons = "//input[@id='simulate-attackers-0-techs-weapon']";
            public static string relxpathAtackerTechShield = "//input[@id='simulate-attackers-0-techs-shield']";
            public static string relxpathAtackerTechArmour = "//input[@id='simulate-attackers-0-techs-armour']";
            public static string relxpathAtackerTechChemicalCombusion = "//input[@id='simulate-attackers-0-techs-combustion']";
            public static string relxpathAtackerTechImpulse = "//input[@id='simulate-attackers-0-techs-impulse']";
            public static string relxpathAtackerTechHypersace = "//input[@id='simulate-attackers-0-techs-hyperspace']";

            /// <summary>
            /// 202-215 without212
            /// </summary>
            public static string relxpathAtackersFleet = "//input[@id='simulate-attackers-0-entity-&']";
            public static string relxpathAtackerPositionGalaxi= "//input[@id='simulate-attackers-0-coords-galaxy']";
            public static string relxpathAtackerPositionSystem= "//input[@id='simulate-attackers-0-coords-system']";
            public static string relxpathAtackerPositionPosition= "//input[@id='simulate-attackers-0-coords-position']";

            public static string relxpathAtackerFlightTime ="//span[@id='simulate-attackers-0-flighttime']";
            public static string relxpathAtackerFlightFuelConsumation= "//span[@id='simulate-attackers-0-consumption']";

            public static string relxpathSimulateButton = "//div[@id='simulate-button-bottom']";
            public static string relxpathLoadSpyMessageButton = "//div[@class='party-api-wrapper clearfix']//div[1]";
            public static string idLoot = "result-possible-plunder-total";
            public static string idCookie = "uv-apps-cc-agree";
            




        }
        public class Suply
        {
            public static string xpathSuply = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[$]/li[&]/div[1]/div[1]/a[%]";
            public static string xpathSuplyInUpgrade = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[$]/li[&]/div[1]/div[1]/div[1]/a[2]";
            public static string xpathSuplyUpgrade = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[$]/li[&]/div[1]/div[1]/a[%]/img[1]";
            public static int Metal = 1;
            public static int Crystal = 2;
            public static int Deuterium = 3;
            public static int Solar = 4;
            public static int Fusion = 5;
            public static int SolarSatelit = 6;
            public static int StorageMetal = 7;
            public static int StorageCrystal = 8;
            public static int StorageDeuterium = 9;
        }
        public class Stations
        {            
            public static string xpathStations = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[&]/div[1]/div[1]/a[%]"; 
            public static string xpathStationsInUpgrade = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[&]/div[1]/div[1]/div[1]/a[2]";
            public static string xpathStationsUpgrade = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[&]/div[1]/div[1]/a[1]/img[1]";
            public static int Robot = 1;
            public static int Hangar = 2;
            public static int Laboratory = 3;
            public static int AlianceStorage = 4;
            public static int RocketSile = 5;
            public static int sNanoRobot = 6;
            public static int Teraformer = 7;
            public static int SpaceDock = 8;
        }
        public class Research
        {
            //research
            public static string xpathResearch = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[$]/ul[1]/li[&]/div[1]/div[1]/a[%]";
            public static string xpathResearchUpgrade = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[$]/ul[1]/li[&]/div[1]/div[1]/a[%]/img[1]";
            public static string xpathResearchInUpgrade = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[$]/ul[1]/li[&]/div[1]/div[1]/div[1]/a[2]";
            public static int BasicEnergy = 1;
            public static int BasicLaser = 2;
            public static int BasicIont = 3;
            public static int BasicHyper = 4;
            public static int BasicPlasma = 5;
            public static int DriveChemical = 6;
            public static int DriveImpulse = 7;
            public static int DriveHyper = 8;
            public static int AdvancedSpy = 9;
            public static int AdvancedComputer = 10;
            public static int AdvancedAstrofizik = 11;
            public static int AdvancedResearchSite = 12;
            public static int AdvancedGraviton = 13;
            public static int BattleWeapons = 14;
            public static int BattleShields = 15;
            public static int BattleArmor = 16;
        }
        public class Hangar
        {
            public static string xpathHangar = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/div[$]/ul[1]/li[&]/div[1]/div[1]/a[1]";
            public static string xpathHangarInBuild = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/div[$]/ul[1]/li[&]/div[1]/div[1]/div[1]/a[2]";
            public static string xpathHangarBuildConfirm = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/form[1]/div[1]/div[2]/div[3]/a[1]/span[1]";
            public static string relxpathHangar = "//a[@id='detailsXXX']";
            public static int[] shipDetail = new int[] { 204, 205, 206, 207, 215, 211, 213, 214, 202, 203, 208, 209, 210, 212 };
            public static string idHangarBuildCount = "number";
            public static int Fighter = 1;
            public static int HeavyFighter = 2;
            public static int Cruiser = 3;
            public static int BattleShip = 4;
            public static int BattleCruiser = 5;
            public static int Bomber = 6;
            public static int Destorier = 7;
            public static int DeathStar = 8;
            public static int SmallTransporter = 9;
            public static int LargeTransporter = 10;
            public static int ColonyShip = 11;
            public static int Recycle = 12;
            public static int Spy = 13;
            public static int SolarSatelit = 14;
        }
        public class Defence
        {
            public static string xpathDefence = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[&]/div[1]/div[1]/a[1]";
            public static string xpathDefenceInBuild = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[&]/div[1]/div[1]/div[1]/a[2]";
            public static string idDefenceBuildCount = "number";
            public static string xpathDefenceBuildConfirm = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/form[1]/div[1]/div[2]/div[3]/a[1]/span[1]";
            public static int Rocket = 1;
            public static int LightLaser = 2;
            public static int HeavyLaser = 3;
            public static int GausCannon = 4;
            public static int IonCannon = 5;
            public static int PlasmaTower = 6;

            public static int SmallShield = 7;
            public static int LargeShield = 8;

            public static int AntibalisticMissles = 9;
            public static int InterplanetaryMissles = 10;
        }
        //start from 0
        public enum ResearchInfo
        {
            basicEnergy,
            basicLaser,
            basicIont,
            basicHyper,
            basicPlasma,
            driveChemical,
            driveImpulse,
            driveHyper,
            advancedSpy,
            advancedComputer,
            advancedAstrofizik,
            advancedResearchSite,
            advancedGraviton,
            battleWeapons,
            battleShields,
            battleArmor
        }
        //start from 1
        public enum FleetMision
        {
            expedition=1,
            kolonization,
            minederbys,
            transport,
            deployment,
            spy,
            APPdefence,
            attack,
            APPattack,
            moondestroying
        }
        ////aktualní produkce v přehledu////
        //html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[1]/div[2]/table[1]/tbody[1]/tr[1]/th[1]
        //Countdown //building

        //html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/div[2]/table[1]/tbody[1]/tr[1]/th[1]
        //researchCountdown

        //html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[3]/div[2]/table[1]/tbody[1]/tr[1]/th[1]
        //shipCountdown7
        //shipAllCountdown7
        //shipSumCount7
    }
}