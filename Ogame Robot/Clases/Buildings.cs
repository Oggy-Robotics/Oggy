using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ogame_Robot.Clases
{
    public class Buildings : BrowserManipulation
    {
        public BrowserManipulation browserManipulation;

        public Buildings(BrowserManipulation browserManipulation)
        {
            this.browserManipulation = browserManipulation;
        }

        public void Inicialization()
        {

        }





        public TimeSpan AutomatickaStavba(BrowserManipulation browse, int vychozíRezim)
        {
            List<Player.Planet> PlanetaHrace = new List<Player.Planet>();
            List<string> cas_dalsiho_volani = new List<string>();

            /*       Režim                  */
            List<int> rezim = new List<int>();
            for (int i2 = 0; i2 < browserManipulation.InfoPlayerNumberOfPlanets(); i2++)
            {
                rezim.Add(vychozíRezim);   //výchozí režim je 0 => nedělá nic
                PlanetaHrace.Add(new Player.Planet());
            }
            ; //možnost vybrat stavební režim pro planety
            //rezim[4] = 2; rezim[6] = 2;
            /*  Nastavuje 5. a 7. planetu do režimu 2  */



            for (int a = 0; a < browserManipulation.InfoPlayerNumberOfPlanets(); a++)
            {
                //Pro planety v režimu 0 ani nebudem hledat info :D
                if (rezim[a] != 0)
                {
                    PlanetaHrace[a].suply = browserManipulation.InfoSuply(a + 1);   //získání infa o dolech atd na planetě 1->X
                }
                //Jaký je režim?
                switch (rezim[a])
                {
                    /*     čas = -1            */
                    case 0: {/* Recess -> nestaví */} break;
                    
                    case 1:/* Balanced 
                        Snaží se mít kov a krystal na stejné urovni 
                        když dul na kov je nižší než nebo roven krystalu, vylepší ho, jinak vylepší krystal*/
                        {
                            //                  kov    je menší nebo roven     krystal
                            if (PlanetaHrace[a].suply[0].lv <= PlanetaHrace[a].suply[1].lv)
                            {
                                try//dá se něco postavit?
                                {
                                    browserManipulation.UpgradeSuply(1);    // 1=kov
                                    try//získej čas stavby
                                    {
                                        cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                    }
                                    catch(Exception e)
                                    {
                                        FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                        ;
                                    }
                                }
                                catch (Exception)//když už se staví
                                {
                                    try//získej čas stavby
                                    {                                                       
                                        cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                        ;
                                    }
                                }
                            }
                            //                  kov    je větší než           krystal 
                            if (PlanetaHrace[a].suply[0].lv > PlanetaHrace[a].suply[1].lv)
                            {
                                try//dá se něco postavit?
                                {

                                    browserManipulation.UpgradeSuply(2);    // 2=krystal
                                    try//získej čas stavby
                                    {
                                        cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                        ;
                                    }
                                }
                                catch (Exception)//když už se staví
                                {
                                    try//získej čas stavby
                                    {
                                        cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                        ;
                                    }
                                }
                            }
                        }
                        break;

                    case 2:/* Balanced+Solar
                        Snaží se mít kov,krystal,S.elektrárnu na stejné urovni 
                        když dul na kov je nižší než krystal
                        Elektrárna > Kov > krystal  */
                        {
                            //                  elek    je menší nebo roven     kovu     a nebo              elek    je menší nebo roven     krystalu
                            if ((PlanetaHrace[a].suply[3].lv <= PlanetaHrace[a].suply[0].lv) | (PlanetaHrace[a].suply[3].lv <= PlanetaHrace[a].suply[1].lv))
                            { /* pak se vylepší elektrárna */
                                try//dá se něco postavit?
                                {
                                    browserManipulation.UpgradeSuply(4);    //elektrárna
                                    try//získej čas stavby
                                    {
                                        cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                        ;
                                    }
                                }
                                catch (Exception)//když už se staví nebo nejsou surky
                                {
                                    try//získej čas stavby
                                    {
                                        cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                    }
                                    catch (Exception e)
                                    {
                                        FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                        ;
                                    }
                                }
                            }
                            else /* když je ale elektrárna top minimálně o 1lvl nad nejlepším z dolů (kov/krystal)
                                    pak staví důl, kov má přednost stejně jako v case 1 */
                            {
                                //                  kov    je menší nebo roven     krystal
                                if (PlanetaHrace[a].suply[0].lv <= PlanetaHrace[a].suply[1].lv)
                                {
                                    try//dá se něco postavit?
                                    {
                                        browserManipulation.UpgradeSuply(1);    // 1=kov
                                        try//získej čas stavby
                                        {
                                            cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                        }
                                        catch (Exception e)
                                        {
                                            FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                            ;
                                        }
                                    }
                                    catch (Exception)//když už se staví
                                    {
                                        try//získej čas stavby
                                        {
                                            cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                        }
                                        catch (Exception e)
                                        {
                                            FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                            ;
                                        }
                                    }
                                }
                                //                  kov    je větší než           krystal 
                                if (PlanetaHrace[a].suply[0].lv > PlanetaHrace[a].suply[1].lv)
                                {
                                    try//dá se něco postavit?
                                    {
                                        browserManipulation.UpgradeSuply(2);    // 2=krystal
                                        try//získej čas stavby
                                        {
                                            cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                        }
                                        catch (Exception e)
                                        {
                                            FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                            ;
                                        }
                                    }
                                    catch (Exception)//když už se staví
                                    {
                                        try//získej čas stavby
                                        {
                                            cas_dalsiho_volani.Add(browse.WaitForElement(By.XPath("//span[@class='time']")).Text);
                                        }
                                        catch (Exception e)
                                        {
                                            FilesOperations.ErrorLogFileAddError(e); // CHYBA XPATHU čas stavby
                                            ;
                                        }
                                    }
                                }
                            }

                        }
                        break;

                    case 3:/* ProductionBased
                        Snaží se vyrovnávat produkci (kov je vyšší) */
                        {
                            if (PlanetaHrace[a].suply[0].lv < PlanetaHrace[a].suply[1].lv)
                            {
                                //něco se možná stane
                            }
                        }
                        break;

                    // poměrové nastavování kov[25%].krystal[20%].deu[15%].Solar[16].Fus[0].Solar[x] pomerove
                    case 4:
                        {
                            if (PlanetaHrace[a].suply[0].lv < PlanetaHrace[a].suply[1].lv)
                            {
                                //něco se možná stane
                            }
                        }
                        break;
                }//end switch
            }//end for

            List<TimeSpan> cas = new List<TimeSpan>();
            TimeSpan min_cas = new TimeSpan();
            if (cas_dalsiho_volani.Count == 0)
                cas.Add(TimeSpan.FromSeconds(2));

            for(int i = 0; i < cas_dalsiho_volani.Count; i++)
            {
                cas.Add(ConvertTextToTimespan(cas_dalsiho_volani[i]));
            }
            try
            {
                min_cas=cas.Min();
            }
            catch (Exception e)
            {
                min_cas = TimeSpan.FromMilliseconds(-666);
                FilesOperations.ErrorLogFileAddError(e);//možná že cas.count=0? možná zas nějaká chyba?
            }
            if(min_cas== TimeSpan.FromMilliseconds(-666)) { min_cas = TimeSpan.FromSeconds(-1); }
            return min_cas;
        }

        public TimeSpan ConvertTextToTimespan(string text)
        {
            TimeSpan time = new TimeSpan();
            //1d 35min
            string[] texts = text.Split(' ');
            List<int> numbers = new List<int>();
            List<string> stringy = new List<string>();

            for (int b = 0; b < texts.Count(); b++)
            {
                int pocet = 0;
                for (int i = 0; i < texts[b].Length; i++)
                {
                    int a = -1;
                    try
                    {
                        a = Convert.ToInt32(texts[b][i]);
                    }
                    catch (Exception)
                    {
                    }
                    if (a <= 57 && a >= 48)
                    {
                        pocet++;
                    }

                }
                int a2 = 0;
                int.TryParse(texts[b].Substring(0, pocet), out a2);
                numbers.Add(a2);

                stringy.Add(texts[b].Substring(pocet, texts[b].Length - pocet));

            }

            for (int i = 0; i < stringy.Count; i++)
            {
                switch (stringy[i])
                {
                    case "t":
                        {
                            time = time.Add(TimeSpan.FromDays(7 * numbers[i]));
                        }
                        break;
                    case "d":
                        {
                            time = time.Add(TimeSpan.FromDays(numbers[i]));
                        }
                        break;
                    case "hod":
                        {
                            time = time.Add(TimeSpan.FromHours(numbers[i]));
                        }
                        break;
                    case "min":
                        {
                            time = time.Add(TimeSpan.FromMinutes(numbers[i]));
                        }
                        break;
                    case "s":
                        {
                            time = time.Add(TimeSpan.FromSeconds(numbers[i]));
                        }
                        break;

                }
            }
            return time;
        }


    }
}
