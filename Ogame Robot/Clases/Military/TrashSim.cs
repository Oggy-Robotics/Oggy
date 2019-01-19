using Ogame_Robot.Clases.Military;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ogame_Robot.Clases
{
    public class TrashSim : BrowserManipulation
    {
        BrowserManipulation browser;
        public IWebDriver TrashDriver;

        public TrashSim(BrowserManipulation browser)
        {
            this.browser = browser;
            OpenTrashSim();
        }
        /// <summary>
        /// do nothing, uninicialized
        /// </summary>
        public TrashSim()
        { }
        public bool OpenTrashSim()
        {
            if (TrashDriver == null)
            {
                TrashDriver = new FirefoxDriver();
                TrashDriver.Navigate().GoToUrl(TrashSimLib.mainPage);
                WaitForElement(By.XPath(TrashSimLib.relxpathdefendersAPI), TrashDriver);
            }

            return true;
        }
        public SimulationResult SimulateBattle(string API, int[] myfleet, int[] myResearch, Coordinates coordinates)
        {
            try
            {
                TrashDriver.FindElement(By.Id(TrashSimLib.idCookie)).Click();
            }
            catch (Exception)
            { }


            //research
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechArmour), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechArmour), TrashDriver).SendKeys(Convert.ToString(myResearch[(int)ResearchInfo.battleArmor]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechShield), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechShield), TrashDriver).SendKeys(Convert.ToString(myResearch[(int)ResearchInfo.battleShields]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechWeapons), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechWeapons), TrashDriver).SendKeys(Convert.ToString(myResearch[(int)ResearchInfo.battleWeapons]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechChemicalCombusion), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechChemicalCombusion), TrashDriver).SendKeys(Convert.ToString(myResearch[(int)ResearchInfo.driveChemical]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechImpulse), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechImpulse), TrashDriver).SendKeys(Convert.ToString(myResearch[(int)ResearchInfo.driveImpulse]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechHypersace), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechHypersace), TrashDriver).SendKeys(Convert.ToString(myResearch[(int)ResearchInfo.driveHyper]));

            //attacker fleet
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "202")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "202")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.SmallCargo]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "203")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "203")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.LargeCargo]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "204")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "204")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.LightFighter]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "205")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "205")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.HeavyFighter]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "206")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "206")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.Cruiser]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "207")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "207")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.Battleship]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "208")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "208")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.ColonyShip]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "209")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "209")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.Recycler]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "210")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "210")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.EspProbe]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "211")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "211")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.Bomber]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "213")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "213")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.Destroyer]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "214")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "214")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.Deathstar]));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "215")), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "215")), TrashDriver).SendKeys(Convert.ToString(myfleet[(int)UnitType.Battlecruiser]));

            //attacker cord
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionGalaxi), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionGalaxi), TrashDriver).SendKeys(Convert.ToString(coordinates.galaxy));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionSystem), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionSystem), TrashDriver).SendKeys(Convert.ToString(coordinates.system));
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionPosition), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionPosition), TrashDriver).SendKeys(Convert.ToString(coordinates.planet));

            //API
            WaitForElement(By.XPath(TrashSimLib.relxpathdefendersAPI), TrashDriver).Clear();
            WaitForElement(By.XPath(TrashSimLib.relxpathdefendersAPI), TrashDriver).SendKeys(API);
            WaitForElement(By.XPath(TrashSimLib.relxpathLoadSpyMessageButton), TrashDriver).Click();
            Thread.Sleep(50);

            //Simulate
            bool unloaded = true;
            while (unloaded)
            {
                try
                {
                    TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathSimulateButton)).Click();
                    unloaded = false;
                }
                catch (Exception)
                { }
            }
            //is simulation completed?
            unloaded = true;
            while (unloaded)
            {
                try
                {
                    if (Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.idSimulationCurrent)).Text)
                    == Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.idSimulationTotal)).Text))
                        unloaded = false;
                    Thread.Sleep(TimeSpan.FromMilliseconds(200));
                }
                catch (Exception)
                { } 
            }

            SimulationResult simulationResult = new SimulationResult();

            simulationResult.attackerWinn = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathAttackerWinn)).Text.Replace("%", ""));
            simulationResult.defenderWinn = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathDefenderWinn)).Text.Replace("%", ""));
            simulationResult.indecisively = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathIndecisively)).Text.Replace("%", ""));
            simulationResult.attackerLoss = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathAttackerLoss)).Text.Replace(".", ""));
            simulationResult.defenderLoss = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathDefenderLoss)).Text.Replace(".", ""));
            simulationResult.plunderTotal = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathPlunderTotal)).Text.Replace(".", ""));
            simulationResult.posiblePlunderTotal = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathPosiblePlunderTotal)).Text.Replace(".", ""));




            return simulationResult;
        }
        

    }

    public struct SimulationResult
    {
        //chanches
        public int attackerWinn;
        public int defenderWinn;
        public int indecisively;

        //resources totall
        public int attackerLoss;
        public int defenderLoss;
        public int plunderTotal;
        public int posiblePlunderTotal;

    }

}