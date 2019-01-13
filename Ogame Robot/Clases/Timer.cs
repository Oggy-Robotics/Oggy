using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ogame_Robot.Clases
{

    public partial class Timer
    {
        public List<PackedFunction> packedFunctions = new List<PackedFunction>();
        public void Call(int i)
        {
            packedFunctions[i].nextCall = DateTime.Now + packedFunctions[i].CallFunction();
            ;
        }
        public TimeSpan UntilNextCall()
        {
            int id = 0;
            for (int i = 0; i < packedFunctions.Count(); i++)
            {
                if (packedFunctions[i].nextCall < packedFunctions[id].nextCall)
                    id = i;
            }

            return DateTime.Now - packedFunctions[id].nextCall;
        }
        public TimeSpan WaitAndCallNext(bool wait)
        {
            int id = 0;
            for (int i = 0; i < packedFunctions.Count(); i++)
            {
                if (packedFunctions[i].nextCall < packedFunctions[id].nextCall)
                    id = i;
            }
            if (DateTime.Now - packedFunctions[id].nextCall < TimeSpan.FromMilliseconds(0) && wait)//nothing important to do in meantime/short meantime
            {
                Thread.Sleep(packedFunctions[id].nextCall - DateTime.Now);

            }
            else if (DateTime.Now - packedFunctions[id].nextCall > TimeSpan.FromMilliseconds(0) && !wait)
            {
                return DateTime.Now - packedFunctions[id].nextCall;
            }

            Call(id);

            return UntilNextCall();
        }
        /// <summary>
        /// in real situation it should be realy complex
        /// </summary>
        public void Start()
        {
            int crashCounter = 0;
            while (true)
            {
                    WaitAndCallNext(true);
                try
                {
                }
                catch (Exception e)
                {
                    crashCounter++;
                    FilesOperations.ErrorLogFileAddError(e);

                    DataBase.dataBase.game.browserManipulation.driver.Quit();
                    DataBase.dataBase.game.browserManipulation.StartBrowser();
                    DataBase.dataBase.game.browserManipulation.Loggin();
                    DataBase.dataBase.game.browserManipulation.LogginUniverse();

                }

            }
        }



        public class PackedFunction : Timer
        {
            public PackedFunction()
            { }

            public DateTime nextCall;
            public TimeSpan standardFrequency;

            public virtual TimeSpan CallFunction()
            {
                return standardFrequency;
            }



        }

        public class InfoPlayer : PackedFunction
        {
            public BrowserManipulation browser;

            public InfoPlayer(Timer timer, BrowserManipulation browser, TimeSpan standardFrequency)
            {
                this.browser = browser;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public InfoPlayer(Timer timer, BrowserManipulation browser, TimeSpan standardFrequency, TimeSpan addTime)
            {
                this.browser = browser;
                nextCall = DateTime.Now + addTime;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public override TimeSpan CallFunction()
            {
                browser.InfoPlayer();

                return standardFrequency;
            }
        }
        public class UnderAttack : PackedFunction
        {
            public BrowserManipulation browser;
            public int numberOfCall = 0;

            public UnderAttack(Timer timer, BrowserManipulation browser, TimeSpan standardFrequency)
            {
                this.browser = browser;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public UnderAttack(Timer timer, BrowserManipulation browser, TimeSpan standardFrequency, TimeSpan addTime)
            {
                this.browser = browser;
                nextCall = DateTime.Now + addTime;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public override TimeSpan CallFunction()
            {
                browser.UnderAttack();


                numberOfCall++;
                return standardFrequency;
            }
        }

    }

    //vzor vytvoření balíčků funkcí pro časovač.
    public partial class Timer
    {
        public class FarmInactive : PackedFunction
        {
            public BrowserManipulation browser;
            public int numberOfCall = 0;

            public FarmInactive(Timer timer, BrowserManipulation browser, TimeSpan standardFrequency)
            {
                this.browser = browser;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public FarmInactive(Timer timer, BrowserManipulation browser, TimeSpan standardFrequency, TimeSpan addTime)
            {
                this.browser = browser;
                nextCall = DateTime.Now + addTime;
                this.standardFrequency = standardFrequency;
                timer.packedFunctions.Add(this);
            }
            public override TimeSpan CallFunction()
            {
                browser.FarmInactive(1, 200, 0.0, false, 18, 3, 450);


                numberOfCall++;
                return standardFrequency;
            }
        }
    }
}
