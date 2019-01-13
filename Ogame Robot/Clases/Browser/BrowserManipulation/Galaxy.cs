using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ogame_Robot.Clases
{
    public partial class BrowserManipulation
    {

        public void GalaxyOpen(int galaxy, int system)
        {
            ChangeMenu(Menutab.Galaxie);
            WaitForElement(By.Id(Galaxy.idGalaxyInput)).Clear();
            WaitForElement(By.Id(Galaxy.idGalaxyInput)).SendKeys(Convert.ToString(galaxy));
            WaitForElement(By.Id(Galaxy.idSystemInput)).Clear();
            WaitForElement(By.Id(Galaxy.idSystemInput)).SendKeys(Convert.ToString(system));
            WaitForElement(By.XPath(Galaxy.relxpathGo)).Click();

            //Automatic logout handler
            //id="galaxyLoading"
            //style="display: none;"
            //style="display: block;"
            int counter = 0;
            bool unloadad = true;
            Thread.Sleep(40);
            try
            {
                driver.FindElement(By.Id(Galaxy.idGalaxyLoading));
            }
            catch (Exception)
            {
                unloadad = false;
            }
            while (unloadad)
            {
                try
                {
                    if (WaitForElement(By.Id(Galaxy.idGalaxyLoading)).GetAttribute("style") == "display: block;")
                    {
                        if (counter > 20)
                        {
                            unloadad = false;
                            driver.Navigate().Refresh();
                            GalaxyOpen(galaxy, system);
                        }
                        Thread.Sleep(100);
                        counter++;
                    }
                    else if (WaitForElement(By.Id(Galaxy.idGalaxyLoading)).GetAttribute("style") == "display: none;")
                    {
                        unloadad = false;
                    }
                }
                catch (Exception)
                {
                    if (counter > 20)
                    {
                        unloadad = false;
                        driver.Navigate().Refresh();
                        GalaxyOpen(galaxy, system);
                    }
                    Thread.Sleep(100);
                    counter++;
                }

            }

        }
        //public void GalaxyScan(int galaxy, int system, int startFromCyrkle,byte trySpy) { }
        public List<GalaxySystem> GalaxyScan(int fromPlanet, Coordinates from, Coordinates to, byte trySpy, int requestedRank, bool short_verze)
        {
            ChangePlanet(fromPlanet);
            List<GalaxySystem> galaxySystems = new List<GalaxySystem>();

            Coordinates coordinates = from;
            for (int i = 0; i <= to.NumberOfSystems() - from.NumberOfSystems(); i++)
            {
                coordinates = from.Change(i);
                galaxySystems.Add(GalaxyScan(coordinates.galaxy, coordinates.system, trySpy, requestedRank, short_verze));
            }
            for (int i = 0; i <= from.NumberOfSystems() - to.NumberOfSystems(); i++)
            {
                coordinates = from.Change(-i);
                galaxySystems.Add(GalaxyScan(coordinates.galaxy, coordinates.system, trySpy, requestedRank, short_verze));
            }
            return galaxySystems;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galaxy"></param>
        /// <param name="system"></param>
        /// <param name="trySpy">0=without,1=inactive,2=probably weaker</param>
        /// <param name="requestedRank"> spy from this rank to upper -1=uneffected</param>
        /// <param name="short_verze">doesnt scan system. just for spy</param>
        /// <returns></returns>
        public GalaxySystem GalaxyScan(int galaxy, int system, byte trySpy, int requestedRank, bool short_verze)
        {
            GalaxyOpen(galaxy, system);

            GalaxySystem galaxySystem = new GalaxySystem();
            galaxySystem.galaxy = galaxy;
            galaxySystem.system = system;

            int planetnumber = -1;//for counting planets in the system starts from 0
                                  //iterating positions searching for planets to scan
            if (short_verze)
            {
                /* SHORT  */
                for (int i = 1; i < 16; i++)
                {

                    try
                    {
                        GalaxySystem.Position position = new GalaxySystem.Position();
                        driver.FindElement(By.XPath(Galaxy.xpathTablePlanetExisted.Replace("&", Convert.ToString(i))));
                        position.position = i;

                        //name
                        try
                        {
                            string text = driver.FindElement(By.XPath(Galaxy.xpathTablePlayerExtaInfo.Replace("&", Convert.ToString(i)))).Text;
                            if (text == InfoPlayerName())
                            {
                                position.playerName = text;
                            }
                            else
                            {
                                if (driver.FindElement(By.XPath(Galaxy.xpathTablePlayerExtaInfo.Replace("&", Convert.ToString(i)))).GetAttribute("class").Contains("honorRank"))
                                {
                                    position.playerExtraInfo = driver.FindElement(By.XPath(Galaxy.xpathTablePlayerExtaInfoWithHonorRank.Replace("&", Convert.ToString(i)))).Text;
                                    position.playerName = driver.FindElement(By.XPath(Galaxy.xpathTablePlayerAnother.Replace("&", Convert.ToString(i)))).Text; ;
                                }
                                else
                                {
                                    position.playerExtraInfo = text;
                                    position.playerName = driver.FindElement(By.XPath(Galaxy.xpathTablePlayerAnother.Replace("&", Convert.ToString(i)))).Text;
                                }
                            }
                        }
                        catch (Exception)
                        { }

                        //rank
                        position.rank = Convert.ToInt32(driver.FindElement(By.XPath(Galaxy.xpathTablePlayerRankUV.Replace("&", Convert.ToString(i)))).Text);

                        galaxySystem.positions.Add(position); planetnumber++;

                        //spying
                        try
                        {
                            if (trySpy == 1 && galaxySystem.positions[planetnumber].rank <= requestedRank)
                            {
                                while (driver.FindElement(By.Id(Galaxy.idHeadSlots_twoinone)).Text.Split('/')[1] == driver.FindElement(By.Id(Galaxy.idHeadSlotsUsed)).Text
                                    || Convert.ToInt32(driver.FindElement(By.Id(Galaxy.idHeadProbes)).Text) < 1)
                                {
                                    Thread.Sleep(5000);//stoped-odloglo me to-refresh/restart needed
                                    GalaxyOpen(galaxy, system);
                                }
                                if (position.playerExtraInfo == "(i)" || position.playerExtraInfo == "(I)")
                                {
                                    Thread.Sleep(500);
                                    driver.FindElement(By.XPath(Galaxy.xpathTableSendSpy.Replace("&", Convert.ToString(i)))).Click();
                                    Thread.Sleep(500);
                                }

                                //spy mission succesful or ERROR?                                                               
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    catch (Exception)
                    { }
                }

            }
            else
            {
                for (int i = 1; i < 16; i++)
                {
                    try
                    {
                        GalaxySystem.Position position = new GalaxySystem.Position();
                        driver.FindElement(By.XPath(Galaxy.xpathTablePlanetExisted.Replace("&", Convert.ToString(i))));
                        position.position = i;
                        //active
                        try
                        {
                            driver.FindElement(By.XPath(Galaxy.xpathTablePlanetExistedActivity.Replace("&", Convert.ToString(i))));
                            position.active = 1;
                        }
                        catch (Exception)
                        { position.moon = false; }

                        try
                        {
                            driver.FindElement(By.XPath(Galaxy.xpathTableMoonExisted.Replace("&", Convert.ToString(i))));
                            position.moon = true;
                        }
                        catch (Exception)
                        { position.moon = false; }
                        try
                        {
                            driver.FindElement(By.XPath(Galaxy.xpathTableDerbysExisted.Replace("&", Convert.ToString(i))));
                            position.derbysCrystal = 1;
                            position.derbysMetal = 1;
                        }
                        catch (Exception)
                        { }
                        //name
                        try
                        {
                            string text = driver.FindElement(By.XPath(Galaxy.xpathTablePlayerExtaInfo.Replace("&", Convert.ToString(i)))).Text;
                            if (text == InfoPlayerName())
                            {
                                position.playerName = text;
                            }
                            else
                            {
                                if (driver.FindElement(By.XPath(Galaxy.xpathTablePlayerExtaInfo.Replace("&", Convert.ToString(i)))).GetAttribute("class").Contains("honorRank"))
                                {
                                    position.playerExtraInfo = driver.FindElement(By.XPath(Galaxy.xpathTablePlayerExtaInfoWithHonorRank.Replace("&", Convert.ToString(i)))).Text;
                                    position.playerName = driver.FindElement(By.XPath(Galaxy.xpathTablePlayerAnother.Replace("&", Convert.ToString(i)))).Text; ;
                                }
                                else
                                {
                                    position.playerExtraInfo = text;
                                    position.playerName = driver.FindElement(By.XPath(Galaxy.xpathTablePlayerAnother.Replace("&", Convert.ToString(i)))).Text;
                                }
                            }
                        }
                        catch (Exception)
                        { }

                        //alliance
                        try
                        {
                            position.alli = driver.FindElement(By.XPath(Galaxy.xpathTableAllianceInfo.Replace("&", Convert.ToString(i)))).Text;
                        }
                        catch (Exception)
                        { }

                        //rank
                        try
                        {
                            position.rank = Convert.ToInt32(driver.FindElement(By.XPath(Galaxy.xpathTablePlayerRankUV.Replace("&", Convert.ToString(i)))).Text);
                        }
                        catch (Exception)
                        { }


                        galaxySystem.positions.Add(position);
                        planetnumber++;

                        //spying
                        try
                        {
                            if (trySpy == 1 && galaxySystem.positions[planetnumber].rank <= requestedRank)
                            {

                                while (driver.FindElement(By.Id(Galaxy.idHeadSlots_twoinone)).Text.Split('/')[1] == driver.FindElement(By.Id(Galaxy.idHeadSlotsUsed)).Text
                                    || Convert.ToInt32(driver.FindElement(By.Id(Galaxy.idHeadProbes)).Text) < 1)
                                {
                                    Thread.Sleep(5000);
                                    GalaxyOpen(galaxy, system);
                                }
                                if (position.playerExtraInfo == "(i)" || position.playerExtraInfo == "(I)")
                                {
                                    Thread.Sleep(500);
                                    driver.FindElement(By.XPath(Galaxy.xpathTableSendSpy.Replace("&", Convert.ToString(i)))).Click();
                                    Thread.Sleep(500);
                                }

                                //spy mission succesful or ERROR?



                            }
                        }
                        catch (Exception)
                        { }


                    }
                    catch (Exception)
                    { }
                }
            }
            return galaxySystem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromPlanet"></param>
        /// <param name="from"></param>
        /// <param name="range"></param>
        /// <param name="trySpy">0=without,1=inactive,2=probably weaker</param>
        /// <param name="requestedRank"> spy from this rank to upper -1=uneffected</param>
        /// <param name="short_verze">doesnt scan system. just for spy</param>
        /// <returns></returns>
        public List<GalaxySystem> GalaxyScan(int fromPlanet, Coordinates from, int range, byte trySpy, int requestedRank, bool short_verze)
        {
            ChangePlanet(fromPlanet);
            List<GalaxySystem> galaxySystems = new List<GalaxySystem>();


            Coordinates coordinates = from;
            galaxySystems.Add(GalaxyScan(coordinates.galaxy, coordinates.system, trySpy, requestedRank, short_verze));/*      SHORT VERZE         */
            for (int i = 1; i <= range; i++)
            {
                coordinates = from.Change(i);
                galaxySystems.Add(GalaxyScan(coordinates.galaxy, coordinates.system, trySpy, requestedRank, short_verze));/*      SHORT VERZE         */
                coordinates = from.Change(-i);
                galaxySystems.Add(GalaxyScan(coordinates.galaxy, coordinates.system, trySpy, requestedRank, short_verze));/*      SHORT VERZE         */
            }

            return galaxySystems;
        }

    }
}
