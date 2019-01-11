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
        public bool UpgradeSuply(int suply)
        {
            ChangeMenu(Menutab.Zasobovani);

            int a = suply;
            int b = 1;
            if (suply > 6)
            { a -= 6; b = 2; }

            string xpSuplyUpgrade1 = Suply.xpathSuplyUpgrade.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a)).Replace('%', '1');
            string xpSuplyUpgrade2 = Suply.xpathSuplyUpgrade.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a)).Replace('%', '2');

            try
            {
                driver.FindElement(By.XPath(xpSuplyUpgrade1)).Click();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    driver.FindElement(By.XPath(xpSuplyUpgrade2)).Click();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool UpgradeStation(int station)
        {
            ChangeMenu(Menutab.Továrny);

            int a = station;
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[2]/div[1]/div[1]/a[1]/img[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[2]/li[2]/div[1]/div[1]/a[1]/img[1]
            string xpStationsUpgrade1 = Stations.xpathStationsUpgrade.Replace("&", Convert.ToString(a));

            try
            {
                driver.FindElement(By.XPath(xpStationsUpgrade1)).Click();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpgradeResearch(int research)
        {
            ChangeMenu(Menutab.Výzkum);
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[$]/ul[1]/li[&]/div[1]/div[1]/a[%]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[1]/ul[1]/li[3]/div[1]/div[1]/a[1]/img[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[4]/ul[1]/li[2]/div[1]/div[1]/a[1]/img[1]
            //i = turny a=tabulky b=subtabulky
            int a = 0, b = 0;
            if (research < 6)//1-5
            { a = 1; b = research; }
            if (research < 9 && research > 5)//6-8
            { a = 2; b = research - 5; }
            if (research < 14 && research > 8)//9-13
            { a = 3; b = research - 8; }
            if (research < 17 && research > 13)//14-16
            { a = 4; b = research - 13; }


            string xpathResearch = Research.xpathResearchUpgrade.Replace("$", Convert.ToString(a)).Replace("&", Convert.ToString(b)).Replace('%', '1');//posible
            try
            {
                driver.FindElement(By.XPath(xpathResearch)).Click();//lze zkoumat
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool BuildHangar(int ship, int amount)
        {

            ChangeMenu(Menutab.Hangar);
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/div[$]/ul[1]/li[&]/div[1]/div[1]/a[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/div[1]/ul[1]/li[1]/div[1]/div[1]/div[1]/a[2]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/div[2]/ul[1]/li[1]/div[1]/div[1]/a[1]

            int a = ship;
            int b = 1;
            if (ship > 8)
            { a -= 8; b = 2; }
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/div[1]/a[2]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/a[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/a[2]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[4]/div[2]/ul[1]/li[1]/div[1]/div[1]/div[1]/a[2]

            string xpath1 = Hangar.xpathHangar.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a));//počet lodí
            string xpath2 = Hangar.xpathHangarInBuild.Replace("$", Convert.ToString(b)).Replace("&", Convert.ToString(a));//in process //Text = "1\r\n2"
            try
            {
                driver.FindElement(By.XPath(xpath1)).Click();
            }
            catch (Exception)
            {
                try
                {
                    driver.FindElement(By.XPath(xpath2)).Click();//zrovna se staví
                }
                catch (Exception)
                {
                    return false;
                }
            }

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(Hangar.idHangarBuildCount)));
            driver.FindElement(By.Id(Hangar.idHangarBuildCount)).SendKeys(Convert.ToString(amount));
            driver.FindElement(By.XPath(Hangar.xpathHangarBuildConfirm)).Click();

            return true;
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/form[1]/div[1]/div[2]/div[3]/a[1]/span[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/form[1]/div[1]/div[2]/div[3]/a[1]/span[1]
            ///html[1]/body[1]/div[2]/div[2]/div[1]/div[3]/div[2]/div[1]/form[1]/div[1]/div[2]/div[3]/a[1]/span[1]
        }

        public bool BuildDefence(int defence, int amount)
        {

            ChangeMenu(Menutab.Obrana);

            int a = defence;

            string xpath1 = Defence.xpathDefence.Replace("&", Convert.ToString(a));
            string xpath2 = Defence.xpathDefenceInBuild.Replace("&", Convert.ToString(a));//in process

            try
            {
                driver.FindElement(By.XPath(xpath1)).Click();
            }
            catch (Exception)
            {
                try
                {
                    driver.FindElement(By.XPath(xpath2)).Click();//in process
                }
                catch (Exception)
                {
                    return false;
                }
            }
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(Hangar.idHangarBuildCount)));
            driver.FindElement(By.Id(Defence.idDefenceBuildCount)).SendKeys(Convert.ToString(amount));
            driver.FindElement(By.XPath(Defence.xpathDefenceBuildConfirm)).Click();

            return true;
        }


    }
}
