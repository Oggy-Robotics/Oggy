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
                TrashDriver.Manage().Window.Maximize();
                TrashDriver.Navigate().GoToUrl(TrashSimLib.mainPage);
                WaitForElement(By.XPath(TrashSimLib.relxpathdefendersAPI), TrashDriver);

                try
                {
                    WaitForElement(By.Id(TrashSimLib.idCookie),TrashDriver).Click();
                }
                catch (Exception)
                { }
            }

            return true;
        }
        public SimulationResult SimulateBattle(string API, int[] myfleet, int[] myResearch, Coordinates myCoordinates)
        {
            return SimulateBattle(API, myfleet, myResearch, myCoordinates, null, null, null, new Coordinates());
        }
        public SimulationResult SimulateBattle(string API, int[] attackerFleet, int[] attackerResearch, Coordinates attackerCoordinates, int[] defenderFleet, int[] defenderDefence, int[] defenderResearch, Coordinates defenderCoordinates)
        {


            //attacker research
            if (attackerResearch != null)
            {
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechArmour), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechArmour), TrashDriver).SendKeys(Convert.ToString(attackerResearch[(int)ResearchInfo.battleArmor]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechShield), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechShield), TrashDriver).SendKeys(Convert.ToString(attackerResearch[(int)ResearchInfo.battleShields]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechWeapons), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechWeapons), TrashDriver).SendKeys(Convert.ToString(attackerResearch[(int)ResearchInfo.battleWeapons]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechChemicalCombusion), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechChemicalCombusion), TrashDriver).SendKeys(Convert.ToString(attackerResearch[(int)ResearchInfo.driveChemical]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechImpulse), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechImpulse), TrashDriver).SendKeys(Convert.ToString(attackerResearch[(int)ResearchInfo.driveImpulse]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechHypersace), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechHypersace), TrashDriver).SendKeys(Convert.ToString(attackerResearch[(int)ResearchInfo.driveHyper]));
            }
            //attacker fleet
            if (attackerFleet != null)
            {
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "202")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "202")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.SmallCargo]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "203")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "203")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.LargeCargo]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "204")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "204")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.LightFighter]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "205")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "205")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.HeavyFighter]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "206")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "206")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.Cruiser]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "207")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "207")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.Battleship]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "208")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "208")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.ColonyShip]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "209")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "209")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.Recycler]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "210")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "210")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.EspProbe]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "211")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "211")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.Bomber]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "213")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "213")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.Destroyer]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "214")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "214")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.Deathstar]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "215")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "215")), TrashDriver).SendKeys(Convert.ToString(attackerFleet[(int)UnitType.Battlecruiser]));
            }

            //attacker cord
            if (attackerCoordinates.galaxy != 0)
            {
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionGalaxi), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionGalaxi), TrashDriver).SendKeys(Convert.ToString(attackerCoordinates.galaxy));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionSystem), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionSystem), TrashDriver).SendKeys(Convert.ToString(attackerCoordinates.system));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionPosition), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionPosition), TrashDriver).SendKeys(Convert.ToString(attackerCoordinates.planet));
            }

            //defender research
            if (defenderResearch != null)
            {
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechArmour.Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechArmour.Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderResearch[(int)ResearchInfo.battleArmor]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechShield.Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechShield.Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderResearch[(int)ResearchInfo.battleShields]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechWeapons.Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerTechWeapons.Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderResearch[(int)ResearchInfo.battleWeapons]));
            }
            //defender fleet
            if (defenderFleet != null)
            {
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "202").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "202").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.SmallCargo]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "203").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "203").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.LargeCargo]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "204").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "204").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.LightFighter]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "205").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "205").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.HeavyFighter]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "206").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "206").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.Cruiser]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "207").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "207").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.Battleship]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "208").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "208").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.ColonyShip]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "209").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "209").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.Recycler]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "210").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "210").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.EspProbe]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "211").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "211").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.Bomber]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "213").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "213").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.Destroyer]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "214").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "214").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.Deathstar]));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "215").Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackersFleet.Replace("&", "215").Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderFleet[(int)UnitType.Battlecruiser]));
            }

            //defender defence
            if (defenderFleet != null)
            {
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "401")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "401")), TrashDriver).SendKeys(Convert.ToString(defenderDefence[(int)UnitType.RocketLauncher-14]));
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "402")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "402")), TrashDriver).SendKeys(Convert.ToString(defenderDefence[(int)UnitType.LightLaser-14]));
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "403")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "403")), TrashDriver).SendKeys(Convert.ToString(defenderDefence[(int)UnitType.HeavyLaser-14]));
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "404")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "404")), TrashDriver).SendKeys(Convert.ToString(defenderDefence[(int)UnitType.GaussCannon - 14]));
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "405")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "405")), TrashDriver).SendKeys(Convert.ToString(defenderDefence[(int)UnitType.IonCannon - 14]));
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "406")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "406")), TrashDriver).SendKeys(Convert.ToString(defenderDefence[(int)UnitType.PlasmaTurret - 14]));
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "407")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathDefenderDefence.Replace("&", "407")), TrashDriver).SendKeys(Convert.ToString(defenderDefence[(int)UnitType.GaussCannon - 14]));
            }
            //defender cord
            if (defenderCoordinates.galaxy != 0)
            {
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionGalaxi.Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionGalaxi.Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderCoordinates.galaxy));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionSystem.Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionSystem.Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderCoordinates.system));
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionPosition.Replace("attackers", "defenders")), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathAtackerPositionPosition.Replace("attackers", "defenders")), TrashDriver).SendKeys(Convert.ToString(defenderCoordinates.planet));
            }

            //API
            if (API != null)
            {
                WaitForElement(By.XPath(TrashSimLib.relxpathdefendersAPI), TrashDriver).Clear();
                WaitForElement(By.XPath(TrashSimLib.relxpathdefendersAPI), TrashDriver).SendKeys(API);
                WaitForElement(By.XPath(TrashSimLib.relxpathLoadSpyMessageButton), TrashDriver).Click();
            }


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
                string webElement1;//for testing
                string webElement2;
                try
                {
                    webElement1 = TrashDriver.FindElement(By.Id(TrashSimLib.idSimulationCurrent)).Text;
                    webElement2 = TrashDriver.FindElement(By.Id(TrashSimLib.idSimulationTotal)).Text;
                    if (Convert.ToInt32(TrashDriver.FindElement(By.Id(TrashSimLib.idSimulationCurrent)).Text)
                    == Convert.ToInt32(TrashDriver.FindElement(By.Id(TrashSimLib.idSimulationTotal)).Text))
                        unloaded = false;
                    Thread.Sleep(200);
                }
                catch (Exception)
                { unloaded = false; }
            }
            //reading results
            SimulationResult simulationResult = new SimulationResult();
            simulationResult.attackerWinn = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathAttackerWinn)).Text.Replace("%", ""));
            simulationResult.defenderWinn = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathDefenderWinn)).Text.Replace("%", ""));
            simulationResult.indecisively = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathIndecisively)).Text.Replace("%", ""));
            simulationResult.attackerLossRes = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathAttackerLoss)).Text.Replace(".", ""));
            simulationResult.defenderLossRes = Convert.ToInt32(TrashDriver.FindElement(By.XPath(TrashSimLib.relxpathDefenderLoss)).Text.Replace(".", ""));
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
        public int attackerLossRes;
        public int defenderLossRes;
        public int plunderTotal;
        public int posiblePlunderTotal;

    }

}