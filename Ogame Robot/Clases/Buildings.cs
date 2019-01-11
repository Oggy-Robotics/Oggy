using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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





        public void AutomatickaStavba(BrowserManipulation browser)
        {
            this.browserManipulation = browser;
            List<Player.Planet> PlanetaHrace = InfoDoly();
            List<int> rezim = new List<int>();
            for (int i2 = 0; i2 < PlanetaHrace.Count(); i2++)
            {
                rezim.Add(1);
            }

            ;
            for (int a = 0; a < PlanetaHrace.Count(); a++)
            {

                switch (rezim[a])
                {

                    case 1:
                        /*
                        Balanced
                        Snaží se mít kov,krystal,S.elektrárnu na stejné urovni 
                        když dul na kov je nižší než krystal
                        */
                        {
                            browser.UpgradeSuply(1);
                            /*if (PlanetaHrace[a].suply[0].lv < PlanetaHrace[a].suply[1].lv)
                            {
                                //něco se možná stane
                                
                            }*/
                        }
                        break;
                    /*
                    ProductionBased
                    Snaží se vyrovnávat produkci (kov je vyšší)



                    */
                    case 2:
                        {
                            if (PlanetaHrace[a].suply[0].lv < PlanetaHrace[a].suply[1].lv)
                            {
                                //něco se možná stane
                            }
                        }
                        break;
                    case 3:
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
                    default:

                        break;
                }
            }
        }


        public List<Player.Planet> InfoDoly()
        {

            List<Player.Planet> planet = new List<Player.Planet>();
            for (int i = 0; i < browserManipulation.InfoPlayerNumberOfPlanets(); i++)
            {
                planet.Add(new Player.Planet());

                planet[i].suply = browserManipulation.InfoSuply(i + 1);

            }
            return planet;
        }

    }
}