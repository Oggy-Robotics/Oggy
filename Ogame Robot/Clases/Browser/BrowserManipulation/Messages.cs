using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogame_Robot.Clases
{
    public partial class BrowserManipulation
    {

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
                        if (messageSpy.player.Count() < 2)
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
                    {//autologout needed relog
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
    }
}
