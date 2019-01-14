using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ogame_Robot.Clases
{
    public partial class BrowserManipulation
    {
        public DateTime InfoLastShipCameBack()
        {
            DateTime last = DateTime.MinValue;
            FleetMovements fleetMovements = InfoFleetMovement();


            return fleetMovements.fleetMovements[fleetMovements.fleetMovements.Count() - 1].arrivalTime;
        }

        public FleetMovements InfoFleetMovement()
        {
            driver.Navigate().Refresh();
            FleetMovements fleetMovements = FleetMovements.Inicialization();
            bool someFleetActivity = false;

            //detection for fleet bar if exist or isnt
            int cycleClick = 0;
            bool cycleUnsucees = true;
            while (cycleUnsucees)
            {
                try
                {
                    driver.FindElement(By.XPath(FleetPanel.panel)).Click();
                    someFleetActivity = true;
                    cycleUnsucees = false;
                }
                catch (Exception)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    cycleClick++;
                    if (cycleClick == 3)
                        cycleUnsucees = false;
                }

            }
            if (someFleetActivity)
            {


                bool wait = true;
                int counter = 0;
                while (wait)
                {
                    try
                    {
                        new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementIsVisible(By.XPath(FleetPanel.idHeaderTest)));
                        wait = false;
                    }
                    catch (Exception)
                    {
                        if(counter>3)
                        driver.FindElement(By.XPath(FleetPanel.panel)).Click();
                        counter++;
                    }
                }

                bool next = true;
                int line = 1;
                while (next)
                {
                    FleetMovements.FleetMovement fleetMovement = FleetMovements.FleetMovement.Inicialization();
                    try
                    {
                        fleetMovement.coordsOrigin = Coordinates.ConvertFromText(driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.coordsOrigin)))).Text);
                    }
                    catch (Exception)
                    {
                        next = false;
                    }
                    if (next)
                    {


                        fleetMovement.coordsOrigin = Coordinates.ConvertFromText(driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.coordsOrigin)))).Text);

                        //!not counted days and longer
                        fleetMovement.arrivalTime = Convert.ToDateTime(driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.arrivalTime)))).Text.Split(' ')[0]);
                        //!text=[::] =error?
                        fleetMovement.destCoords = Coordinates.ConvertFromText(driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.destCoords)))).Text);
                        fleetMovement.originFleet = driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.originFleet)))).Text;
                        fleetMovement.destFleet = driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.destFleet)))).Text;

                        //class="icon_movement_reserve" ,class="icon_movement"
                        fleetMovement.icon_movement = driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.arrivalTime)))).GetAttribute("class").Contains("reserve");



                        switch (driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.missionFleet)) + "/img[1]")).GetAttribute("src"))
                        {
                            case "https://gf1.geo.gfsrv.net/cdn9a/cd360bccfc35b10966323c56ca8aac.gif": //attack
                                fleetMovement.missionFleet = Convert.ToByte(FleetMision.attack);
                                break;
                            case "https://gf1.geo.gfsrv.net/cdn38/2af2939219d8227a11a50ff4df7b51.gif": //transport
                                fleetMovement.missionFleet = Convert.ToByte(FleetMision.transport);
                                break;
                            case "https://gf3.geo.gfsrv.net/cdnb7/60a018ae3104b4c7e5af8b2bde5aee.gif": //spy
                                fleetMovement.missionFleet = Convert.ToByte(FleetMision.spy);
                                break;
                            case "https://gf1.geo.gfsrv.net/cdn08/26dd1bcab4f77fe67aa47846b3b375.gif": //mine
                                fleetMovement.missionFleet = Convert.ToByte(FleetMision.minederbys);
                                break;
                            case "https://gf3.geo.gfsrv.net/cdnb0/4dab966bded2d26f89992b2c6feb4c.gif": //deployment
                                fleetMovement.missionFleet = Convert.ToByte(FleetMision.deployment);
                                break;
                            default:
                                string test = driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.missionFleet)) + "/img[1]")).GetAttribute("src");
                                ;
                                break;
                                
                        }


                        //ships in fleet
                        string textShips = "";//"Detail letky:\r\nLodě:\r\nMalý transportér: 205\r\n  Dodávka:\r\nKov: 428.219\r\nKrystaly: 215.449\r\nDeuterium: 20.123"
                        bool hided = true;
                        int cycle = 1;
                        Actions action = new Actions(driver);

                        //if player dont have spy lv
                        if (driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.detailsFleet)))).Text.Length<2)
                        {
                            hided = false;
                        }

                        //move with view
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.icon_movement)))));
                        IWebElement webDriverWait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementIsVisible(By.XPath(FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.icon_movement)))));
                        action.MoveToElement(driver.FindElement(By.XPath((FleetPanel.Table.Replace("$", Convert.ToString(line)).Replace("&", Convert.ToString(FleetPanel.icon_movement)))))).Perform();

                        //find element with info about fleet
                        while (hided)//ok!if spy tech is 0 or low it get stopped here-any info abaout units-no clickable element
                        {
                            try
                            {
                                string path = "/html[1]/body[1]/div[" + Convert.ToString(cycle) + "]";
                                string path2 = "/html[1]/body[1]/div[" + Convert.ToString(cycle) + "]/div[3]/div[1]";
                                IWebElement webElement = driver.FindElement(By.XPath(path));

                                if (!webElement.GetAttribute("style").Contains("-10000px") && webElement.GetAttribute("style").Contains("1000000"))
                                {
                                    textShips = WaitForElement(By.XPath(path2)).Text;
                                    hided = false;
                                }
                            }
                            catch (Exception)
                            {
                            }
                            cycle++;
                            if (cycle == 500)
                                cycle = 1;
                        }

                        //!ships should be loaded from scan from hangar from each ship type name
                        string[] ships = { "Lehký stíhač", "Těžký stíhač", "Křižník", "Bitevní loď", "Bitevní křižník", "Bombardér", "Ničitel", "Hvězda smrti", "Malý transportér", "Velký transportér", "Kolonizační loď", "Recyklátor", "Špionážní sonda" };
                        string[] lines = textShips.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        bool fleetStats = true;
                        bool resourceStats = true;
                        cycle = 2;
                        while (fleetStats)
                        {
                            bool added = false;
                            if (lines.Count() > cycle)
                            {
                                for (int i = 0; i < ships.Length; i++)
                                {
                                    if (lines[cycle].Contains(ships[i]))
                                    {
                                        fleetMovement.fleetDetails[i] = Resources.ConvertNumberFromText(lines[cycle].Replace(" ", "").Split(':')[1]);
                                        added = true;
                                    }
                                }
                            }
                            else
                            {
                                fleetStats = false;
                                resourceStats = false;
                            }
                            if (!added)
                                fleetStats = false;

                            cycle++;
                        }
                        if (resourceStats)
                        {
                            fleetMovement.resources.metal = Resources.ConvertNumberFromText(lines[cycle].Replace(" ", "").Split(':')[1]);
                            cycle++;
                            fleetMovement.resources.crystal = Resources.ConvertNumberFromText(lines[cycle].Replace(" ", "").Split(':')[1]);
                            cycle++;
                            fleetMovement.resources.deuterium = Resources.ConvertNumberFromText(lines[cycle].Replace(" ", "").Split(':')[1]);
                        }


                        fleetMovements.fleetMovements.Add(fleetMovement);
                        line++;
                    }
                }

                //closeing panel
                driver.FindElement(By.XPath(FleetPanel.panel)).Click();
            }


            fleetMovements.scanned = DateTime.Now;
            return fleetMovements;
        }


        /// <summary>
        /// start from 1
        /// </summary>
        /// <returns></returns>
        private int InfoActualPlanetWiewing()
        {
            int numberOfPlanets = InfoPlayerNumberOfPlanets();
            int actualPlanet = 1;
            if (numberOfPlanets < 6)
            {
                for (int i = 1; i < numberOfPlanets + 1; i++)
                {
                    if (WaitForElement(By.XPath(Overview.relxpathPlanetsUntil5.Replace("&", Convert.ToString(i)))).GetAttribute("class").Contains("active"))
                        actualPlanet = i;
                }
            }
            else
            {
                for (int i = 1; i < numberOfPlanets + 1; i++)
                {
                    if (WaitForElement(By.XPath(Overview.relxpathPlanets6AndMore.Replace("&", Convert.ToString(i)))).GetAttribute("class").Contains("active"))
                        actualPlanet = i;
                }
            }
            return actualPlanet;
        }

        public void InfoPlayer()
        {

            Player player = new Player();
            //player.research = new Player.Level[16];


            //planets
            int numberOfPLanets = InfoPlayerNumberOfPlanets();
            //
            for (int i = 1; i <= numberOfPLanets; i++)
            {
                player.planets.Add(InfoPlanet(i));
            }

            //research
            player.research = InfoResearch();



            this.player = player;//kdyby se nacítani info nezdařilo neovlivní to aktualni data

            //under atack
            //UnderAttack();


            //end of playerinfo zarážka
            int i3 = 0;
        }

        public int InfoPlayerNumberOfPlanets()
        {
            return Convert.ToInt32(WaitForElement(By.XPath(Overview.xpathPlanetsAmounth)).Text.Split('/')[0]);
        }
        public List<Coordinates> InfoPlayerCoordinatesOfPlanets()
        {
            List<Coordinates> planets = new List<Coordinates>();
            int number = InfoPlayerNumberOfPlanets();
            if (number < 6)
            {
                for (int i = 1; i < number + 1; i++)
                {
                    planets.Add(Coordinates.ConvertFromText(WaitForElement(By.XPath(Overview.relxpathPlanetsCoordinatesUntil5.Replace("&", Convert.ToString(i)))).Text));
                }
                return planets;
            }
            else
            {
                for (int i = 1; i < number + 1; i++)
                {
                    planets.Add(Coordinates.ConvertFromText(WaitForElement(By.XPath(Overview.relxpathPlanetsCoordinates6AndMore.Replace("&", Convert.ToString(i)))).Text));
                }
                return planets;

            }

        }
        public string InfoPlayerName()
        {

            string text = WaitForElement(By.Id(Overview.idPlayerName)).Text.Split(':')[1];
            if (text[0] == ' ') text = text.Remove(0, 1);
            return text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planetNumber">start from 1</param>
        /// <returns></returns>
        public Player.Planet InfoPlanet(int planetNumber)
        {

            ChangePlanet(planetNumber);
            Player.Planet planet = new Player.Planet();
            //planet.suply = new Player.Level[9];
            //planet.station = new Player.Level[8];
            //planet.fleet = new int[14];
            //planet.defence = new int[10];


            //overview
            {
                ChangeMenu(Menutab.Prehled);
                IWebElement name = WaitForElement(By.Id("planetNameHeader"));
                IWebElement coord = WaitForElement(By.Id("positionContentField"));
                IWebElement diameter = WaitForElement(By.Id("diameterContentField"));
                IWebElement temperature = WaitForElement(By.Id("temperatureContentField"));
                planet.name = name.Text;

                FunctionInt = (x) => Convert.ToInt32(x.Text.Split(' ')[1].Replace("(", "").Replace(")", "").Split('/')[0]);
                WaitForElementFunction(diameter, FunctionInt);
                planet.fieldsActual = Convert.ToInt32(diameter.Text.Split(' ')[1].Replace("(", "").Replace(")", "").Split('/')[0]);

                FunctionInt = (x) => Convert.ToInt32(x.Text.Split(' ')[1].Replace("(", "").Replace(")", "").Split('/')[1]);
                WaitForElementFunction(diameter, FunctionInt);
                planet.fieldsMax = Convert.ToInt32(diameter.Text.Split(' ')[1].Replace("(", "").Replace(")", "").Split('/')[1]);


                string test = (temperature.Text);
                string test2 = (temperature.Text.Split(' ')[0].Split('°')[0]);

                FunctionInt = (x) => Convert.ToInt32(x.Text.Split(' ')[0].Split('°')[0]);
                WaitForElementFunction(temperature, FunctionInt);
                planet.TemperatureMin = Convert.ToInt32(temperature.Text.Split(' ')[0].Split('°')[0]);

                FunctionInt = (x) => Convert.ToInt32(x.Text.Split(' ')[2].Split('°')[0]);
                WaitForElementFunction(temperature, FunctionInt);
                planet.TemperatureMax = Convert.ToInt32(temperature.Text.Split(' ')[2].Split('°')[0]);


                FunctionInt = (x) => Convert.ToInt32(x.Text.Replace("[", "").Replace("]", "").Split(':')[2]);
                WaitForElementFunction(coord, FunctionInt);

                planet.coordinates = new Coordinates//muže to hodit eror kvuli nenačtení
                {
                    galaxy = Convert.ToInt32(coord.Text.Replace("[", "").Replace("]", "").Split(':')[0]),
                    system = Convert.ToInt32(coord.Text.Replace("[", "").Replace("]", "").Split(':')[1]),
                    planet = Convert.ToInt32(coord.Text.Replace("[", "").Replace("]", "").Split(':')[2]),

                };

                planet.metal = Convert.ToInt32(WaitForElement(By.Id("resources_metal")).Text.Replace(".", ""));
                planet.crystal = Convert.ToInt32(WaitForElement(By.Id("resources_crystal")).Text.Replace(".", ""));
                planet.deuterium = Convert.ToInt32(WaitForElement(By.Id("resources_deuterium")).Text.Replace(".", ""));
                planet.energy = Convert.ToInt32(WaitForElement(By.Id("resources_energy")).Text.Replace(".", ""));
            }



            //suply
            planet.suply = InfoSuply();



            //stations
            planet.station = InfoStations();


            //hangar
            planet.fleet = InfoHangar();

            //defence
            planet.defence = InfoDefence();



            return planet;
            /*
            letky
            ChangeMenu(menuLetky);
            /html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[7]/div[2]/form[1]/div[$]/ul[1]/li[&]/div[1]/a[1]/span[1]/span[1]
            /html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[7]/div[2]/form[1]/div[1]/ul[1]/li[2]/div[1]/a[1]/span[1]/span[1]
            /html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[7]/div[2]/form[1]/div[1]/ul[1]/li[8]/div[1]/a[1]/span[1]/span[1]
            /html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[7]/div[2]/form[1]/div[3]/ul[1]/li[1]/div[1]/a[1]/span[1]/span[1]
            /html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[7]/div[2]/form[1]/div[3]/ul[1]/li[5]/div[1]/a[1]/span[1]/span[1]
            */
        }

        public Level[] InfoSuply(int planet)
        {
            ChangePlanet(planet);
            return InfoSuply();

        }
        public Level[] InfoSuply()
        {
            ChangeMenu(Menutab.Zasobovani);


            return InfoSuply(true);

        }
        private Level[] InfoSuply(bool menu)
        {


            Level[] suply = new Level[9];

            for (int i = 1; i < 10; i++)
            {
                int a = i;
                int b = 1;
                if (i > 6)
                { a -= 6; b = 2; }
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/div[1]/a[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/a[1]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/a[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/div[1]/a[2]
                string xpathMetal1 = Suply.xpathSuply.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a)).Replace('%', '1');//nelze stavet
                string xpathMetal2 = Suply.xpathSuply.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a)).Replace('%', '2');//posible
                string xpathMetal3 = Suply.xpathSuplyInUpgrade.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a));//in process
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[2]/div[1]/div[1]/div[1]/a[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[2]/div[1]/div[1]/a[1]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[2]/div[1]/div[1]/a[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/div[1]/a[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[$]/li[&]/div[1]/div[1]/div[1]/a[2]
                IWebElement webElement;
                string str;
                string str2;
                string str3;
                int status;

                try
                {
                    webElement = driver.FindElement(By.XPath(xpathMetal1));//nelze stavet
                    str = webElement.Text;

                    status = 1;
                    if (!int.TryParse(webElement.Text, out suply[i - 1].lv))
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        webElement = driver.FindElement(By.XPath(xpathMetal2));//lze stavet
                        str2 = webElement.Text;

                        status = 2;
                        if (!int.TryParse(webElement.Text, out suply[i - 1].lv))
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            webElement = driver.FindElement(By.XPath(xpathMetal3));//in process
                            str3 = webElement.Text;

                            status = 3;
                            //text="21\r\n20" =>new lv old lv
                            //string text = webElement.Text;
                            //string[] text2 = webElement.Text.Split(new char[] { '\r', '\n' });
                            //int i2 = 0;
                            if (!int.TryParse(webElement.Text.Split(new char[] { '\r', '\n' })[2], out suply[i - 1].lv))
                            {
                                throw new Exception();
                            }
                        }
                        catch (Exception e)//spadlo to sem mockrat- nedostatek času na načtení? když na planete 2 se vylepšovala budova 2 byl vybrán webelement lze stavet a skončilo to s chybou zde
                        {
                            FilesOperations.ErrorLogFileAddError(e);
                            int i2 = 0;
                        }
                        //throw;
                    }
                    //throw;
                }

            }

            int actualplanet = InfoActualPlanetWiewing();
            if (player.planets.Count >= actualplanet)
            {
                player.planets[actualplanet - 1].suply = suply;
            }

            return suply;

        }

        public Level[] InfoStations(int planet)
        {
            ChangePlanet(planet);
            return InfoStations();
        }
        public Level[] InfoStations()
        {
            ChangeMenu(Menutab.Továrny);


            return InfoStations(true);
        }
        private Level[] InfoStations(bool menu)
        {

            Level[] station = new Level[8];

            for (int i = 1; i < 9; i++)
            {
                int a = i;

                string xpathStations1 = Stations.xpathStations.Replace("&", Convert.ToString(a)).Replace('%', '1');//nelze stavet
                string xpathStations2 = Stations.xpathStations.Replace("&", Convert.ToString(a)).Replace('%', '2');//posible
                string xpathStations3 = Stations.xpathStationsInUpgrade.Replace("&", Convert.ToString(a));//in process
                IWebElement webElement;
                int status = 0;
                try
                {
                    webElement = driver.FindElement(By.XPath(xpathStations1));//nelze stavet
                    status = 1;
                    if (!int.TryParse(webElement.Text, out station[i - 1].lv))
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    try
                    {
                        webElement = driver.FindElement(By.XPath(xpathStations2));//lze stavet
                        status = 2;
                        if (!int.TryParse(webElement.Text, out station[i - 1].lv))
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            // Text = "1\r\n0"
                            webElement = driver.FindElement(By.XPath(xpathStations3));//in process
                            status = 3;
                            if (!int.TryParse(webElement.Text.Split(new char[] { '\r', '\n' })[2], out station[i - 1].lv))
                            {
                                throw new Exception();
                            }
                        }
                        catch (Exception e)//spadlo to sem mockrat- nedostatek času na načtení? když na planete 2 se vylepšovala budova 2 byl vybrán webelement lze stavet a skončilo to s chybou zde
                        {
                            FilesOperations.ErrorLogFileAddError(e);
                            int i2 = 0;
                        }
                        //throw;
                    }
                    //throw;
                }
                station[i - 1].posible = Convert.ToByte(status);
            }

            int actualplanet = InfoActualPlanetWiewing();
            if (player.planets.Count >= actualplanet)
            {
                player.planets[actualplanet - 1].station = station;
            }

            return station;
        }

        public int[] InfoHangar(int planet)
        {
            ChangePlanet(planet);
            return InfoHangar();

        }
        public int[] InfoHangar()
        {
            ChangeMenu(Menutab.Hangar);


            return InfoHangar(true);

        }
        private int[] InfoHangar(bool menu)
        {

            int[] fleet = new int[14];

            for (int i = 1; i < 15; i++)
            {
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/div[$]/ul[1]/li[&]/div[1]/div[1]/a[1]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/div[1]/ul[1]/li[1]/div[1]/div[1]/div[1]/a[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/div[2]/ul[1]/li[1]/div[1]/div[1]/a[1]


                int a = i;
                int b = 1;
                if (i > 8)
                { a -= 8; b = 2; }
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/div[1]/a[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/a[1]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/a[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/div[1]/a[2]

                string xpath1 = Hangar.relxpathHangar.Replace("XXX", Convert.ToString(Hangar.shipDetail[i-1]));//more stable-ewen for build in proces
                //string xpath1 = Hangar.xpathHangar.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a));//počet lodí
                string xpath2 = Hangar.xpathHangarInBuild.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a));//in process
                IWebElement webElement;//Text = "1\r\n2"
                try
                {
                    webElement = driver.FindElement(By.XPath(xpath1));//pocet lodí
                    fleet[i - 1] = Resources.ConvertNumberFromText(webElement.Text);
                }
                catch (Exception)
                {
                    try
                    {
                        webElement = driver.FindElement(By.XPath(xpath2));//zrovna se staví
                                                                          //Text = "178\r\n1.137"=error
                        fleet[i - 1] = Resources.ConvertNumberFromText(webElement.Text.Split(new char[] { '\r', '\n' })[2]);
                    }
                    catch (Exception e)//spadlo to sem mockrat- nedostatek času na načtení? když na planete 2 se vylepšovala budova 2 byl vybrán webelement lze stavet a skončilo to s chybou zde
                    {
                        FilesOperations.ErrorLogFileAddError(e);
                        int i2 = 0;
                    }
                }
            }

            int actualplanet = InfoActualPlanetWiewing();
            if (player.planets.Count >= actualplanet)
            {
                player.planets[actualplanet - 1].fleet = fleet;
            }

            return fleet;

        }

        public int[] InfoDefence(int planet)
        {
            ChangePlanet(planet);
            return InfoDefence();

        }
        public int[] InfoDefence()
        {
            ChangeMenu(Menutab.Obrana);


            return InfoDefence(true);

        }
        private int[] InfoDefence(bool menu)
        {

            int[] defence = new int[10];

            for (int i = 1; i < 11; i++)
            {
                int a = i;

                string xpath1 = Defence.xpathDefence.Replace("&", Convert.ToString(a));
                string xpath2 = Defence.xpathDefenceInBuild.Replace("&", Convert.ToString(a));//in process
                IWebElement webElement;
                try
                {
                    webElement = driver.FindElement(By.XPath(xpath1));
                    defence[i - 1] = Resources.ConvertNumberFromText(webElement.Text);
                }
                catch (Exception)
                {
                    try
                    {
                        webElement = driver.FindElement(By.XPath(xpath2));//in process

                        defence[i - 1] = Resources.ConvertNumberFromText(webElement.Text.Split(new char[] { '\r', '\n' })[2]);

                    }
                    catch (Exception e)//spadlo to sem mockrat- nedostatek času na načtení? když na planete 2 se vylepšovala budova 2 byl vybrán webelement lze stavet a skončilo to s chybou zde
                    {
                        FilesOperations.ErrorLogFileAddError(e);
                        int i2 = 0;
                    }
                }


            }

            int actualplanet = InfoActualPlanetWiewing();
            if (player.planets.Count >= actualplanet)
            {
                player.planets[actualplanet - 1].defence = defence;
            }

            return defence;

        }

        public Level[] InfoResearch()
        {
            ChangeMenu(Menutab.Výzkum);


            return InfoResearch(true);
        }
        private Level[] InfoResearch(bool menu)
        {
            Level[] research = new Level[16];

            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[$]/ul[1]/li[&]/div[1]/div[1]/a[%]
            //i = turny b=tabulky a=subtabulky
            for (int i = 1, a = 1, b = 1; i < 17; i++, a++)
            {
                if (i == 6)
                { a = 1; b++; }
                if (i == 9)
                { a = 1; b++; }
                if (i == 14)
                { a = 1; b++; }

                string xpathResearch1 = Research.xpathResearch.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a)).Replace('%', '1');//nelze stavet
                string xpathResearch2 = Research.xpathResearch.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a)).Replace('%', '2');//posible
                string xpathResearch3 = Research.xpathResearchInUpgrade.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a));//in process
                IWebElement webElement;
                int status = 1;
                try
                {
                    webElement = driver.FindElement(By.XPath(xpathResearch1));//nelze stavet
                    status = 1;
                    if (!int.TryParse(webElement.Text, out research[i - 1].lv))
                    { throw new Exception(); }
                }
                catch (Exception)
                {
                    try
                    {
                        webElement = driver.FindElement(By.XPath(xpathResearch2));//lze stavet
                        status = 2;
                        if (!int.TryParse(webElement.Text, out research[i - 1].lv))
                        { throw new Exception(); }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[3]/ul[1]/li[3]/div[1]/div[1]/div[1]/a[2]
                            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[3]/ul[1]/li[3]/div[1]/div[1]/div[1]/a[2]
                            webElement = driver.FindElement(By.XPath(xpathResearch3));//in process
                            status = 3;

                            if (!int.TryParse(webElement.Text.Split(new char[] { '\r', '\n' })[2], out research[i - 1].lv))
                            {
                                throw new Exception();
                            }
                        }
                        catch (Exception e)//spadlo to sem mockrat- nedostatek času na načtení? když na planete 2 se vylepšovala budova 2 byl vybrán webelement lze stavet a skončilo to s chybou zde
                        {
                            FilesOperations.ErrorLogFileAddError(e);
                            int i2 = 0;
                        }
                    }

                }
                //research[i - 1].posible = Convert.ToByte(status); good for nothing every planet have diferent buildability
            }

            player.research = research;

            return research;
        }

        public ProductionSeting InfoProductionSeting(int planet)
        {
            ChangePlanet(planet);
            return InfoProductionSeting();
        }
        public ProductionSeting InfoProductionSeting()
        {
            ChangeMenu(SettingsSuply.settingsSuply);
            return InfoProductionSeting(true);
        }
        private ProductionSeting InfoProductionSeting(bool menu)
        {

            ProductionSeting productionSeting = new ProductionSeting();


            FunctionInt = (x) => Convert.ToInt32(x.Text.Replace(" ", "").Replace("%", "").Split(':')[1]);
            WaitForElementFunction(WaitForElement(By.XPath(SettingsSuply.relxpathFactorKey)), FunctionInt);
            productionSeting.factorKey = Convert.ToInt32(WaitForElement(By.XPath(SettingsSuply.relxpathFactorKey)).Text.Replace(" ", "").Replace("%", "").Split(':')[1]);

            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[2]/div[2]/form[1]/table[1]/tbody[1]/tr[16]/td[2]/span[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[2]/div[2]/form[1]/table[1]/tbody[1]/tr[16]/td[5]/span[1]
            int[] production = new int[4];
            for (int i = 2; i < 6; i++)
            {
                string xpath = SettingsSuply.relxpathtable.Replace("$", "16").Replace("&", Convert.ToString(i));
                string test = WaitForElement(By.XPath(xpath)).Text;
                production[i - 2] = Convert.ToInt32(WaitForElement(By.XPath(xpath)).Text.Replace(".", "").Replace(",", "").Replace("M", ""));
            }
            productionSeting.resources.metal = production[0];
            productionSeting.resources.crystal = production[1];
            productionSeting.resources.deuterium = production[2];
            productionSeting.resources.energy = production[3];


            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[2]/div[2]/form[1]/table[1]/tbody[1]/tr[4]/td[7]/span[1]/a[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[2]/div[2]/form[1]/table[1]/tbody[1]/tr[9]/td[7]/span[1]/a[1]
            for (int i = 4; i < 10; i++)
            {
                string xpath = SettingsSuply.relxpathtablePercentage.Replace("$", Convert.ToString(i)).Replace("&", "7");
                productionSeting.percentage[i - 4] = Convert.ToByte(WaitForElement(By.XPath(xpath)).Text.Replace("%", ""));
            }




            int actualplanet = InfoActualPlanetWiewing();
            if (player.planets.Count >= actualplanet)
            {
                player.planets[actualplanet - 1].productionSeting = productionSeting;
            }

            return productionSeting;


        }

        /// <summary>
        /// utility for changing menu
        /// </summary>
        /// <param name="menu"></param>
        private void InfoMenu(string menu)
        {
            if (menu == Menutab.Zasobovani)
            {
                InfoSuply(true);
            }
            if (menu == Menutab.Hangar)
            {
                InfoHangar(true);
            }
            if (menu == Menutab.Obrana)
            {
                InfoDefence(true);
            }
            if (menu == Menutab.Výzkum)
            {
                InfoResearch(true);
            }
            if (menu == SettingsSuply.settingsSuply)
            {
                InfoProductionSeting(true);
            }

        }

    }
}
