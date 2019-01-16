using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Support;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using Ogame_Robot.Clases.Military;

namespace Ogame_Robot.Clases
{
    public partial class BrowserManipulation : BrowserInfo
    {
        public IWebDriver driver;
        public Player player = new Player();
        public int waitMilisec = 1000;
        Func<IWebElement, string> FunctionString;
        Func<IWebElement, int> FunctionInt;

        public BrowserManipulation()
        {
        }

        public bool StartBrowser()
        {
            try
            {
                //načtení profilu
                FirefoxOptions options = new FirefoxOptions();
                FirefoxProfile jmeno_profilu = new FirefoxProfile(@"Profil\mkd2wx0g.testUV");
                options.Profile = jmeno_profilu;
                //proxy
                string proxySetting = DataBase.InicializationFile.getAddLine("proxy");
                if (proxySetting!=null)
                {
                        Proxy proxy = new Proxy();
                        proxy.HttpProxy = proxySetting;
                        proxy.FtpProxy = proxySetting;
                        proxy.SslProxy = proxySetting;
                        jmeno_profilu.SetProxyPreferences(proxy);
                }



                //načtení hlavní stánky s přihlášením
                this.driver = new FirefoxDriver(options);
                driver.Url = Login.urlMain;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }



        }
        public void Loggin()
        {


            //kliknutí na tabulku pro přihlášení          
            IWebElement logintab = driver.FindElement(By.Id(Login.idlogintab));//!firefox aktualizace error znovunačetlo to browser, při pvrním použití
            logintab.Click();

            //vypsání přihlašovaciho jmena
            IWebElement username = driver.FindElement(By.Id(Login.idusernameLogin));
            username.SendKeys(DataBase.dataBase.inicializationFile.username);

            //vypsání hesla
            IWebElement password = driver.FindElement(By.Id(Login.idpasswordLogin));
            password.SendKeys(DataBase.dataBase.inicializationFile.password);

            //accept_btn//cookies
            IWebElement cookies = driver.FindElement(By.Id("accept_btn"));
            cookies.Click();

            //autoLogin
            IWebElement autoLogin = driver.FindElement(By.Id("autoLogin"));
            autoLogin.Click();


            //přihlášení
            IWebElement login = driver.FindElement(By.Id(Login.idloginSubmit));
            login.Click();

            //new additional view for loggin
            bool unloaded = true;
            int counter = 0;
            while (unloaded)
            {
                try
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                    driver.FindElement(By.Id("joinGame"));//test if player was redirected to additional loggin
                    WaitForElement(By.XPath(Login.relxpathPlayNow)).Click();
                    unloaded = false;
                }
                catch (Exception)
                {
                    if (counter > 3)//3sec wait for element if ewen exist
                        unloaded = false;
                    counter++;
                    
                }
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("accountlist")));
            Thread.Sleep(waitMilisec);




        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">name,number,//null,""=1.</param>
        public void LogginUniverse(string name)
        {
            //waiter for page loaded
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Id("accountlist")));
            Thread.Sleep(waitMilisec);

            int orderCounter = 1;
            bool extractFromIniFile = false;
            bool extractFromName = true;

            if (name != null)
            {
                if (name != "")
                {
                    int newNumber;
                    if (int.TryParse(name, out newNumber))
                    {
                        orderCounter = newNumber;
                        extractFromName = false;
                    }
                    
                }
                else
                {
                    extractFromIniFile = true;
                    extractFromName = true;
                }
            }
            else
            {
                extractFromIniFile = true;
                extractFromName = true;
            }
            if (extractFromIniFile)
            {
                string settings = DataBase.InicializationFile.getAddLine("loginUniverse");
                if (settings != null)
                {
                    name = settings;
                    if (name.Contains(','))
                    {
                        name = name.Split(',')[0];
                    }
                }
                else
                {
                    extractFromName = false;
                }
            }
            if (extractFromName)
            {
                try
                {
                    bool unfound = true;
                    while (unfound)
                    {
                        if (driver.FindElement(By.XPath(Login.relxpathPlayOnUniverseName.Replace("&", Convert.ToString(orderCounter)))).Text == name)
                        {
                            unfound = false;
                            orderCounter--;
                        }
                        orderCounter++;
                    }
                }
                catch (Exception e)
                {
                    FilesOperations.ErrorLogFileAddError(e);
                    FilesOperations.ErrorLogFileAddLines(new string[] { "probably wrong universe name. Looking for:" + name });
                }
            }


            IWebElement logginuniversebutton = driver.FindElement(By.XPath(Login.relxpathPlayOnUniverseButton.Replace("&", Convert.ToString(orderCounter))));
            logginuniversebutton.Click();
            driver.SwitchTo().Window(driver.WindowHandles[1]);


            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            wait1.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(Overview.xpathPlanetList)));
            Thread.Sleep(waitMilisec);

            //close UniView info panel
            bool unclosed = true;
            int i = 0;
            while (unclosed)
            {
                string path = "/html[1]/body[1]/ul[1]/li[1]/span[1]";
                try
                {
                    i++;
                    driver.FindElement(By.XPath(path)).Click();
                    //new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementIsVisible(By.XPath(path))).Click();
                    unclosed = false;
                }
                catch (Exception)
                {
                    Thread.Sleep(500);
                    if (i >= 10)
                    {
                        unclosed = false;

                    }
                }

            }
            //close login page
            driver.SwitchTo().Window(driver.WindowHandles[0]).Close();
            driver.SwitchTo().Window(driver.WindowHandles[0]);
        }


        public IWebElement WaitForElement(By locator)
        {
            ////ul[@class='tab_inner ctn_with_trash clearfix']//ul[1]//li[3]
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementExists(locator));
            }
            catch (Exception e)
            {
                try
                {
                    Thread.Sleep(waitMilisec);
                    return new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementExists(locator));
                }
                catch (Exception)
                { }
                try
                {
                    //for looking actual planet wiewing in suply settings
                    if (locator == By.XPath(Overview.xpathPlanetMenuName))
                        return new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementExists(By.XPath(SettingsSuply.xpathPlanetName)));
                }
                catch (Exception e2)
                {
                    FilesOperations.ErrorLogFileAddError(e2);
                    return null;
                }
                FilesOperations.ErrorLogFileAddError(e);
                return null;

            }
        }
        public IWebElement WaitForElement(By locator, IWebDriver driver)
        {
            ////ul[@class='tab_inner ctn_with_trash clearfix']//ul[1]//li[3]
            try
            {
                return new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementExists(locator));
            }
            catch (Exception)
            {
                try
                {
                    //for looking actual planet wiewing in suply settings
                    if (locator == By.XPath(Overview.xpathPlanetMenuName))
                        return new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementExists(By.XPath(SettingsSuply.xpathPlanetName)));
                }
                catch (Exception e)
                {
                    FilesOperations.ErrorLogFileAddError(e);
                    return null;
                }
                return null;

            }
        }

        public void WaitForElementFunction(IWebElement webElement, Func<IWebElement, string> f1)
        {
            bool i = true;
            while (i)
            {
                try
                {
                    f1(webElement);
                    i = false;
                }
                catch (Exception)
                { i = true; }
            }
        }
        public void WaitForElementFunction(IWebElement webElement, Func<IWebElement, int> f1)
        {
            bool i = true;
            while (i)
            {
                try
                {
                    //Thread.Sleep(1000);
                    f1(webElement);
                    i = false;
                }
                catch (Exception)
                {
                    //i = true;
                }
            }
        }



        public void ChangeMenu(string Path)
        {
            if (Path != SettingsSuply.settingsSuply)
            {


                if (driver.FindElement(By.XPath(Path)).Text != WaitForElement(By.XPath(Menutab.SelectedIcon)).Text)
                {
                    WaitForElement(By.XPath(Path)).Click();

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(500));
                    bool unfinished = true;
                    while (unfinished)
                    {
                        try
                        {
                            while (driver.FindElement(By.XPath(Path)).Text != driver.FindElement(By.XPath(Menutab.SelectedIcon)).Text)//ok!unattached to the DOM
                            {
                                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(Path)));
                                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(Menutab.SelectedIcon)));
                            }
                            unfinished = false;
                        }
                        catch (Exception)
                        {}
                    }

                    InfoMenu(Path);

                    Thread.Sleep(waitMilisec);
                }
            }
            else
            {
                WaitForElement(By.XPath(Path)).Click();
                WaitForElement(By.XPath(SettingsSuply.relxpathFactorKey));
                InfoMenu(Path);

                Thread.Sleep(waitMilisec);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planet">start from 1</param>
        public void ChangePlanet(int planet)
        {

            int numberOfPlanets = InfoPlayerNumberOfPlanets();
            
            //actual planet viewing
            int actualPlanet = InfoActualPlanetWiewing();

            if (planet != actualPlanet)
            {
                if (numberOfPlanets < 6)
                {
                    WaitForElement(By.XPath(Overview.relxpathPlanetsUntil5.Replace("&", Convert.ToString(planet)))).Click();

                    bool unLoaded = true;
                    while (unLoaded)
                    {
                        if (WaitForElement(By.XPath(Overview.relxpathPlanetsUntil5.Replace("&", Convert.ToString(planet)))).GetAttribute("class").Contains("active"))
                        {
                            unLoaded = false;
                        }
                        Thread.Sleep(200);
                    }
                }
                else
                {

                    WaitForElement(By.XPath(Overview.relxpathPlanets6AndMore.Replace("&", Convert.ToString(planet)))).Click();

                    bool unLoaded = true;
                    while (unLoaded)
                    {
                        if (WaitForElement(By.XPath(Overview.relxpathPlanets6AndMore.Replace("&", Convert.ToString(planet)))).GetAttribute("class").Contains("active"))
                        {
                            unLoaded = false;
                        }
                        Thread.Sleep(200);
                    }
                }

            }
        }


        public List<MessageSpy> GetSpyMessages(int maxMinutsOld)
        {
            //!can spy only planets

            MessagesOpenType(1, 1);
            bool opakujPage = true;
            int actualPage = 1;
            while (opakujPage)
            {
                try
                {
                    actualPage = Convert.ToInt32(driver.FindElement(By.XPath(Messages.xpathPageNumber)).Text.Split('/')[0]);//!lepší waiter
                    opakujPage = false;
                }
                catch (Exception)
                { }
            }
            int maxPage = Convert.ToInt32(WaitForElement(By.XPath(Messages.xpathPageNumber)).Text.Split('/')[1]);
            List<MessageSpy> messageSpies = new List<MessageSpy>();
            bool NextMessage = true;
            bool NeprezkocZpravu = true;
            bool nextPage = false;

            while (actualPage <= maxPage && NextMessage)
            {
                for (int i = 1; i < 51; i++)
                {
                    nextPage = false;
                    //test if the message is correct for procces
                    //time corect -should'nt be older than maxMinutsOld
                    //another message in the list -unknown amouth

                    try
                    {
                        DateTime dateTime = Convert.ToDateTime(driver.FindElements(By.XPath(Messages.xpathMessageDateAndTime.Replace("$", Convert.ToString(i))))[1].Text);
                        TimeSpan diference = DateTime.Now - dateTime;
                        if (maxMinutsOld != 0)
                            if (diference.TotalMinutes > maxMinutsOld)
                                NextMessage = false;
                    }
                    catch (Exception)
                    {
                        nextPage = true;
                        if (actualPage == maxPage)
                            NextMessage = false;
                    }


                    //next spy message is from enemy to me
                    if (!nextPage)
                    {
                        NeprezkocZpravu = true;
                        try
                        {
                            if (driver.FindElement(By.XPath(Messages.xpathMessageLineForeignSpy.Replace("$", Convert.ToString(i)))).GetAttribute("class") == "espionageDefText")
                            { NeprezkocZpravu = false; }

                        }
                        catch (Exception)
                        { }
                    }


                    if (NextMessage && NeprezkocZpravu && !nextPage)
                    {
                        MessageSpy messageSpy = new MessageSpy();

                        //player name
                        messageSpy.player = WaitForElement(By.XPath(Messages.xpathMessageLinePlayerName.Replace("$", Convert.ToString(i)))).Text;
                        string name = messageSpy.player;
                        if (messageSpy.player.Count()<2)
                            messageSpy.player = "AnyPlayer*";
                        else
                        {

                            while (messageSpy.player[0] == ' ')
                                messageSpy.player = messageSpy.player.Remove(0, 1);
                        }

                        //coordinates
                        //Domovska planeta [1:460:10]
                        string[] coordinates = WaitForElement(By.XPath(Messages.xpathMessagePlanetNameAndCoord.Replace("$", Convert.ToString(i)))).Text.Split('[')[1].Replace("]", "").Split(':');
                        messageSpy.fromPlanet.galaxy = Convert.ToInt32(coordinates[0]);
                        messageSpy.fromPlanet.system = Convert.ToInt32(coordinates[1]);
                        messageSpy.fromPlanet.planet = Convert.ToInt32(coordinates[2]);


                        //resources
                        messageSpy.resources.metal = Resources.ConvertFromText(WaitForElement(By.XPath(Messages.xpathMessageLineMetal.Replace("$", Convert.ToString(i)))).Text);
                        messageSpy.resources.crystal = Resources.ConvertFromText(WaitForElement(By.XPath(Messages.xpathMessageLineCrystal.Replace("$", Convert.ToString(i)))).Text);
                        messageSpy.resources.deuterium = Resources.ConvertFromText(WaitForElement(By.XPath(Messages.xpathMessageLineDeu.Replace("$", Convert.ToString(i)))).Text);
                        messageSpy.lootable = Convert.ToInt32((messageSpy.resources.metal + messageSpy.resources.crystal + messageSpy.resources.deuterium) *
                            (Convert.ToDouble(WaitForElement(By.XPath(Messages.xpathMessageLineLootable.Replace("$", Convert.ToString(i)))).Text.Replace(" ", "").Replace("%", "").Split(':')[1]) / 100))
                            ;

                        //fleet and defence
                        try
                        {
                            messageSpy.fleetResorces = Resources.ConvertFromText(driver.FindElement(By.XPath(Messages.xpathMessageLineFleet.Replace("$", Convert.ToString(i)))).Text);
                            messageSpy.defenceResorces = Resources.ConvertFromText(driver.FindElement(By.XPath(Messages.xpathMessageLineDefence.Replace("$", Convert.ToString(i)))).Text);

                        }
                        catch (Exception)
                        {
                            messageSpy.fleetResorces = -1;
                            messageSpy.defenceResorces = -1;
                        }

                        /*
                        //api
                        bool api = true;
                        int cycle = 1;
                        Actions action = new Actions(driver);

                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(By.XPath(Messages.xpathMessageApiPicture.Replace("$", Convert.ToString(i)))));
                        IWebElement webDriverWait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementIsVisible(By.XPath(Messages.xpathMessageApiPicture.Replace("$", Convert.ToString(i)))));
                        action.MoveToElement(driver.FindElement(By.XPath(Messages.xpathMessageApiPicture.Replace("$", Convert.ToString(i))))).Perform();
                        ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath("//div[@class='tab_inner ctn_with_trash clearfix']"));

                        while (api)
                        {
                            try
                            {
                                string path = "/html[1]/body[1]/div[" + Convert.ToString(cycle) + "]";
                                IWebElement webElement = driver.FindElement(By.XPath(path));

                                if (!webElement.GetAttribute("style").Contains("-10000px") && webElement.GetAttribute("style").Contains("1000000"))
                                {
                                    messageSpy.api = WaitForElement(By.XPath(Messages.xpathMessageApiNumber.Replace("$", Convert.ToString(cycle)))).GetAttribute("value");
                                    api = false;
                                }


                            }
                            catch (Exception)
                            {
                            }
                            cycle++;
                            if (cycle == 500)
                                cycle = 1;
                        }
                        */

                        //Already attacked?
                        try
                        {
                            driver.FindElement(By.XPath(Messages.xpathMessageAttackAlreadyUnderAttack.Replace("$", Convert.ToString(i))));
                            messageSpy.alreadyAttacked = 1;
                        }
                        catch (Exception)
                        {
                        }



                        //neobsahuje již zprávu ze stejné planety?
                        bool notContainsMessage = true;
                        foreach (var item in messageSpies)
                        {
                            if (item.fromPlanet.galaxy == messageSpy.fromPlanet.galaxy)
                                if (item.fromPlanet.system == messageSpy.fromPlanet.system)
                                    if (item.fromPlanet.planet == messageSpy.fromPlanet.planet)
                                        if (item.fromPlanet.moon == messageSpy.fromPlanet.moon)
                                            if (item.player == messageSpy.player)
                                                notContainsMessage = false;

                        }
                        if (notContainsMessage)
                            messageSpies.Add(messageSpy);


                    }

                    if (nextPage && actualPage != maxPage && NextMessage)
                    {
                        WaitForElement(By.XPath(Messages.xpathPageNext)).Click();
                        actualPage++;
                        bool nextPageNotLoaded = true;
                        int counter = 0;
                        while (nextPageNotLoaded)
                        {
                            try
                            {
                                
                                IWebElement webDriverWaitNextPage = new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementIsVisible(By.XPath(Messages.xpathPageNumber)));

                                string text = webDriverWaitNextPage.Text;
                                if (text == Convert.ToString(actualPage) + "/" + Convert.ToString(maxPage))
                                    nextPageNotLoaded = false;
                            }
                            catch (Exception)
                            {
                                //-Automatic logout handler
                                if (counter > 10)
                                {
                                    driver.Navigate().Refresh();
                                }
                                counter++;
                            }

                        }
                        i = 0;
                        /*
                        while (WaitForElement(By.XPath(Messages.xpathPageNumber)).Text != actualPage + "/" + maxPage)
                        { Thread.Sleep(50); }
                        */
                    }

                }

            }


            int i10 = 0;
            return messageSpies;
        }
        public void MessagesOpenMessages()
        {
            bool i = false;
            //already opened?
            try
            {
                driver.FindElement(By.XPath(Messages.xpathType.Replace('&', '1')));
            }
            catch (Exception)
            {
                i = true;
            }
            if (i)
            {
                 
                WaitForElement(By.XPath(Messages.xpathMessagesIcon)).Click();


                while (i)
                {
                    try
                    {
                        if (WaitForElement(By.XPath(Messages.xpathType.Replace('&', '1'))).GetAttribute("aria-selected") == "true")
                            i = false;
                    }
                    catch (Exception)
                    { i = true; }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">1-6</param>
        public void MessagesOpenType(int type)
        {
            MessagesOpenMessages();
            bool i = false;
            //already opened?
            try
            {
                if (driver.FindElement(By.XPath(Messages.xpathType.Replace("&", Convert.ToString(type)))).GetAttribute("aria-selected") != "true")
                    i = true;
            }
            catch (Exception)
            {
                i = true;
            }
            if (i)
            {
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/ul[1]/li[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[2]/div[1]/ul[1]/li[2]
                ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/div[2]/div[1]/ul[1]/li[&]
                IWebElement webElement = WaitForElement(By.XPath(Messages.xpathType.Replace("&", Convert.ToString(type))));
                WaitForElement(By.XPath(Messages.xpathType.Replace("&", Convert.ToString(type)))).Click();
                while (i)
                {
                    try
                    {
                        if (WaitForElement(By.XPath(Messages.xpathType.Replace("&", Convert.ToString(type)))).GetAttribute("aria-selected") == "true")
                            i = false;
                    }
                    catch (Exception)
                    { i = true; }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">1-2</param>
        /// <param name="subtype">1-5</param>
        public void MessagesOpenType(int type, int subtype)
        {
            MessagesOpenMessages();
            bool i = false;
            //already opened?
            try
            {
                if (driver.FindElement(By.XPath(Messages.xpathType.Replace("$", Convert.ToString(type)).Replace("&", Convert.ToString(subtype)))).GetAttribute("aria-selected") != "true")
                    i = true;
            }
            catch (Exception)
            {
                i = true;
            }
            if (i)
            {

                MessagesOpenType(type);
                WaitForElement(By.XPath(Messages.xpathSubType.Replace("$", Convert.ToString(type)).Replace("&", Convert.ToString(subtype)))).Click();
                while (i)
                {
                    try
                    {
                        if (WaitForElement(By.XPath(Messages.xpathSubType.Replace("$", Convert.ToString(type)).Replace("&", Convert.ToString(subtype)))).GetAttribute("aria-selected") == "true")
                            i = false;
                    }
                    catch (Exception)
                    { i = true; }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startingPlanet">start from 1</param>
        /// <param name="distance">distance in cyrcle</param>
        public void FarmInactive(int startingPlanet, int distance, int requestedRank)
        {
            //get info
            List<Coordinates> coordinates = InfoPlayerCoordinatesOfPlanets();
            List<GalaxySystem> galaxySystems = GalaxyScan(startingPlanet, coordinates[startingPlanet - 1], distance, 1, requestedRank,true);
            Thread.Sleep(TimeSpan.FromMinutes(2));//použít letky-start /end
            List<MessageSpy> messageSpies = GetSpyMessages(120);
            ProductionSeting productionSeting = InfoProductionSeting();
            int[] myHangar = InfoHangar();

            //1/10/365 -4,5M
            //1/3/427-600k
            SortedList<double, MessageSpy> sortedMessages = new SortedList<double, MessageSpy>();

            for (int i = 0; i < messageSpies.Count(); i++)
            {
                double poměr = (double)messageSpies[i].lootable / (double)(productionSeting.resources.metal + productionSeting.resources.crystal + productionSeting.resources.deuterium);
                bool notPassed = true;
                while (notPassed)
                {

                    try
                    {
                        sortedMessages.Add(poměr, messageSpies[i]);
                        notPassed = false;
                    }
                    catch (Exception)
                    {
                        poměr = poměr * 0.999999999;
                    }
                }


            }
            for (int i = 0; i < sortedMessages.Count(); i++)
            {
                int[] fleet = new int[13] { 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                if (sortedMessages.Values[sortedMessages.Count() - i - 1].alreadyAttacked == 0)
                    if (sortedMessages.Values[sortedMessages.Count() - i - 1].fleetResorces != -1 && sortedMessages.Values[sortedMessages.Count() - i - 1].defenceResorces != -1)
                        if (sortedMessages.Values[sortedMessages.Count() - i - 1].fleetResorces + sortedMessages.Values[sortedMessages.Count() - i - 1].defenceResorces < 3000)
                            if (sortedMessages.Keys[sortedMessages.Count() - i - 1] > 3)//3násopek produkce
                            {
                                fleet[8] = sortedMessages.Values[sortedMessages.Count() - i - 1].lootable / 5000 + 2;

                                FleetSend(coordinates[startingPlanet], fleet, sortedMessages.Values[sortedMessages.Count() - i - 1].fromPlanet, 10, 8, new Resources { metal = 0, crystal = 0, deuterium = 0 });

                                //already attacked
                                MessageSpy messageSpy = sortedMessages.Values[sortedMessages.Count() - i - 1];
                                messageSpy.alreadyAttacked = 1;

                            }

            }
        }

        public void FarmInactive(int startingPlanet, int distance, double distanceCoefficient, bool simulate, int forceMultiplier, int minimalHourResources, int requestedRank)
        {
            //get info
            List<Coordinates> coordinates = InfoPlayerCoordinatesOfPlanets();
            List<GalaxySystem> galaxySystems = GalaxyScan(startingPlanet, coordinates[startingPlanet - 1], distance, 1, requestedRank,true);
            Thread.Sleep(TimeSpan.FromMinutes(2));//použít letky-start /end
            List<MessageSpy> messageSpies = GetSpyMessages(120);
            ProductionSeting productionSeting = InfoProductionSeting();

            Level[] research = InfoResearch();
            UnitInfo[] myShips = Military.Military.GetMyFleetInfo();//basic unit info
            Military.PlayerMilitary player = new PlayerMilitary();//for interact with unit info and getting real property
            player.HyperTech = research[(int)ResearchInfo.driveHyper].lv;
            player.ImpulseTech = research[(int)ResearchInfo.driveImpulse].lv;
            player.CombustTech = research[(int)ResearchInfo.driveChemical].lv;

            SortedList<double, MessageSpy> sortedMessages = new SortedList<double, MessageSpy>();
            int minimalHourResourcesOrigin = minimalHourResources;
            //TrashSim trashSim = new TrashSim(this);


            //sorting messages
            for (int i = 0; i < messageSpies.Count(); i++)
            {
                //surovinný poměr v hodinnách
                double poměr = ((double)messageSpies[i].lootable / (double)(productionSeting.resources.metal + productionSeting.resources.crystal + productionSeting.resources.deuterium));
                //pripočte se doba letu
                minimalHourResources = minimalHourResourcesOrigin + Convert.ToInt32(2 * (10 + 3500 / 100 * Math.Pow(10 * Coordinates.Distance(coordinates[startingPlanet - 1], messageSpies[i].fromPlanet) / 10000, 0.5)) * distanceCoefficient / 60);




                bool notPassed = true;
                while (notPassed)
                {
                    try
                    {
                        sortedMessages.Add(poměr, messageSpies[i]);
                        notPassed = false;
                    }
                    catch (Exception)
                    {
                        poměr = poměr * 0.999999999;
                    }
                }
            }

            //iterating messages
            for (int i = sortedMessages.Count() - 1; i > -1; i--)
            {
                if (sortedMessages.Values[i].alreadyAttacked == 0)
                    if (sortedMessages.Values[i].fleetResorces != -1 && sortedMessages.Values[i].defenceResorces != -1)//spy message know fleet and defence
                        if (sortedMessages.Keys[i] > minimalHourResources)//3násopek produkce
                        {
                            MyFleetInfo myFleetInfo = Military.Military.GetMyFleetTotalInfo(startingPlanet, this);//pro jistotu to přečte aktualni stav flotily

                            MyFleetInfo FleetToSend = MyFleetInfo.Inicialization();


                            //have we enought small cargo
                            bool useFaster = false;
                            bool useFasterDefAndFleet = false;
                            if (sortedMessages.Values[i].lootable < myFleetInfo.myHangar[(int)UnitType.SmallCargo] * UnitInfo.GetInfo(UnitType.Destroyer).Capacity)
                                useFaster = true;
                            //which ships are faster than smallcargo?
                            MyFleetInfo myFleetFaster = MyFleetInfo.Inicialization();//fleet of faster ships -without civil ships
                            MyFleetInfo myFleetFasterDefence = MyFleetInfo.Inicialization();//faster fleet against defence
                            MyFleetInfo myFleetFasterFleet = MyFleetInfo.Inicialization();//faster fleet against fleet
                            if (useFaster)
                            {
                                myFleetFaster.myHangar[(int)UnitType.Cruiser] = myFleetInfo.myHangar[(int)UnitType.Cruiser];
                                myFleetFaster.myHangar[(int)UnitType.Battleship] = myFleetInfo.myHangar[(int)UnitType.Battleship];
                                myFleetFaster.myHangar[(int)UnitType.Battlecruiser] = myFleetInfo.myHangar[(int)UnitType.Battlecruiser];


                                if (myShips[(int)UnitType.Destroyer].GetActualSpeed(player) >= myShips[(int)UnitType.SmallCargo].GetActualSpeed(player))
                                    myFleetFaster.myHangar[(int)UnitType.Destroyer] = myFleetInfo.myHangar[(int)UnitType.Destroyer];
                                if (myShips[(int)UnitType.Bomber].GetActualSpeed(player) >= myShips[(int)UnitType.SmallCargo].GetActualSpeed(player))
                                    myFleetFaster.myHangar[(int)UnitType.Bomber] = myFleetInfo.myHangar[(int)UnitType.Bomber];


                                myFleetFaster = Military.Military.GetMyFleetTotalInfo(false, myFleetFaster.myHangar);
                            }

                            //is faster fleet strong enaught?
                            if (useFaster)
                            {
                                if (myFleetFaster.myHangarResuourcesTotal > forceMultiplier * sortedMessages.Values[i].defenceResorces + sortedMessages.Values[i].fleetResorces)
                                    useFaster = true;
                                else useFaster = false;
                            }

                            //is fasted units against defence and fleet strong enought?
                            if (useFaster)
                            {
                                //faster fleet against defence
                                myFleetFasterDefence.myHangar[(int)UnitType.Cruiser] = myFleetFaster.myHangar[(int)UnitType.Cruiser];
                                myFleetFasterDefence.myHangar[(int)UnitType.Battleship] = myFleetFaster.myHangar[(int)UnitType.Battleship];
                                myFleetFasterDefence.myHangar[(int)UnitType.Bomber] = myFleetFaster.myHangar[(int)UnitType.Bomber];

                                myFleetFasterDefence = Military.Military.GetMyFleetTotalInfo(false, myFleetFasterDefence.myHangar);
                                if (myFleetFasterDefence.myHangarResuourcesTotal >= forceMultiplier * sortedMessages.Values[i].defenceResorces)
                                    useFasterDefAndFleet = true;


                                //faster fleet against fleet
                                myFleetFasterFleet.myHangar[(int)UnitType.Battlecruiser] = myFleetFaster.myHangar[(int)UnitType.Battlecruiser];
                                myFleetFasterFleet.myHangar[(int)UnitType.Destroyer] = myFleetFaster.myHangar[(int)UnitType.Destroyer];

                                myFleetFasterFleet = Military.Military.GetMyFleetTotalInfo(false, myFleetFasterFleet.myHangar);
                                if (myFleetFasterFleet.myHangarResuourcesTotal >= forceMultiplier * sortedMessages.Values[i].fleetResorces && useFasterDefAndFleet)
                                    useFasterDefAndFleet = true;
                                else useFasterDefAndFleet = false;
                            }

                            //fleet to send
                            if (useFaster && useFasterDefAndFleet)
                            {
                                Double test01 = (double)myShips[3].Cost.TotalResources();
                                Double test02 = (double)myFleetFasterDefence.resuourcesTypeTotalPercentage[3] * forceMultiplier * sortedMessages.Values[i].defenceResorces
                                                                 / (double)myShips[3].Cost.TotalResources();

                                for (int a = 0; a < 8; a++)
                                {
                                    FleetToSend.myHangar[a] += Convert.ToInt32((double)myFleetFasterDefence.resuourcesTypeTotalPercentage[a] * forceMultiplier * sortedMessages.Values[i].defenceResorces
                                                                 / (double)myShips[a].Cost.TotalResources());
                                    FleetToSend.myHangar[a] += Convert.ToInt32((double)myFleetFasterFleet.resuourcesTypeTotalPercentage[a] * forceMultiplier * sortedMessages.Values[i].fleetResorces
                                         / (double)myShips[a].Cost.TotalResources());
                                }
                                FleetToSend.myHangar[(int)UnitType.SmallCargo] = sortedMessages.Values[i].lootable / myShips[(int)UnitType.SmallCargo].Capacity + 1;

                                //simulation
                                int[] researchLevels = new int[research.Count()];
                                for (int a = 0; a < research.Count(); a++)
                                { researchLevels[a] = research[a].lv; }

                                //trashSim.SimulateBattle(sortedMessages.Values[i].api, FleetToSend.myHangar, researchLevels, coordinates[startingPlanet - 1]);


                                //sending fleet
                                FleetSend(coordinates[startingPlanet], FleetToSend.myHangar, sortedMessages.Values[i].fromPlanet, 10, 8, new Resources { metal = 0, crystal = 0, deuterium = 0 });
                                ;
                            }


                        }
                /*
                // //settin amouth of small cargo needed
                int[] fleet = new int[13] { 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                if (sortedMessages.Values[i].alreadyAttacked == 0)
                    if (sortedMessages.Values[i].fleetResorces != -1 && sortedMessages.Values[i].defenceResorces != -1)//spy message know fleet and defence
                        if (sortedMessages.Keys[i] > 3)//3násopek produkce
                            if (sortedMessages.Values[i].fleetResorces + sortedMessages.Values[i].defenceResorces < 3000)
                            {
                                fleet[8] = sortedMessages.Values[i].lootable / 5000 + 2;

                                FleetSend(coordinates[startingPlanet], fleet, sortedMessages.Values[i].fromPlanet, 10, 8, new Resources { metal = 0, crystal = 0, deuterium = 0 });

                                //already attacked
                                MessageSpy messageSpy = sortedMessages.Values[i];
                                messageSpy.alreadyAttacked = 1;

                            }
*/
            }

            //trashSim.TrashDriver.Close();

        }




        /// <summary>
        /// planets starts from 1,playerinfo must be loaded!
        /// </summary>
        /// <returns></returns>
        public List<int> UnderAttack()
        {
            List<int> planets = new List<int>();

            
            int amount = InfoPlayerNumberOfPlanets();
            List<Coordinates> myCoordinates = InfoPlayerCoordinatesOfPlanets();
            FleetMovements fleetMovements = InfoFleetMovement();

            for (int i = 0; i < fleetMovements.fleetMovements.Count(); i++)
            {
                for (int b = 0; b < myCoordinates.Count(); b++)//b+1=my planet
                {
                    if (myCoordinates[b].NumberOfSystems() == fleetMovements.fleetMovements[i].destCoords.NumberOfSystems())
                    {
                        if (fleetMovements.fleetMovements[i].missionFleet == (int)FleetMision.attack)
                        {
                            MyFleetInfo enemyFleet = Military.Military.GetMyFleetTotalInfo(false, fleetMovements.fleetMovements[i].fleetDetails);
                            MyFleetInfo myFleet = Military.Military.GetMyFleetTotalInfo(b + 1, this);

                            if (2*enemyFleet.myHangarResuourcesTotal>myFleet.myHangarResuourcesTotal)//if enemy is 1/2* stronger than me I will try to save my fleet
                            {
                                /*
                                !save resources 
                                =somebody atacking to my planet
                                -rescure fleet =send it to another planet
                                -withraw all fleet sended to that planet with arrival time before enemys arrival time
                                -get help prom others fleet if their arrival time is shorter than enemys arrival time 
                                 and their power is much stronger than ennemys power.
                                */

                                //transport fleet to clossest planet in clossest system.

                                ChangePlanet(b+1);
                                GalaxySystem scannedSystem = this.GalaxyScan(myCoordinates[b].galaxy, myCoordinates[b].system,0,1000000, false);
                                if (scannedSystem.positions.Count() > 0)//at least one planet
                                {
                                    if (scannedSystem.positions[0].playerName==InfoPlayerName())
                                    {

                                    }

                                }






                                //!if player have only one planet no fleetsave, maybe use the closest planet-basic fleetsave
                                int coordinatesTo = 0;
                                if (b + 1 < amount)
                                    coordinatesTo = b + 1;
                                else if (b - 1 > 0)
                                    coordinatesTo = b - 1;


                                FleetSend(myCoordinates[b], myFleet.myHangar, myCoordinates[coordinatesTo], 10, (int)FleetMision.deployment, new Resources { metal = 0, crystal = 0, deuterium = 0 });


                                ;
                            }
                            ;


                        }
                    }
                }

            }


            /*
            int amount = player.planets.Count;//namísto zjistění realného množství zjistí aktualní uložené množství 

            //WaitForElement(By.Id(idUnderAttack));
            driver.FindElement(By.XPath("/html[1]/body[1]/div[2]/div[2]/div[1]/div[1]/div[7]/div[2]/a[1]"));

            for (int i2 = 1; i2 < amount + 1; i2++)
            {
                try
                {
                    //class="activity"  -the same element as buildings
                    driver.FindElement(By.XPath(Overview.xpathPlanetUnderAttack.Replace("&", Convert.ToString(i2))));
                    planets.Add(i2);
                    try
                    {
                        player.underAtack.Add(new UnderAttack() { planet = player.planets[i2].coordinates });//not enought info
                    }
                    catch (Exception e)
                    {
                        FilesOperations.ErrorLogFileAddError(e);
                        //player isnt loaded
                        int i = 0;
                    }
                }
                catch (Exception)
                { }
            }

            //throw event for reacting or waiting for reaction because we are under attack!

            */
            return planets;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinatesFrom"></param>
        /// <param name="moonFrom"></param>
        /// <param name="fleet"></param>
        /// <param name="coordinatesTo">1=planet,2moon,3derbys</param>
        /// <param name="speed">1-10</param>
        /// <param name="mision"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        public bool FleetSend(Coordinates coordinatesFrom, int[] fleet, Coordinates coordinatesTo, byte speed, byte mision, Resources resources)  //fleet=> -1 = max mnozství typu lodě    //int[] fleet,int planetFrom,bool moonFrom, int planetTo, bool moonTo
        {
            //!nelze odesílat letku z měsíce
            //!občas může byt cena deu za přepravu větší než nakl prostor

            //coordinates into planet
            int planet = 1;
            int i2 = 1;
            foreach (Player.Planet item in player.planets)
            {
                if (coordinatesFrom.galaxy == item.coordinates.galaxy && coordinatesFrom.planet == item.coordinates.planet && coordinatesFrom.moon == item.coordinates.moon)
                {
                    planet = i2;
                }
                i2++;
            }
            i2 = 0;
            ChangePlanet(planet);
            ChangeMenu(Menutab.Letky);

            string xpathfleetPutAmount = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[%]/div[2]/form[1]/div[$]/ul[1]/li[&]/input[1]";
            string xpathFleetSpeed = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/form[1]/div[1]/div[4]/div[2]/div[1]/ul[1]/li[3]/div[1]/a[&]";
            string xpathFleetMision = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[5]/div[2]/ul[1]/li[&]/a[1]";
            string relxpathFleetNextButon1_2 = "//a[@id='continue']/span[1]";//class="on"

            string relxpathFleetNextButon3 = "//a[@id='start']/span[1]";
            string xpathFleetIfleetslots = "/html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[2]/div[1]/div[1]/span[1]";

            IWebElement webElement1 = WaitForElement(By.XPath(xpathFleetIfleetslots));
            string string1 = WaitForElement(By.XPath(xpathFleetIfleetslots)).Text;//letek : 1/13

            //free fleet slot?
            if (Convert.ToInt32(WaitForElement(By.XPath(xpathFleetIfleetslots)).Text.Replace(" ", "").Split(':')[1].Split('/')[0]) < Convert.ToInt32(WaitForElement(By.XPath(xpathFleetIfleetslots)).Text.Replace(" ", "").Split(':')[1].Split('/')[1]))
            {

                //sending fleet I
                {
                    try
                    {
                        IWebElement webElement = driver.FindElement(By.Id("warning"));
                        if (webElement != null)
                        {
                            return false;//není zde flotila
                        }
                    }
                    catch (Exception) { }


                    for (int i = 1; i < 14; i++)
                    {

                        int a = i;
                        int b = 1;
                        if (i > 8)
                        { a -= 8; b = 3; }

                        string xpath1 = xpathfleetPutAmount.Replace("%", "7").Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a));//počet lodí
                        string xpath2 = xpathfleetPutAmount.Replace("%", "6").Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a));//počet lodí
                        string xpath3 = xpathfleetPutAmount.Replace("%", "5").Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a));//počet lodí

                        if (fleet[i - 1] != 0)
                        {

                            IWebElement webElement;
                            try
                            {
                                webElement = driver.FindElement(By.XPath(xpath1));//pocet lodí
                                webElement.SendKeys(Convert.ToString(fleet[i - 1]));
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    webElement = driver.FindElement(By.XPath(xpath2));//pocet lodí
                                    webElement.SendKeys(Convert.ToString(fleet[i - 1]));
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        webElement = driver.FindElement(By.XPath(xpath3));//pocet lodí
                                        webElement.SendKeys(Convert.ToString(fleet[i - 1]));
                                    }
                                    catch (Exception e)
                                    {
                                        FilesOperations.ErrorLogFileAddError(e);

                                        //nejsou k dispozici lode tohoto typu
                                        int i3 = 0;//nelze najít prvek
                                    }
                                }
                            }
                        }
                    }
                    bool unfinished = true;
                    while (unfinished)
                    {
                        try
                        {
                            driver.FindElement(By.XPath(relxpathFleetNextButon1_2)).Click();
                            unfinished = false;
                        }
                        catch (Exception)
                        { }
                    }
                    Thread.Sleep(1000);
                }


                //sending fleet II
                {
                    //+is enought deu? -cost: /html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/form[1]/div[1]/div[4]/div[2]/div[1]/ul[1]/li[2]/span[1]/span[1]/span[1]
                    bool unloaded = true;
                    while (unloaded)
                    {
                        try
                        {                            
                            WaitForElement(By.Id("galaxy")).Clear();
                            unloaded = false;
                        }
                        catch (Exception)
                        { }
                    }
                    WaitForElement(By.Id("galaxy")).SendKeys(Convert.ToString(coordinatesTo.galaxy));
                    WaitForElement(By.Id("system")).Clear();
                    WaitForElement(By.Id("system")).SendKeys(Convert.ToString(coordinatesTo.system));
                    WaitForElement(By.Id("position")).Clear();
                    WaitForElement(By.Id("position")).SendKeys(Convert.ToString(coordinatesTo.planet));
                    if (coordinatesTo.moon == 1)
                        WaitForElement(By.Id("pbutton")).Click();
                    if (coordinatesTo.moon == 2)
                        WaitForElement(By.Id("mbutton")).Click();
                    if (coordinatesTo.moon == 3)
                        WaitForElement(By.Id("dbutton")).Click();
                    WaitForElement(By.XPath(xpathFleetSpeed.Replace("&", Convert.ToString(speed)))).Click();
                    ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/form[1]/div[1]/div[4]/div[2]/table[1]/tbody[1]/tr[2]/td[1]/div[2]/a[1] //planeta vzlet
                    ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/form[1]/div[1]/div[4]/div[2]/table[1]/tbody[1]/tr[2]/td[1]/div[2]/a[2] //mesíc vzlet
                    bool unfinished = true;
                    while (unfinished)
                    {
                        try
                        {
                            driver.FindElement(By.XPath(relxpathFleetNextButon1_2)).Click();
                            unfinished = false;
                        }
                        catch (Exception)
                        { }
                    }
                    Thread.Sleep(1000);
                }

                //sending fleet III
                {
                    bool unloaded = true;
                    while (unloaded)
                    {
                        try
                        {
                            WaitForElement(By.XPath(xpathFleetMision.Replace("&", Convert.ToString(mision)))).Click();
                            unloaded = false;
                        }
                        catch (Exception)
                        { }
                    }
                    WaitForElement(By.Id("metal")).Clear();
                    WaitForElement(By.Id("metal")).SendKeys(Convert.ToString(resources.metal));
                    WaitForElement(By.Id("crystal")).Clear();
                    WaitForElement(By.Id("crystal")).SendKeys(Convert.ToString(resources.crystal));
                    WaitForElement(By.Id("deuterium")).Clear();
                    WaitForElement(By.Id("deuterium")).SendKeys(Convert.ToString(resources.deuterium));


                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(By.XPath(relxpathFleetNextButon3)));
                    IWebElement webDriverWait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000)).Until(ExpectedConditions.ElementIsVisible(By.XPath(relxpathFleetNextButon3)));
                    bool unfinished = true;
                    while (unfinished)
                    {
                        try
                        {
                            driver.FindElement(By.XPath(relxpathFleetNextButon3)).Click();
                            unfinished = false;
                        }
                        catch (Exception)
                        { }
                    }

                    Thread.Sleep(1000);

                }
                return true;
            }
            return false;
        }


    }


    public struct Level
    {
        public int lv;
        public byte posible;
        public int metal;
        public int crystal;
        public int deuterium;
    }
    public struct Resources
    {
        public int metal;
        public int crystal;
        public int deuterium;
        public int energy;
        public static int ConvertFromText(string text)
        {
            if (text.Contains("M"))
                return Convert.ToInt32(text.Replace(".", "").Replace(",", "").Replace(" ", "").Replace("M", "").Split(':')[1]) * 1000;

            return Convert.ToInt32(text.Replace(".", "").Replace(" ", "").Split(':')[1]);

        }
        
            /// <summary>
            /// warious. všeobecné
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
        public static int ConvertNumberFromText(string text)
        {
            if (text.Contains("M"))
                return Convert.ToInt32(text.Replace(".", "").Replace(",", "").Replace(" ", "").Replace("M", "")) * 1000;

            return Convert.ToInt32(text.Replace(".", "").Replace(" ", ""));

        }
        public Resources(int metal, int crystal, int deuterium)
        {
            this.metal = metal; this.crystal = crystal; this.deuterium = deuterium; this.energy = 0;
        }
        /// <summary>
        /// m+cr+deu
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int TotalResources(Resources resources)
        {
            return resources.metal + resources.crystal + resources.deuterium;
        }
        public int TotalResources()
        {
            return this.metal + this.crystal + this.deuterium;
        }
    }
    public struct Coordinates
    {
        public int galaxy;
        public int system;
        public int planet;
        /// <summary>
        /// planet=0/1,2moon,3derbys
        /// </summary>
        public byte moon;
        private static int maxSystem = 499;

        /// <summary>
        /// math +or- for coordinations
        /// </summary>
        /// <param name="systems"></param>
        /// <returns></returns>
        public Coordinates Change(int systems)
        {
            Coordinates coordinates = this;
            if (coordinates.system + systems >= maxSystem + 1 || coordinates.system + systems <= 0)
            {
                int galaxychange = (coordinates.system + systems) / maxSystem;
                coordinates.galaxy += galaxychange;
                coordinates.system += systems - maxSystem * galaxychange;
            }
            else
            {
                coordinates.system += systems;
            }
            return coordinates;
        }

        /// <summary>
        /// math +or- for coordinations
        /// </summary>
        /// <param name="systems"></param>
        /// <returns></returns>
        public static Coordinates Change(Coordinates coordinates, int systems)
        {

            return coordinates.Change(systems);
        }

        public int NumberOfSystems()
        {
            return (this.galaxy * maxSystem + this.system);


        }

        public static Coordinates ConvertFromText(string text)
        {
            Coordinates coord = new Coordinates();
            //inputy:
            //Domovska planeta [1:460:10]
            //[1:460:10]
            string[] coordinates = text.Insert(0, "text ").Split('[')[1].Replace("]", "").Split(':');
            coord.galaxy = Convert.ToInt32(coordinates[0]);
            coord.system = Convert.ToInt32(coordinates[1]);
            coord.planet = Convert.ToInt32(coordinates[2]);

            return coord;
        }

        /// <summary>
        /// for counting ships travel time
        /// </summary>
        /// <returns></returns>
        public static int Distance(Coordinates from, Coordinates to)
        {
            if (from.galaxy != to.galaxy)
            {
                return Math.Abs(from.galaxy - to.galaxy) * 20000;
            }
            else if (from.system != to.system)
            {
                return Math.Abs(from.system - to.system) * 95 + 2700;
            }
            else if (from.planet != to.planet)
            {
                return Math.Abs(from.planet - to.planet) * 5 + 1000;
            }
            else
            {
                return 5;
            }
        }
    }
    public struct UnderAttack
    {

        public Coordinates planet;
        public int enemy;//when not enaught spy LV
        public int[] enemyFleet;
    }

    public class ProductionSeting
    {
        public int factorKey;
        public Resources resources;
        /// <summary>
        /// 6ks
        /// </summary>
        public byte[] percentage = new byte[6];
    }

    public class GalaxySystem
    {
        public int galaxy;
        public int system;
        public List<Position> positions = new List<Position>();
        public struct Position
        {
            public byte active;
            /// <summary>
            /// start from 1
            /// </summary>
            public int position;
            public bool moon;
            public int derbysMetal;
            public int derbysCrystal;

            public string playerName;
            public string playerExtraInfo;
            public string alli;
            public int rank;
        }

    }

    public class MessageSpy
    {
        public Coordinates fromPlanet;
        public string player;
        public Resources resources;
        public int lootable;
        /// <summary>
        /// -1=unknown
        /// </summary>
        public int fleetResorces;
        /// <summary>
        /// -1=unknown
        /// </summary>
        public int defenceResorces;
        public string api;
        public byte alreadyAttacked;

    }



    /// <summary>
    /// fleet movement
    /// </summary>
    public struct FleetMovements
    {
        public DateTime scanned;
        public List<FleetMovement> fleetMovements;
        public static FleetMovements Inicialization()
        {
            FleetMovements fleetMovements = new FleetMovements { fleetMovements = new List<FleetMovement> () };

            return fleetMovements;
        }

        public struct FleetMovement
        {
            public DateTime arrivalTime;//13:14:20
            public byte missionFleet;
            public string originFleet;//Ztorm 
            public Coordinates coordsOrigin;//[1:446:4]
            public int[] fleetDetails;//13 156 4 0 4....
            //public int[] fleetAmouth;//134 -total number of ships usuable during ennemy attack but player dont have spy lv
            /// <summary>
            /// from planet to target = true, going back = false
            /// </summary>
            public bool icon_movement;
            public string destFleet;//The land of Dragons
            public Coordinates destCoords;//[1:296:6] //white charakters
            public Resources resources;


            public static FleetMovement Inicialization()
            {
                FleetMovement fleetMovement = new FleetMovement { fleetDetails = new int[13] };

                return fleetMovement;
            }


        }
    }
}
